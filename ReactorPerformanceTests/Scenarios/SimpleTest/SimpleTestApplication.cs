using Assets.Reactor.Examples.Performance.Components;
using Assets.Reactor.Examples.Performance.Systems;
using Reactor.Systems.Executor;
using Reactor.Unity;

namespace Assets.Reactor.Examples.Performance
{
    public class SimpleTestApplication : ReactorApplication
    {
        protected override void ApplicationStarting()
        {
            SystemExecutor.AddSystem(new SomeSystem());
            SystemExecutor.AddSystem(new SomeSystem2());
            SystemExecutor.AddSystem(new SomeSystem3());
        }

        protected override void ApplicationStarted()
        {
            var defaultPool = PoolManager.GetPool();

            // create 5k entities
            for (var i = 0; i < 4000; i++)
            {
                var entity = defaultPool.CreateEntity();

                entity.AddComponent<Component1>();
                entity.AddComponent<Component2>();
                entity.AddComponent<Component3>();
            }
        }
    }
}
