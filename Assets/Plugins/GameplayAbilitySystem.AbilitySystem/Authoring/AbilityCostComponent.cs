using GameplayAbilitySystem.GameplayTags;
using Unity.Entities;
using UnityEngine;
namespace GameplayAbilitySystem.AbilitySystem
{
    [DisallowMultipleComponent]
    public class AbilityCostComponent : ConvertToSpec
    {
        public GameplayTagScriptableObject GameplayEffect;
        public override void CreateSpec(Entity entity, EntityManager dstManager)
        {
            dstManager.AddComponent<Component>(entity);
            if (Object.ReferenceEquals(GameplayEffect, null)) return;
            dstManager.SetComponentData<Component>(entity, new Component()
            {
                Id = GameplayEffect.Tag
            });
        }
        public override ComponentType GetComponentType()
        {
            return ComponentType.ReadOnly<Component>();
        }
        public struct Component : IComponentData
        {
            public GameplayTag Id;
        }

    }
}
