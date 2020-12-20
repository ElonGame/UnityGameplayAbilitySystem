
using UnityEngine;
using Unity.Entities;
using GameplayAbilitySystem.GameplayTags;

namespace GameplayAbilitySystem.AbilitySystem
{
    public class AbilitySystemActorTags : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var buffer = dstManager.AddBuffer<Component>(entity);
        }
        public struct Component : IBufferElementData
        {
            public static implicit operator GameplayTag(Component e) { return e.Value; }
            public static implicit operator Component(GameplayTag e) { return new Component { Value = e }; }
            public GameplayTag Value;
        }
    }
}