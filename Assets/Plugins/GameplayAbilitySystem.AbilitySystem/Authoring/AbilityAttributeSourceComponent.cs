using Unity.Entities;

namespace GameplayAbilitySystem.AbilitySystem
{
    public struct AbilityAttributeSourceComponent : IComponentData
    {
        public Entity Value;

        public static implicit operator Entity(AbilityAttributeSourceComponent e) { return e.Value; }
        public static implicit operator AbilityAttributeSourceComponent(Entity e) { return new AbilityAttributeSourceComponent { Value = e }; }
    }
}