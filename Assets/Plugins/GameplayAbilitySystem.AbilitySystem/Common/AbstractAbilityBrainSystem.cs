using System.Collections.Generic;
using GameplayAbilitySystem.GameplayTags;
using Unity.Entities;

namespace GameplayAbilitySystem.AbilitySystem
{
    public abstract class AbstractAbilityBrainSystem : SystemBase
    {
        public GameplayTag Tag;
        protected abstract EntityQuery Query { get; set; }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            Query = InitialiseEntityQuery();
            Query.SetSharedComponentFilter<AbilityBrainComponent.Component>(new AbilityBrainComponent.Component() { Tag = Tag });
        }
        public void SetQuery(List<ComponentType> query)
        {
            this.Query = GetEntityQuery(query.ToArray());
        }

        protected virtual EntityQuery InitialiseEntityQuery()
        {
            return Query;
        }

    }
}