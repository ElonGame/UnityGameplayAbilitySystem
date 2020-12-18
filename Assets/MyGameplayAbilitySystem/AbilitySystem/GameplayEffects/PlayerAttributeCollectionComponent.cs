using Unity.Entities;
using MyGameplayAbilitySystem;

public struct PlayerAttributeCollectionComponent : IComponentData
{
    public MyAttributeValues Source;
    public MyAttributeValues Target;
}
