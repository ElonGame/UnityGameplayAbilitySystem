
using UnityEngine;
using Unity.Entities;
using GameplayAbilitySystem.GameplayTags;
using System.Collections.Generic;
using GameplayAbilitySystem.AttributeSystem;

namespace GameplayAbilitySystem.AbilitySystem
{
    public class GrantedAbilitiesAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public GameplayTagScriptableObject[] GrantedAbilities;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var buffer = dstManager.AddBuffer<Component>(entity);
            for (var i = 0; i < GrantedAbilities.Length; i++)
            {
                buffer.Add(new Component()
                {
                    Value = GrantedAbilities[i].Tag
                });
            }
        }
        public struct Component : IBufferElementData
        {
            public static implicit operator GameplayTag(Component e) { return e.Value; }
            public static implicit operator Component(GameplayTag e) { return new Component { Value = e }; }
            public GameplayTag Value;
        }
    }
}