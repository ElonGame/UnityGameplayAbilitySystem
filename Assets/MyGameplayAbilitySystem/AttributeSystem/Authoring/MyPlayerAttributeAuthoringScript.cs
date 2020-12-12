using Gamekit3D;
using MyGameplayAbilitySystem;
using Unity.Entities;
using UnityEngine;

public class MyPlayerAttributeAuthoringScript : MonoBehaviour, IConvertGameObjectToEntity
{
    public EntityManager dstManager { get; private set; }
    public Entity attributeEntity { get; private set; }

    [SerializeField]
    private MyPlayerAttributes defaultAttributes;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        attributeEntity = InitialiseAttributeEntity(dstManager);
        this.dstManager = dstManager;
    }

    public Entity InitialiseAttributeEntity(EntityManager dstManager)
    {
        var damagable = GetComponent<Damageable>();
        var _attributeEntity = MyAttributeUpdateSystem.CreatePlayerEntity(dstManager, new MyAttributeValues() { BaseValue = defaultAttributes });
        dstManager.SetName(_attributeEntity, $"{this.gameObject.name} - Attributes");
        return _attributeEntity;
    }
}

public abstract class AbilitySystemComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public Entity ActorAttributeEntity;
    public Entity AbilitySystemActorEntity;
    protected abstract Entity RegisterActorAttributeEntity();

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        AbilitySystemActorEntity = entity;
        ActorAttributeEntity = RegisterActorAttributeEntity();
    }
}
