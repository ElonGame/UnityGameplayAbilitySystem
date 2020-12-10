using GameplayAbilitySystem.AbilitySystem;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericComponentType(typeof(AbilitySystemTagsDefinitionComponent<ITargetBlockedTags>.Component))]
namespace GameplayAbilitySystem.AbilitySystem
{
    public interface ITargetBlockedTags : IAbilityTagDefinition { }
    public class TargetBlockedTagsComponent : AbilitySystemTagsDefinitionComponent<ITargetBlockedTags> { }
}
