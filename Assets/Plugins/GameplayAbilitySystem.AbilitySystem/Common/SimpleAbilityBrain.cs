using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{
    public interface IAbilityLogic<TEnum>
    where TEnum : System.Enum
    {
        TEnum CanActivateAbility();
        TEnum ActivateAbility();
        IAbilityLogic<TEnum> SetActorHost(MonoBehaviour host);

    }

    public enum EAbilityStateEnum
    {
        SUCCESS, ON_COOLDOWN, INSUFFICIENT_RESOURCE, GAMEPLAY_EFFECTS_APPLIED
    }

    public class SimpleAbilityBrainLogic : IAbilityLogic<EAbilityStateEnum>
    {
        private MonoBehaviour abilityActorHost;
        public EAbilityStateEnum ActivateAbility()
        {
            if (CanActivateAbility() == EAbilityStateEnum.SUCCESS)
            {
                //ActivateAbility();
                if (CommitAbility() == EAbilityStateEnum.SUCCESS)
                {

                }
            }

            return EAbilityStateEnum.SUCCESS;

        }

        public EAbilityStateEnum CanActivateAbility()
        {
            return EAbilityStateEnum.ON_COOLDOWN;
        }
        private EAbilityStateEnum CommitAbility()
        {
            return EAbilityStateEnum.ON_COOLDOWN;

        }

        private EAbilityStateEnum ApplyGameplayEffects()
        {
            return EAbilityStateEnum.GAMEPLAY_EFFECTS_APPLIED;
        }
        private EAbilityStateEnum EndAbility()
        {
            return EAbilityStateEnum.ON_COOLDOWN;

        }
        public IAbilityLogic<EAbilityStateEnum> SetActorHost(MonoBehaviour host)
        {
            this.abilityActorHost = host;
            return this;
        }
    }

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