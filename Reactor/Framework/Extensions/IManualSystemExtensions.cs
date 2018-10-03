using System;
using Reactor.Systems;
using UniRx;

namespace Reactor.Extensions
{
    public static class IManualSystemExtensions
    {
        public static UniRx.IObservable<long> WaitForScene(this IManualSystem manualSystem)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(16.6)).First();
        }  
    }
}