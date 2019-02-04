using System.Reactive.Linq;
using Assets.Reactor.Examples.Performance.Systems;
using EcsRx.Examples.Application;
using EcsRx.Extensions;
using EcsRx.Infrastructure;
using EcsRx.Infrastructure.Dependencies;
using EcsRxPerformanceTests.Scenarios.Components;

namespace EcsRxPerformanceTests.Scenarios
{
    public class SimpleOptimizedTestApplication : EcsRxConsoleApplication
    {
        protected override void ApplicationStarted()
        {
            Observable.Start(() =>
            {
                var defaultPool = EntityCollectionManager.GetCollection();

                // create 5k entities
                for (var i = 0; i < 50000; i++)
                {
                    var entity = defaultPool.CreateEntity();

                    entity.AddComponents(new Component1(), new Component2(), new Component3());
                }
            }, EcsScheduler.EventLoopScheduler);
        }
    }
}