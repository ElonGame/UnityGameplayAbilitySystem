using Unity.Entities;

namespace GameplayAbilitySystem.AbilitySystem
{
    public struct AbilityAttributeTargetSingleComponent : IComponentData
    {
        public Entity Value;
        public static implicit operator Entity(AbilityAttributeTargetSingleComponent e) { return e.Value; }
        public static implicit operator AbilityAttributeTargetSingleComponent(Entity e) { return new AbilityAttributeTargetSingleComponent { Value = e }; }
    }
}
