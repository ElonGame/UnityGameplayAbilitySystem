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
        var damagable = GetComponent<Damageable>();
        var _attributeEntity = MyAttributeUpdateSystem.CreatePlayerEntity(dstManager, new MyAttributeValues() { BaseValue = defaultAttributes });
        dstManager.SetName(_attributeEntity, $"{this.gameObject.name} - Attributes");
        return _attributeEntity;
    }
}
