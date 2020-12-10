using Unity.Entities;
using UnityEngine;
using GameplayAbilitySystem.GameplayTags;
using GameplayAbilitySystem.AbilitySystem;

namespace GameplayAbilitySystem.AbilitySystem
{
    public interface IAbilityTagDefinition { }

    [DisallowMultipleComponent]
    public abstract class AbilitySystemTagsDefinitionComponent<T> : ConvertToSpec
    where T : IAbilityTagDefinition
    {
        public GameplayTagScriptableObject[] Tags;

        public override void CreateSpec(Entity entity, EntityManager dstManager)
        {
            var buffer = dstManager.AddBuffer<Component>(entity);
            for (var i = 0; i < Tags.Length; i++)
            {
                buffer.Add(Tags[i].Tag);
            }

        }

        public override ComponentType GetComponentType()
        {
            return default(ComponentType);
        }

        [InternalBufferCapacity(4)]
        public struct Component : IBufferElementData
        {
            public GameplayTag Value;

            public static implicit operator GameplayTag(Component e)
            {
                return e.Value;
            }

            public static implicit operator Component(GameplayTag e)
            {
                return new Component { Value = e };
            }
        }
    }
}
