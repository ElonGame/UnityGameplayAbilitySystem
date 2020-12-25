
using UnityEngine;
using Unity.Entities;
using GameplayAbilitySystem.GameplayTags;

namespace GameplayAbilitySystem.AbilitySystem
{
    public class ActorActiveAbilitiesAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var buffer = dstManager.AddBuffer<Component>(entity);
        }
        public struct Component : IBufferElementData
        {
            public static implicit operator GameplayTag(Component e) { return e.Tag; }
            public static implicit operator Component(GameplayTag e) { return new Component { Tag = e }; }
            public GameplayTag Tag;
            public Entity EntityReference;
        }
    }
}