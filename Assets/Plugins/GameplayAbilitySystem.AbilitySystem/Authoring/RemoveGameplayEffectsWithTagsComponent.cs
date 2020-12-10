using GameplayAbilitySystem.AbilitySystem;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericComponentType(typeof(AbilitySystemTagsDefinitionComponent<IRemoveGameplayEffectsWithTags>.Component))]
namespace GameplayAbilitySystem.AbilitySystem
{
    public interface IRemoveGameplayEffectsWithTags : IAbilityTagDefinition { }
    public class RemoveGameplayEffectsWithTagsComponent : AbilitySystemTagsDefinitionComponent<IRemoveGameplayEffectsWithTags> { }
}
