using Unity.Entities;

namespace GameplayAbilitySystem.AbilitySystem
{
    public struct AbilitySpecEntityComponent : IComponentData
    {
        public Entity Value;
        public static implicit operator Entity(AbilitySpecEntityComponent e) { return e.Value; }
        public static implicit operator AbilitySpecEntityComponent(Entity e) { return new AbilitySpecEntityComponent { Value = e }; }
    }
}