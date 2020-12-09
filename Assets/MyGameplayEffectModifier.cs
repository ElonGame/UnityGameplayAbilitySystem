using System.Collections;
using System.Collections.Generic;
using GameplayAbilitySystem.AbilitySystem;
using MyGameplayAbilitySystem;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericComponentType(typeof(GameplayEffectModifier<EMyPlayerAttribute, EMyAttributeModifierOperator>.Component))]
namespace MyGameplayAbilitySystem
{
    public class MyGameplayEffectModifier : GameplayEffectModifier<EMyPlayerAttribute, EMyAttributeModifierOperator> { }
}