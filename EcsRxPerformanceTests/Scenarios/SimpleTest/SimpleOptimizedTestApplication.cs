using Assets.Reactor.Examples.Performance.Systems;
using EcsRxPerformanceTests.Scenarios.Components;

namespace EcsRxPerformanceTests.Scenarios
{
    public class SimpleOptimizedTestApplication : EcsRxApplication
    {
        protected override void ApplicationStarting()
        {
            SystemExecutor.AddSystem(new SomeSystem());
        }

        protected override void ApplicationStarted()
        {
            var defaultPool = EntityCollectionManager.GetCollection();

            // create 5k entities
            for (var i = 0; i < 4000; i++)
            {
                var entity = defaultPool.CreateEntity();

                entity.AddComponents(new Component1(), new Component2(), new Component3());
            }
        }
    }
}