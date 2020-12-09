using GameplayAbilitySystem.AbilitySystem;
using GameplayAbilitySystem.GameplayTags;
using Unity.Entities;
using UnityEngine;

public class DoDamageTest : MonoBehaviour
{
    public MyPlayerAttributeAuthoringScript Character;
    public AbilityRegistryScriptableObject AbilityRegistery;
    public GameplayTagScriptableObject Tag;
    public float AddValue;
    public bool Execute;

    void Update()
    {
        if (Execute)
        {
            var abilityEntity = AbilityRegistery.Get(Tag.Tag);


            // var dstManager = Character.dstManager;
            // var archetype = dstManager.CreateArchetype(new MySimpleGameplayEffectSpec().GetComponents());
            // var sourceAttributes = dstManager.GetComponentData<AttributeValues>(Character.attributeEntity);
            // var entity = dstManager.CreateEntity(archetype);
            // dstManager.SetComponentData(entity, TimeDurationComponent.New(0, 10));
            // dstManager.SetComponentData(entity, new PlayerAttributeCollectionComponent()
            // {
            //     Source = sourceAttributes
            // });
            // dstManager.SetComponentData(entity, new GameplayEffectContextComponent()
            // {
            //     Source = Character.attributeEntity,
            //     Target = Character.attributeEntity
            // });


            // TODO: CREATE DYNAMIC BUFFER OF ATTRIBUTE MODIFIERS ATTACHED TO THE GAMEPLAYEFFECT SPEC.  THE DYNAMIC BUFFER CREATE THE ATTRIBUTE MODIFIERS
            // ALTERNATIVELY, HANDLE ATTRIBUTE CALCULATION ON MAIN THREAD (SOUNDS LIKE A BETTER APPROACH BECAUSE THIS CAN BE AS COMPLEX AS REQUIRED)

            // MyInstantAttributeUpdateSystem.CreateAttributeModifier(dstManager, new MyInstantGameplayAttributeModifier()
            // {
            //     Attribute = EMyPlayerAttribute.Health,
            //     Operator = EMyAttributeModifierOperator.Add,
            //     Value = AddValue
            // }, new GameplayAbilitySystem.AttributeSystem.GameplayEffectContextComponent()
            // {
            //     Source = Entity.Null,
            //     Target = Character.attributeEntity
            // });

            Execute = false;
        }
    }

}