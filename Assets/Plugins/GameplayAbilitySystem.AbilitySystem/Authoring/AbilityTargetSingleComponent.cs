using Unity.Entities;

namespace GameplayAbilitySystem.AbilitySystem
{
    public struct AbilityTargetSingleComponent : IComponentData
    {
        public Entity Target;
    }
}
