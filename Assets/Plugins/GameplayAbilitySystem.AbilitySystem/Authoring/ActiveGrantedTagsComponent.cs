using GameplayAbilitySystem.AbilitySystem;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericComponentType(typeof(AbilitySystemTagsDefinitionComponent<IActiveGrantedTags>.Component))]

namespace GameplayAbilitySystem.AbilitySystem
{
    public interface IActiveGrantedTags : IAbilityTagDefinition { }

    public class ActiveGrantedTagsComponent : AbilitySystemTagsDefinitionComponent<IActiveGrantedTags> { }
}
