using GameplayAbilitySystem.AbilitySystem;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericComponentType(typeof(AbilitySystemTagsDefinitionComponent<ITargetRequiredTags>.Component))]
namespace GameplayAbilitySystem.AbilitySystem
{
    public interface ITargetRequiredTags : IAbilityTagDefinition { }
    public class TargetRequiredTagsComponent : AbilitySystemTagsDefinitionComponent<ITargetRequiredTags> { }
}
