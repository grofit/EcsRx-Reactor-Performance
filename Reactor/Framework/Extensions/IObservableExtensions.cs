using System;
using UniRx;

namespace Reactor.Extensions
{
    public static class IObservableExtensions
    {
        public static UniRx.IObservable<Unit> AsTrigger<T>(this UniRx.IObservable<T> observable)
        { return observable.Select(x => Unit.Default); } 
    }
}