using GameplayAbilitySystem.AbilitySystem;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericComponentType(typeof(AbilitySystemTagsDefinitionComponent<ISourceBlockedTags>.Component))]
namespace GameplayAbilitySystem.AbilitySystem
{
    public interface ISourceBlockedTags : IAbilityTagDefinition { }
    public class SourceBlockedTagsComponent : AbilitySystemTagsDefinitionComponent<ISourceBlockedTags> { }
}
