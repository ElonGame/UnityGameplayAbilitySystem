using System.Collections.Generic;
using System.Linq;
using GameplayAbilitySystem.GameplayTags;
using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{
    public abstract class AbstractAbilityBrain<TAbilityBrainSystem> : MonoBehaviour, IConvertGameObjectToEntity
    where TAbilityBrainSystem : AbstractAbilityBrainSystem
    {
        [SerializeField]
        protected GameplayTagScriptableObject Tag;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var system = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<TAbilityBrainSystem>();
            var components = this.gameObject.GetComponents<ConvertToSpec>().Select(x => x.GetComponentType()).Where(x => x != default(ComponentType)).ToList();
            system.SetQuery(components);
            system.Tag = Tag.Tag;
        }
    }
}