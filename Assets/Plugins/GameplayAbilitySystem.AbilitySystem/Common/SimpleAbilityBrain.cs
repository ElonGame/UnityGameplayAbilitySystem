using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{

    public class SimpleAbilityBrain : AbstractAbilityBrain<SimpleAbilityBrainSystem>
    {
    }

    public class SimpleAbilityBrainSystem : AbstractAbilityBrainSystem
    {
        private EntityQuery m_Query;
        protected override EntityQuery Query { get => m_Query; set => m_Query = value; }

        protected override EntityQuery InitialiseEntityQuery()
        {
            return Query;
        }

        protected override void OnUpdate()
        {

        }
    }
}