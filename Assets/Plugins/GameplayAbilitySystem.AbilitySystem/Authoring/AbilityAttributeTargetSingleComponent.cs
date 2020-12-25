using Unity.Entities;

namespace GameplayAbilitySystem.AbilitySystem
{
    public struct AbilityAttributeTargetSingleComponent : IComponentData
    {
        public Entity Value;
        public static implicit operator Entity(AbilityAttributeTargetSingleComponent e) { return e.Value; }
        public static implicit operator AbilityAttributeTargetSingleComponent(Entity e) { return new AbilityAttributeTargetSingleComponent { Value = e }; }
    }

    public struct AbilityActorTargetSingleComponent : IComponentData
    {
        public Entity Value;
        public static implicit operator Entity(AbilityActorTargetSingleComponent e) { return e.Value; }
        public static implicit operator AbilityActorTargetSingleComponent(Entity e) { return new AbilityActorTargetSingleComponent { Value = e }; }
    }
}
