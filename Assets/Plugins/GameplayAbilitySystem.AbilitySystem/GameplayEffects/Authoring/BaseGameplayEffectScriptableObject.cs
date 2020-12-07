using GameplayAbilitySystem.AttributeSystem;
using GameplayAbilitySystem.GameplayTags;
using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{
    public abstract class BaseGameplayEffectScriptableObject<TGameplayEffectSpec, TAttributeModifierEnum, TAttributeModifierOperatorEnum> : ScriptableObject, IGameplayEffectAuthorer<TGameplayEffectSpec>, IComponentData
    where TGameplayEffectSpec : IGameplayEffectIdentifier
    where TAttributeModifierEnum : System.Enum
    where TAttributeModifierOperatorEnum : System.Enum
    {
        [Header("Effect Unique ID")]
        [Tooltip("Unique ID for this Effect")]
        [SerializeField]
        public GameplayTagScriptableObject Id;

        [Header("Grouping ID")]
        [Tooltip("Group ID for this Effect.  Used for controlling the effect logic.")]
        [SerializeField]
        public uint GroupEffectId;

        [Header("Effect Specification")]
        public DurationPolicy Duration;
        public Modifiers<TAttributeModifierEnum, TAttributeModifierOperatorEnum>[] AttributeModifiers;
        //public PeriodPolicy Period;

        [Header("Effect Tags")]
        public GameplayEffectTags EffectTags;
        public abstract TGameplayEffectSpec CreateGameplayEffect(TGameplayEffectSpec Spec);
        public abstract Entity ApplyGameplayEffect(EntityManager dstManager, TGameplayEffectSpec GameplayEffectSpec);
    }

    public class GameplayEffectTags
    {
        public GameplayTagScriptableObject[] AssetTags;
        public GameplayTagScriptableObject[] GrantedTags;
        public GameplayTagScriptableObject[] OngoingTagRequirements;
        public GameplayTagScriptableObject[] ApplicationTagRequirements;
        public GameplayTagScriptableObject[] RemoveGameplayEffectsWithTags;
        public GameplayTagScriptableObject[] GrantedApplicationImmunityTags;
    }




    public abstract class ModifierExecutionCalculation<TGameplayEffectSpec, TInstantAttributesModifier, TDurationalAttributesModifier> : ScriptableObject
    where TGameplayEffectSpec : IGameplayEffectIdentifier
    where TInstantAttributesModifier : IAttributeModifier
    where TDurationalAttributesModifier : IAttributeModifier
    {
        public abstract void Modify(ref TInstantAttributesModifier InstantAttributeModifier, ref TDurationalAttributesModifier DurationalAttributeModifier, TGameplayEffectSpec GameplayEffectSpec);
    }

}
