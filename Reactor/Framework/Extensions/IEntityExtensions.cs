using System;
using System.Linq;
using Reactor.Entities;
using Reactor.Groups;
using UniRx;

namespace Reactor.Extensions
{
    public static class IEntityExtensions
    {
        public static UniRx.IObservable<IEntity> ObserveProperty<T>(this IEntity entity, Func<IEntity, T> propertyLocator)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(16.6)) // Just faking 60 updates per second
                .DistinctUntilChanged(x => propertyLocator(entity))
                .Select(x => entity);
        }

        public static UniRx.IObservable<IEntity> WaitForPredicateMet(this IEntity entity, Predicate<IEntity> predicate)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(16.6))
                .First(x => predicate(entity))
                .Select(x => entity);
        }

        public static bool MatchesGroup(this IEntity entity, IGroup group)
        {
            return entity.HasComponents(group.TargettedComponents.ToArray());
        }
    }
}