using System.Collections.Generic;
using GameplayAbilitySystem.GameplayTags;
using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{
    public abstract class AbstractAbilityBrain<T> : MonoBehaviour, IConvertGameObjectToEntity
    where T : AbstractAbilityBrainSystem
    {
        [SerializeField]
        protected GameplayTagScriptableObject Tag;


        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var system = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<T>();
            var componentSpecs = this.gameObject.GetComponents<ConvertToSpec>();
            var components = new List<ComponentType>() {
                    ComponentType.ReadOnly<AbilityIdentifier.Component>(),
                    ComponentType.ReadOnly<AbilitySystemDefinition.Component>(),
                    ComponentType.ReadOnly<AbilityCooldownComponent.Component>(),
                    ComponentType.ReadOnly<AbilityCostComponent.Component>(),
                    ComponentType.ReadOnly<CancelAbilitiesWithTagsComponent.Component>(),
                    ComponentType.ReadOnly<BlockAbilitiesWithTagsComponent.Component>(),
                    ComponentType.ReadOnly<ActiveGrantedTagsComponent.Component>(),
                    ComponentType.ReadOnly<ActivationRequiredTagsComponent.Component>(),
                    ComponentType.ReadOnly<ActivationBlockedTagsComponent.Component>(),
                    ComponentType.ReadOnly<TargetRequiredTagsComponent.Component>(),
                    ComponentType.ReadOnly<TargetBlockedTagsComponent.Component>(),
                    ComponentType.ReadOnly<AbilityBrainComponent.Component>(),
            };


            var abilityBrainComponent = new AbilityBrainComponent.Component() { Tag = Tag.Tag };

            system.SetQuery(components);
            system.Tag = Tag.Tag;

        }
    }
}