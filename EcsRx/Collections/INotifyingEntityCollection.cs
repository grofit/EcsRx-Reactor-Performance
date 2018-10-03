using EcsRx.Events;

namespace EcsRx.Collections
{
    public interface INotifyingEntityCollection
    {
        UniRx.IObservable<CollectionEntityEvent> EntityAdded { get; }
        UniRx.IObservable<CollectionEntityEvent> EntityRemoved { get; }

        UniRx.IObservable<ComponentsChangedEvent> EntityComponentsAdded { get; }
        UniRx.IObservable<ComponentsChangedEvent> EntityComponentsRemoving { get; }
        UniRx.IObservable<ComponentsChangedEvent> EntityComponentsRemoved { get; }
    }
}