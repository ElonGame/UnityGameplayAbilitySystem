using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AttributeSystem
{
    [DisallowMultipleComponent]
    public abstract class PlayerAttributeAuthoringScript : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Entity ActorAttributeEntity { get; protected set; }
        public abstract Entity InitialiseAttributeEntity(EntityManager dstManager);

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            ActorAttributeEntity = InitialiseAttributeEntity(dstManager);
            dstManager.AddComponent<Component>(entity);
            dstManager.SetComponentData<Component>(entity, new Component() { ActorAttributeEntity = ActorAttributeEntity });
        }

        public struct Component : IComponentData
        {
            public Entity ActorAttributeEntity;
        }
    }
}