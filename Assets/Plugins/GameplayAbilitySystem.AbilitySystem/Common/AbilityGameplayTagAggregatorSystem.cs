using GameplayAbilitySystem.AttributeSystem;
using GameplayAbilitySystem.GameplayTags;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace GameplayAbilitySystem.AbilitySystem
{
    public class AbilityGameplayTagAggregatorSystem : SystemBase
    {
        NativeHashMap<GameplayTag, int> _abilityEntityIndex;
        EntityQuery abilitySpecQuery;
        EntityQuery activeAbilitiesQuery;
        NativeStream _abilityTagsCollection;

        protected override void OnCreate()
        {
            base.OnCreate();
            abilitySpecQuery = GetEntityQuery(ComponentType.ReadOnly<AbilityIdentifier.Component>(), ComponentType.ReadOnly<ActiveGrantedTagsComponent.Component>());

            // var job = new AbilityTagCollectionJob()
            // {
            //     abilityEntityType = GetEntityTypeHandle(),
            //     AbilityTagsCollectionProducer = _abilityTagsCollection.AsWriter(),
            //     activeGrantedTagsBufferHandle = GetBufferTypeHandle<ActiveGrantedTagsComponent.Component>()
            // }.ScheduleParallel(abilitySpecQuery);


            // Find all ability spec and create the NMHM

        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();

            var abilityCount = abilitySpecQuery.CalculateEntityCount();
            _abilityTagsCollection = new NativeStream(abilityCount, Allocator.Persistent);
            _abilityEntityIndex = new NativeHashMap<GameplayTag, int>(abilityCount, Allocator.Persistent);
            var job = new AbilityTagCollectionJob()
            {
                // abilityEntityType = GetEntityTypeHandle(),
                AbilityTagsCollectionProducer = _abilityTagsCollection.AsWriter(),
                activeGrantedTagsBufferHandle = GetBufferTypeHandle<ActiveGrantedTagsComponent.Component>(true),
                EntityIndexMap = _abilityEntityIndex.AsParallelWriter(),
                abilityIdType = GetComponentTypeHandle<AbilityIdentifier.Component>(true)
            };
            job.Run(abilitySpecQuery);
        }

        private struct AbilityTagCollectionJob : IJobChunk
        {
            [ReadOnly] public BufferTypeHandle<ActiveGrantedTagsComponent.Component> activeGrantedTagsBufferHandle;
            // [ReadOnly] public EntityTypeHandle abilityEntityType;
            [ReadOnly] public ComponentTypeHandle<AbilityIdentifier.Component> abilityIdType;
            public NativeStream.Writer AbilityTagsCollectionProducer;
            public NativeHashMap<GameplayTag, int>.ParallelWriter EntityIndexMap;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                var entityIndex = firstEntityIndex;
                BufferAccessor<ActiveGrantedTagsComponent.Component> activeGrantedTagsBuffer = chunk.GetBufferAccessor(activeGrantedTagsBufferHandle);
                // NativeArray<Entity> entityChunk = chunk.GetNativeArray(abilityEntityType);
                NativeArray<AbilityIdentifier.Component> abilityIdChunk = chunk.GetNativeArray(abilityIdType);

                for (var c = 0; c < chunk.Count; c++)
                {
                    AbilityTagsCollectionProducer.BeginForEachIndex(entityIndex);
                    // var entity = entityChunk[c];
                    GameplayTag tag = abilityIdChunk[c].Tag;
                    EntityIndexMap.TryAdd(tag, entityIndex);
                    DynamicBuffer<ActiveGrantedTagsComponent.Component> activeGrantedTags = activeGrantedTagsBuffer[c];
                    for (var i = 0; i < activeGrantedTags.Length; i++)
                    {
                        AbilityTagsCollectionProducer.Write<ActiveGrantedTagsComponent.Component>(activeGrantedTags[i]);
                    }
                    AbilityTagsCollectionProducer.EndForEachIndex();
                    entityIndex++;
                }
            }
        }

        protected override void OnUpdate()
        {


            // NativeMultiHashMap<Entity, Entity> spec = new NativeMultiHashMap<Entity, Entity>(mSpecQuery.CalculateEntityCount(), Allocator.Persistent);
            // var specWriter = spec.AsParallelWriter();
            // Entities.ForEach((Entity entity, in PlayerAttributeAuthoringScript.Component attributeEntity, in DynamicBuffer<AbilitySystemActorTags.Component> actorTagsBuffer) =>
            // {
            //     actorTagsBuffer.Clear();

            // }).ScheduleParallel();

            // Entities
            // .WithStoreEntityQueryInField(ref mSpecQuery)
            // .ForEach((Entity entity, in AbilitySpecEntityComponent abilitySpecEntity, in AbilityAttributeSourceComponent sourceEntity) =>
            // {

            //     specWriter.Add(sourceEntity, abilitySpecEntity);
            // }).ScheduleParallel();


            // Entities.ForEach((Entity entity, in PlayerAttributeAuthoringScript.Component attributeEntity, in DynamicBuffer<AbilitySystemActorTags.Component> actorTagsBuffer) =>
            // {
            //     var iterator = spec.GetValuesForKey(attributeEntity);
            // }).ScheduleParallel();


            NativeHashMap<GameplayTag, int> mAbilityEntityIndex = _abilityEntityIndex;
            NativeStream.Reader mAbilityTagsCollectionConsumer = _abilityTagsCollection.AsReader();


            Entities
                .ForEach((Entity entity, ref DynamicBuffer<AbilitySystemActorTags.Component> actorTagsBuffer, in DynamicBuffer<ActorActiveAbilitiesAuthoring.Component> activeAbilities) =>
                {
                    actorTagsBuffer.Clear();
                    for (var iAbility = 0; iAbility < activeAbilities.Length; iAbility++)
                    {
                        var activeAbility = activeAbilities[iAbility];
                        int streamPointer = mAbilityEntityIndex[activeAbility.Tag];
                        int tagsCount = mAbilityTagsCollectionConsumer.BeginForEachIndex(streamPointer);
                        for (var iTag = 0; iTag < tagsCount; iTag++)
                        {
                            ActiveGrantedTagsComponent.Component tag = mAbilityTagsCollectionConsumer.Read<ActiveGrantedTagsComponent.Component>();
                            actorTagsBuffer.Add(tag.Value);
                        }
                    }
                })
                .WithReadOnly(mAbilityTagsCollectionConsumer)
                .WithReadOnly(mAbilityEntityIndex)
                .ScheduleParallel();
            ;


        }
    }
}