using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Reactor.Systems;


namespace Reactor.Extensions
{
    public static class IManualSystemExtensions
    {
        public static IObservable<long> WaitForScene(this IManualSystem manualSystem)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(16.6), Scheduler.CurrentThread).FirstAsync();
        }  
    }
}