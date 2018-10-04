using System.Reactive.Linq;
using Assets.Reactor.Examples.Performance.Components;
using Assets.Reactor.Examples.Performance.Systems;
using Reactor.Systems.Executor.Handlers;
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

            Observable.Start(() =>
            {
                // create 5k entities
                for (var i = 0; i < 50000; i++)
                {
                    //Console.WriteLine(i + " " + Thread.CurrentThread.ManagedThreadId);
                    var entity = defaultPool.CreateEntity();

                    entity.AddComponent<Component1>();
                    entity.AddComponent<Component2>();
                    entity.AddComponent<Component3>();
                }

            }, Test.EventLoopScheduler);
        }
    }
}
