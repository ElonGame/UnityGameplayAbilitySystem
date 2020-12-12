using System;
using System.Runtime.CompilerServices;
using Unity.Entities;
using GameplayAbilitySystem.AttributeSystem;

namespace MyGameplayAbilitySystem
{

    [Serializable]
    public struct MyPlayerAttributes : IComponentData, IAttributeDefinition
    {
        public float Health;
        public float MaxHealth;
        public float Mana;
        public float MaxMana;
        public float Speed;
        public float PhysicalAttackDamage;
        public float MagicAttackDamage;
        public float PhysicalDefense;
        public float MagicDefense;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float GetPlayerAttribute(EMyPlayerAttribute attribute)
        {
            switch (attribute)
            {
                case EMyPlayerAttribute.Health:
                    return Health;
                case EMyPlayerAttribute.MaxHealth:
                    return MaxHealth;
                case EMyPlayerAttribute.Mana:
                    return Mana;
                case EMyPlayerAttribute.MaxMana:
                    return MaxMana;
                case EMyPlayerAttribute.Speed:
                    return Speed;
                case EMyPlayerAttribute.PhysicalAttackDamage:
                    return PhysicalAttackDamage;
                case EMyPlayerAttribute.MagicAttackDamage:
                    return MagicAttackDamage;
                case EMyPlayerAttribute.PhysicalDefense:
                    return PhysicalDefense;
                case EMyPlayerAttribute.MagicDefense:
                    return MagicDefense;
                default:
                    return 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetPlayerAttribute(EMyPlayerAttribute attribute, float value)
        {
            switch (attribute)
            {
                case EMyPlayerAttribute.Health:
                    Health = value;
                    break;
                case EMyPlayerAttribute.MaxHealth:
                    MaxHealth = value;
                    break;
                case EMyPlayerAttribute.Mana:
                    Mana = value;
                    break;
                case EMyPlayerAttribute.MaxMana:
                    MaxMana = value;
                    break;
                case EMyPlayerAttribute.Speed:
                    Speed = value;
                    break;
                case EMyPlayerAttribute.PhysicalAttackDamage:
                    PhysicalAttackDamage = value;
                    break;
                case EMyPlayerAttribute.MagicAttackDamage:
                    MagicAttackDamage = value;
                    break;
                case EMyPlayerAttribute.PhysicalDefense:
                    PhysicalDefense = value;
                    break;
                case EMyPlayerAttribute.MagicDefense:
                    MagicDefense = value;
                    break;
            }
        }

        public float this[EMyPlayerAttribute attribute]
        {
            get
            {
                return GetPlayerAttribute(attribute);
            }
            set
            {
                SetPlayerAttribute(attribute, value);
            }

        }
    }

}


