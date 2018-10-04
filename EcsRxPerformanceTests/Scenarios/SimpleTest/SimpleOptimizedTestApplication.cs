using System.Reactive.Linq;
using Assets.Reactor.Examples.Performance.Systems;
using EcsRxPerformanceTests.Scenarios.Components;

namespace EcsRxPerformanceTests.Scenarios
{
    public class SimpleOptimizedTestApplication : EcsRxApplication
    {
        protected override void ApplicationStarting()
        {
            SystemExecutor.AddSystem(new SomeSystem());
            SystemExecutor.AddSystem(new SomeSystem2());
            SystemExecutor.AddSystem(new SomeSystem3());
        }

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