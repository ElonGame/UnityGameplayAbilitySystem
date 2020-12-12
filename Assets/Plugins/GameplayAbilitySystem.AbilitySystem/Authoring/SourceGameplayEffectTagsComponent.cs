using GameplayAbilitySystem.AbilitySystem;
using Unity.Entities;

[assembly: RegisterGenericComponentType(typeof(AbilitySystemTagsDefinitionComponent<ISourceGameplayEffectTags>.Component))]
namespace GameplayAbilitySystem.AbilitySystem
{
    public interface ISourceGameplayEffectTags : IAbilityTagDefinition { }
    public class SourceGameplayEffectTagsComponent : AbilitySystemTagsDefinitionComponent<ISourceGameplayEffectTags> { }
}
