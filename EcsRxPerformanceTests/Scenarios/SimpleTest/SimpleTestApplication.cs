using System.Reactive.Linq;
using Assets.Reactor.Examples.Performance.Systems;
using EcsRx.Examples.Application;
using EcsRx.Extensions;
using EcsRx.Infrastructure;
using EcsRx.Infrastructure.Dependencies;
using EcsRxPerformanceTests.Scenarios.Components;

namespace EcsRxPerformanceTests.Scenarios
{
    public class SimpleTestApplication : EcsRxConsoleApplication
    {
        protected override void ApplicationStarted()
        {
            var defaultPool = EntityCollectionManager.GetCollection();

            Observable.Start(() =>
            {
                // create 5k entities
                for (var i = 0; i < 50000; i++)
                {
                    var entity = defaultPool.CreateEntity();

                    entity.AddComponent<Component1>();
                    entity.AddComponent<Component2>();
                    entity.AddComponent<Component3>();
                }

            }, EcsScheduler.EventLoopScheduler);
        }
    }
}
