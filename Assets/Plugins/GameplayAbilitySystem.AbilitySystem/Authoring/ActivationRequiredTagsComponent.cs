using GameplayAbilitySystem.AbilitySystem;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericComponentType(typeof(AbilitySystemTagsDefinitionComponent<IActivationRequiredTags>.Component))]
namespace GameplayAbilitySystem.AbilitySystem
{
    public interface IActivationRequiredTags : IAbilityTagDefinition { }
    public class ActivationRequiredTagsComponent : AbilitySystemTagsDefinitionComponent<IActivationRequiredTags> { }
}
