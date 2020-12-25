using Unity.Entities;

namespace GameplayAbilitySystem.AbilitySystem
{
    public struct AbilityActorSourceComponent : IComponentData
    {
        public Entity Value;

        public static implicit operator Entity(AbilityActorSourceComponent e) { return e.Value; }
        public static implicit operator AbilityActorSourceComponent(Entity e) { return new AbilityActorSourceComponent { Value = e }; }
    }
}