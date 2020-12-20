using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{
    public class SimpleAbilityBrain : AbilityBrainSystemAuthor<SimpleAbilityBrain.System>
    {
        [DisableAutoCreation]
        public class System : AbilityBrainSystem
        {
            protected override void OnUpdate()
            {
            }
        }
    }


}


