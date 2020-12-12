using System.Runtime.CompilerServices;
using GameplayAbilitySystem.AttributeSystem;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace MyGameplayAbilitySystem
{
    [UpdateInGroup(typeof(AttributeUpdateSystemGroup))]
    public class MyAttributeUpdateSystem : AttributeUpdateSystem<MyAttributeValues, MyInstantAttributeModifierValues, MyDurationalAttributeModifierValues, MyPlayerAttributesJob>
    {
        public static Entity CreateGameplayEffect(EntityManager dstManager, GameplayEffectContextComponent context, MyDurationalGameplayAttributeModifier modifier)
        {
            var attributeModifierArchetype = dstManager.CreateArchetype(typeof(GameplayEffectContextComponent), typeof(MyDurationalGameplayAttributeModifier));
            var entity = dstManager.CreateEntity(attributeModifierArchetype);
            dstManager.SetComponentData(entity, context);
            dstManager.SetComponentData(entity, modifier);
            return entity;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            // Create dummy entities for testing
            //CreateTestEntities(attributeArchetype, attributeModifierArchetype);
        }

        private void CreateTestEntities(EntityArchetype attributeArchetype, EntityArchetype attributeModifierArchetype)
        {
            var rand = new Random(1);

            for (var i = 0; i < 100; i++)
            {
                var defaultAttributes = new MyAttributeValues()
                {
                    BaseValue = new MyPlayerAttributes() { Health = 100, MaxHealth = 100, Mana = 10, MaxMana = 10, Speed = 5 }
                };

                var entity = CreatePlayerEntity(EntityManager, defaultAttributes);

                for (var j = 0; j < 100; j++)
                {
                    var context = new GameplayEffectContextComponent()
                    {
                        Source = entity,
                        Target = entity
                    };

                    var attributesModifier = new MyDurationalGameplayAttributeModifier()
                    {
                        Attribute = (EMyPlayerAttribute)(rand.NextInt(0, 5)),
                        Operator = (EMyAttributeModifierOperator)(rand.NextInt(1, 1)),
                        Value = (half)rand.NextFloat(0, 10)
                    };

                    CreateGameplayEffect(EntityManager, context, attributesModifier);
                }

            }
        }
    }

    public struct MyPlayerAttributesJob : IAttributeExecute<MyAttributeValues, MyInstantAttributeModifierValues, MyDurationalAttributeModifierValues>
    {
        public void CalculateDurational(NativeArray<MyAttributeValues> attributeValuesChunk, NativeArray<MyDurationalAttributeModifierValues> attributeModifiersChunk)
        {
            for (var i = 0; i < attributeValuesChunk.Length; i++)
            {
                var attributeValues = attributeValuesChunk[i];
                var attributeModifierValues = attributeModifiersChunk[i];
                attributeValues.CurrentValue[EMyPlayerAttribute.Health] = ModifyValues(attributeValues.BaseValue[EMyPlayerAttribute.Health], attributeModifierValues.AddValue[EMyPlayerAttribute.Health], attributeModifierValues.MultiplyValue[EMyPlayerAttribute.Health], attributeModifierValues.DivideValue[EMyPlayerAttribute.Health], attributeModifierValues.OverrideValue[EMyPlayerAttribute.Health]);
                attributeValues.CurrentValue[EMyPlayerAttribute.MaxHealth] = ModifyValues(attributeValues.BaseValue[EMyPlayerAttribute.MaxHealth], attributeModifierValues.AddValue[EMyPlayerAttribute.MaxHealth], attributeModifierValues.MultiplyValue[EMyPlayerAttribute.MaxHealth], attributeModifierValues.DivideValue[EMyPlayerAttribute.MaxHealth], attributeModifierValues.OverrideValue[EMyPlayerAttribute.MaxHealth]);
                attributeValues.CurrentValue[EMyPlayerAttribute.Mana] = ModifyValues(attributeValues.BaseValue[EMyPlayerAttribute.Mana], attributeModifierValues.AddValue[EMyPlayerAttribute.Mana], attributeModifierValues.MultiplyValue[EMyPlayerAttribute.Mana], attributeModifierValues.DivideValue[EMyPlayerAttribute.Mana], attributeModifierValues.OverrideValue[EMyPlayerAttribute.Mana]);
                attributeValues.CurrentValue[EMyPlayerAttribute.MaxMana] = ModifyValues(attributeValues.BaseValue[EMyPlayerAttribute.MaxMana], attributeModifierValues.AddValue[EMyPlayerAttribute.MaxMana], attributeModifierValues.MultiplyValue[EMyPlayerAttribute.MaxMana], attributeModifierValues.DivideValue[EMyPlayerAttribute.MaxMana], attributeModifierValues.OverrideValue[EMyPlayerAttribute.MaxMana]);
                attributeValues.CurrentValue[EMyPlayerAttribute.Speed] = ModifyValues(attributeValues.BaseValue[EMyPlayerAttribute.Speed], attributeModifierValues.AddValue[EMyPlayerAttribute.Speed], attributeModifierValues.MultiplyValue[EMyPlayerAttribute.Speed], attributeModifierValues.DivideValue[EMyPlayerAttribute.Speed], attributeModifierValues.OverrideValue[EMyPlayerAttribute.Speed]);
                ClampAttributes(ref attributeValues.CurrentValue);
                attributeValuesChunk[i] = attributeValues;
            }
        }
        public void CalculateInstant(NativeArray<MyAttributeValues> attributeValuesChunk, NativeArray<MyInstantAttributeModifierValues> attributeModifiersChunk)
        {
            for (var i = 0; i < attributeValuesChunk.Length; i++)
            {
                var attributeValues = attributeValuesChunk[i];
                var attributeModifierValues = attributeModifiersChunk[i];
                attributeValues.BaseValue[EMyPlayerAttribute.Health] = ModifyValues(attributeValues.BaseValue[EMyPlayerAttribute.Health], attributeModifierValues.AddValue[EMyPlayerAttribute.Health], attributeModifierValues.MultiplyValue[EMyPlayerAttribute.Health], attributeModifierValues.DivideValue[EMyPlayerAttribute.Health], attributeModifierValues.OverrideValue[EMyPlayerAttribute.Health]);
                attributeValues.BaseValue[EMyPlayerAttribute.MaxHealth] = ModifyValues(attributeValues.BaseValue[EMyPlayerAttribute.MaxHealth], attributeModifierValues.AddValue[EMyPlayerAttribute.MaxHealth], attributeModifierValues.MultiplyValue[EMyPlayerAttribute.MaxHealth], attributeModifierValues.DivideValue[EMyPlayerAttribute.MaxHealth], attributeModifierValues.OverrideValue[EMyPlayerAttribute.MaxHealth]);
                attributeValues.BaseValue[EMyPlayerAttribute.Mana] = ModifyValues(attributeValues.BaseValue[EMyPlayerAttribute.Mana], attributeModifierValues.AddValue[EMyPlayerAttribute.Mana], attributeModifierValues.MultiplyValue[EMyPlayerAttribute.Mana], attributeModifierValues.DivideValue[EMyPlayerAttribute.Mana], attributeModifierValues.OverrideValue[EMyPlayerAttribute.Mana]);
                attributeValues.BaseValue[EMyPlayerAttribute.MaxMana] = ModifyValues(attributeValues.BaseValue[EMyPlayerAttribute.MaxMana], attributeModifierValues.AddValue[EMyPlayerAttribute.MaxMana], attributeModifierValues.MultiplyValue[EMyPlayerAttribute.MaxMana], attributeModifierValues.DivideValue[EMyPlayerAttribute.MaxMana], attributeModifierValues.OverrideValue[EMyPlayerAttribute.MaxMana]);
                attributeValues.BaseValue[EMyPlayerAttribute.Speed] = ModifyValues(attributeValues.BaseValue[EMyPlayerAttribute.Speed], attributeModifierValues.AddValue[EMyPlayerAttribute.Speed], attributeModifierValues.MultiplyValue[EMyPlayerAttribute.Speed], attributeModifierValues.DivideValue[EMyPlayerAttribute.Speed], attributeModifierValues.OverrideValue[EMyPlayerAttribute.Speed]);
                ClampAttributes(ref attributeValues.BaseValue);
                attributeValues.CurrentValue = attributeValues.BaseValue;
                attributeValuesChunk[i] = attributeValues;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float ModifyValues(float Base, float Add, float Multiply, float Divide, float Override)
        {
            return math.select(((Base + Add) * (Multiply + 1)) / (Divide + 1), Override, Override != 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void ClampAttributes(ref MyPlayerAttributes Attributes)
        {
            Attributes[EMyPlayerAttribute.MaxHealth] = math.max(0, Attributes[EMyPlayerAttribute.MaxHealth]);
            Attributes[EMyPlayerAttribute.MaxMana] = math.max(0, Attributes[EMyPlayerAttribute.MaxMana]);
            Attributes[EMyPlayerAttribute.Speed] = math.max(0, Attributes[EMyPlayerAttribute.Speed]);
            Attributes[EMyPlayerAttribute.Health] = math.clamp(Attributes[EMyPlayerAttribute.Health], 0, Attributes[EMyPlayerAttribute.MaxHealth]);
            Attributes[EMyPlayerAttribute.Mana] = math.clamp(Attributes[EMyPlayerAttribute.Mana], 0, Attributes[EMyPlayerAttribute.MaxMana]);
        }
    }

}
