using System;
using Reactor.Groups;
using Reactor.Pools;


namespace Reactor.Systems.Executor.Handlers
{
    public class GroupReactionSystemHandler : IGroupReactionSystemHandler
    {
        public IPoolManager PoolManager { get; }

        public GroupReactionSystemHandler(IPoolManager poolManager)
        {
            PoolManager = poolManager;
        }

        public SubscriptionToken Setup(IGroupReactionSystem system)
        {
            var groupAccessor = PoolManager.CreateGroupAccessor(system.TargetGroup);
            var obs = system.Impact(groupAccessor);

            var subscription = obs.Subscribe<IGroupAccessor>(accessor =>
            {
                foreach (var entity in accessor.Entities)
                {
                    system.Reaction(entity);
                }
            });

            return new SubscriptionToken(null, subscription);
        }
    }
}
