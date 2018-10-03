using System.Collections.Generic;
using System.Linq;
using Reactor.Entities;
using Reactor.Systems.Executor;

namespace Reactor.Extensions
{
    public static class IListExtensions
    {
        public static IEnumerable<SubscriptionToken> GetTokensFor(this IList<SubscriptionToken> subscriptionTokens,
            IEntity entity)
        {
            return subscriptionTokens.Where(x => x.AssociatedEntity == entity);
        }
    }
}