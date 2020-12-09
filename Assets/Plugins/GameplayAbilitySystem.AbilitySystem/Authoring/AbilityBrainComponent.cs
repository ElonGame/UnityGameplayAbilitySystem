using GameplayAbilitySystem.GameplayTags;
using Unity.Entities;
using UnityEngine;
namespace GameplayAbilitySystem.AbilitySystem
{
    [DisallowMultipleComponent]
    public class AbilityBrainComponent : ConvertToSpec
    {
        public GameplayTagScriptableObject Brain;
        public override void CreateSpec(Entity entity, EntityManager dstManager)
        {
            dstManager.AddComponent<Component>(entity);
            dstManager.SetSharedComponentData<Component>(entity, new Component() { Tag = Brain.Tag });
        }

        public override ComponentType GetComponentType()
        {
            return ComponentType.ReadOnly<Component>();
        }

        public struct Component : ISharedComponentData
        {
            public GameplayTag Tag;
        }
    }
}

