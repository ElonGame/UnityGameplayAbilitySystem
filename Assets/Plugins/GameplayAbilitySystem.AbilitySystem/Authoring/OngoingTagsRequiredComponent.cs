using GameplayAbilitySystem.AbilitySystem;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericComponentType(typeof(AbilitySystemTagsDefinitionComponent<IOngoingTagsRequired>.Component))]
namespace GameplayAbilitySystem.AbilitySystem
{
    public interface IOngoingTagsRequired : IAbilityTagDefinition { }
    public class OngoingTagsRequiredComponent : AbilitySystemTagsDefinitionComponent<IOngoingTagsRequired> { }
}
