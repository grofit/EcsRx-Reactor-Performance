using System;
using System.Collections.Generic;
using System.Linq;
using EcsRx.Attributes;
using EcsRx.Collections;
using EcsRx.Entities;
using EcsRx.Executor.Handlers;
using EcsRx.Extensions;
using EcsRx.Groups;


namespace EcsRx.Systems.Handlers
{
    [Priority(6)]
    public class ReactToGroupSystemHandler : IConventionalSystemHandler
    {
        public readonly IEntityCollectionManager EntityCollectionManager;       
        public readonly IDictionary<ISystem, IDisposable> _systemSubscriptions;
        
        public ReactToGroupSystemHandler(IEntityCollectionManager entityCollectionManager)
        {
            EntityCollectionManager = entityCollectionManager;
            _systemSubscriptions = new Dictionary<ISystem, IDisposable>();
        }

        public bool CanHandleSystem(ISystem system)
        { return system is IReactToGroupSystem; }

        public void SetupSystem(ISystem system)
        {
            var observableGroup = EntityCollectionManager.GetObservableGroup(system.Group);
            var hasEntityPredicate = system.Group is IHasPredicate;
            var castSystem = (IReactToGroupSystem)system;
            var reactObservable = castSystem.ReactToGroup(observableGroup);

            if (!hasEntityPredicate)
            {
                var noPredicateSub = reactObservable.Subscribe(x => ExecuteForGroup(x, castSystem));
                _systemSubscriptions.Add(system, noPredicateSub);
                return;
            }

            var groupPredicate = system.Group as IHasPredicate;
            var subscription = reactObservable.Subscribe(x => ExecuteForGroup(x.Where(groupPredicate.CanProcessEntity), castSystem));
            _systemSubscriptions.Add(system, subscription);
        }

        private static void ExecuteForGroup(IEnumerable<IEntity> entities, IReactToGroupSystem castSystem)
        {
            foreach(var entity in entities)
            { castSystem.Process(entity); }
        }

        public void DestroySystem(ISystem system)
        { _systemSubscriptions.RemoveAndDispose(system); }

        public void Dispose()
        {
            _systemSubscriptions.Values.DisposeAll();
            _systemSubscriptions.Clear();
        }
    }
}
