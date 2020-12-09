using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{
    public abstract class GameplayEffectModifier<TAttributeModifierEnum, TAttributeModifierOperatorEnum> : ConvertToSpec
    where TAttributeModifierEnum : System.Enum
    where TAttributeModifierOperatorEnum : System.Enum
    {
        public Component[] Modifiers;
        public override void CreateSpec(Entity entity, EntityManager dstManager)
        {
            var buffer = dstManager.AddBuffer<Component>(entity);
            for (var i = 0; i < Modifiers.Length; i++)
            {
                buffer.Add(Modifiers[i]);
            }
        }
        public override ComponentType GetComponentType()
        {
            return default(ComponentType);
        }
        [Serializable]
        public struct Component : IBufferElementData
        {
            public TAttributeModifierOperatorEnum Modifier;
            public TAttributeModifierEnum Attribute;
            public float ModificationValue;
        }
    }
}
