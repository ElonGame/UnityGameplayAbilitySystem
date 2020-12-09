using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{
    public abstract class ConvertToSpec : MonoBehaviour, IConvertGameObjectToEntity
    {
        public abstract void CreateSpec(Entity entity, EntityManager dstManager);
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            CreateSpec(entity, dstManager);
        }

        public abstract ComponentType GetComponentType();
    }
}