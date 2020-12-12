using System;
using GameplayAbilitySystem.AttributeSystem;
using Unity.Entities;

namespace MyGameplayAbilitySystem
{
    [Serializable]
    public struct MyAttributeValues : IAttributeData, IComponentData
    {
        public MyPlayerAttributes BaseValue;
        public MyPlayerAttributes CurrentValue;
    }

}
