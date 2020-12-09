using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameplayAbilitySystem.AbilitySystem
{
    public class AbilitySystemDefinition : ConvertToSpec
    {
        public string Name;
        public string Description;
        public Sprite Icon;

        public override void CreateSpec(Entity entity, EntityManager dstManager)
        {
            dstManager.AddComponent<Component>(entity);
            dstManager.SetComponentData<Component>(entity, new Component()
            {
                Description = Description,
                Name = Name,
                Icon = Icon
            });
        }

        public override ComponentType GetComponentType()
        {
            return default(ComponentType);
        }

        public class Component : IComponentData
        {
            public string Name;
            public string Description;
            public Sprite Icon;
        }
    }


}
