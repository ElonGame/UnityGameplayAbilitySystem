using Unity.Entities;

namespace GameplayAbilitySystem.AbilitySystem
{
    public struct AbilitySourceComponent : IComponentData
    {
        public Entity Target;
    }
}