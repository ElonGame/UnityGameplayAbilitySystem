using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{
    public class GameplayEffectDurationPolicy : ConvertToSpec
    {
        public EDurationPolicy DurationType;
        public float Duration;

        public override void CreateSpec(Entity entity, EntityManager dstManager)
        {
            dstManager.AddComponent<Component>(entity);
            dstManager.SetComponentData<Component>(entity, new Component()
            {
                DurationType = DurationType,
                Duration = Duration
            });
        }
        public override ComponentType GetComponentType()
        {
            return default(ComponentType);
        }

        public struct Component : IComponentData
        {
            public EDurationPolicy DurationType;
            public float Duration;
        }
    }
}
