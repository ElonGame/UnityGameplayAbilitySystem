using Unity.Entities;
using GameplayAbilitySystem.GameplayTags;

namespace GameplayAbilitySystem.AbilitySystem
{
    public abstract class AbilitySystemIdentifier<TRegistry> : ConvertToSpec
    where TRegistry : AbilitySystemRegistryScriptableObject
    {
        public GameplayTagScriptableObject Id;
        public TRegistry Registry;

        public override void CreateSpec(Entity entity, EntityManager dstManager)
        {
            dstManager.AddComponent<Component>(entity);
            dstManager.SetComponentData(entity, new Component()
            {
                Tag = Id.Tag
            });

            Registry.Add(Id.Tag, entity);
        }

        public override ComponentType GetComponentType()
        {
            return ComponentType.ReadOnly<Component>();
        }

        public struct Component : IComponentData
        {
            public GameplayTag Tag;
        }
    }
}
