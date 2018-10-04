using Reactor.Entities;
using Reactor.Events;
using Reactor.Groups;
using Reactor.Pools;
using Reactor.Systems.Executor;
using Reactor.Systems.Executor.Handlers;


namespace Reactor.Unity
{
    public abstract class ReactorApplication
    {
        public ISystemExecutor SystemExecutor { get; private set; }
        public IPoolManager PoolManager { get; private set; }
        public IEventSystem EventSystem { get; private set; }
        public ICoreManager CoreManager { get; private set; }
        
        protected ReactorApplication()
        {
            var messageBroker = new MessageBroker();
            EventSystem = new EventSystem(messageBroker);
            SystemExecutor = new SystemExecutor(EventSystem);

            var entityFactory = new DefaultEntityFactory();
            var entityIndexPool = new EntityIndexPool();
            var poolFactory = new DefaultPoolFactory(entityFactory, EventSystem, entityIndexPool, SystemExecutor);
            var groupAccessorFactory = new DefaultGroupAccessorFactory(EventSystem);
            PoolManager = new PoolManager(EventSystem, poolFactory, groupAccessorFactory);

            var entitySytemHandler = new EntityReactionSystemHandler(PoolManager);
            var groupSystemHandler = new GroupReactionSystemHandler(PoolManager);
            var setupSystemHandler = new SetupSystemHandler(PoolManager);
            var interactSystemHandler = new InteractReactionSystemHandler(PoolManager);
            var manualSystemHandler = new ManualSystemHandler(PoolManager);
            var systemHandlerManager = new SystemHandlerManager(entitySytemHandler, groupSystemHandler, setupSystemHandler, interactSystemHandler, manualSystemHandler);
            CoreManager = new CoreManager(PoolManager, systemHandlerManager, SystemExecutor);
        }

        public void StartApplication()
        {
            ApplicationStarting();

            SystemExecutor.Start(CoreManager);
            ApplicationStarted();
        }

        protected virtual void ApplicationStarting() { }
        protected abstract void ApplicationStarted();    
        
    }
}
