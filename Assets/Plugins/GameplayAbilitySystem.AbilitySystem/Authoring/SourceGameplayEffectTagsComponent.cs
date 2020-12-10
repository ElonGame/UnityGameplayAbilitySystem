namespace GameplayAbilitySystem.AbilitySystem
{
    public interface ISourceGameplayEffectTags : IAbilityTagDefinition { }
    public class SourceGameplayEffectTagsComponent : AbilitySystemTagsDefinitionComponent<ISourceGameplayEffectTags> { }
}
