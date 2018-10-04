using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using Reactor.Entities;
using Reactor.Extensions;
using Reactor.Pools;


namespace Reactor.Systems.Executor.Handlers
{
    public class EntityReactionSystemHandler : IEntityReactionSystemHandler
    {
        public IPoolManager PoolManager { get; }

        public EntityReactionSystemHandler(IPoolManager poolManager)
        {
            PoolManager = poolManager;
        }

        public IEnumerable<SubscriptionToken> Setup(IEntityReactionSystem system)
        {
            var entities = PoolManager.GetEntitiesFor(system.TargetGroup);
            return entities.ForEachRun(x => ProcessEntity(system, x));
        }

        public SubscriptionToken ProcessEntity(IEntityReactionSystem system, IEntity entity)
        {
            var observable = system.Impact(entity);

            var subscription = observable?.Subscribe(x => system.Reaction(entity)) ?? Disposable.Empty;

            return new SubscriptionToken(entity, subscription);
        }
    }
}