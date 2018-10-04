using Assets.Reactor.Examples.Performance.Systems;
using EcsRx.Extensions;
using EcsRxPerformanceTests.Scenarios.Components;

namespace EcsRxPerformanceTests.Scenarios
{
    public class SimpleTestApplication : EcsRxApplication
    {
        protected override void ApplicationStarting()
        {
            SystemExecutor.AddSystem(new SomeSystem());
            SystemExecutor.AddSystem(new SomeSystem2());
            SystemExecutor.AddSystem(new SomeSystem3());
        }

        protected override void ApplicationStarted()
        {
            var defaultPool = EntityCollectionManager.GetCollection();

            // create 5k entities
            for (var i = 0; i < 10; i++)
            {
                var entity = defaultPool.CreateEntity();

                entity.AddComponent<Component1>();
                entity.AddComponent<Component2>();
                entity.AddComponent<Component3>();
            }
        }
    }
}
