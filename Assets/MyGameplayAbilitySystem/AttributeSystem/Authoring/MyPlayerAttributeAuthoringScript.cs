using Gamekit3D;
using GameplayAbilitySystem.AttributeSystem;
using MyGameplayAbilitySystem;
using Unity.Entities;
using UnityEngine;

public class MyPlayerAttributeAuthoringScript : PlayerAttributeAuthoringScript
{
    [SerializeField]
    private MyPlayerAttributes defaultAttributes;
    public override Entity InitialiseAttributeEntity(EntityManager dstManager)
    {
        var _attributeEntity = MyAttributeUpdateSystem.CreatePlayerEntity(dstManager, new MyAttributeValues() { BaseValue = defaultAttributes });
        dstManager.SetName(_attributeEntity, $"{this.gameObject.name} - Attributes");
        this.ActorAttributeEntity = _attributeEntity;
        return _attributeEntity;
    }

}
