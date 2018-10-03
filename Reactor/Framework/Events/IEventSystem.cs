using System;
using UniRx;

namespace Reactor.Events
{
    public interface IEventSystem
    {
        void Publish<T>(T message);
        UniRx.IObservable<T> Receive<T>();
    }
}