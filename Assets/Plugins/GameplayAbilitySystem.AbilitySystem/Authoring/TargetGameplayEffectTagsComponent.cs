using GameplayAbilitySystem.AbilitySystem;
using Unity.Entities;

[assembly: RegisterGenericComponentType(typeof(AbilitySystemTagsDefinitionComponent<ITargetGameplayEffectTags>.Component))]
namespace GameplayAbilitySystem.AbilitySystem
{
    public interface ITargetGameplayEffectTags : IAbilityTagDefinition { }
    public class TargetGameplayEffectTagsComponent : AbilitySystemTagsDefinitionComponent<ITargetGameplayEffectTags> { }
}
