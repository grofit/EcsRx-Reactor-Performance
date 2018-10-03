using System;

namespace EcsRx.Events
{
    public interface IEventSystem
    {
        void Publish<T>(T message);
        UniRx.IObservable<T> Receive<T>();
    }
}