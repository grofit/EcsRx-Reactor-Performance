using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Systems;
using EcsRxPerformanceTests.Scenarios.Components;


namespace Assets.Reactor.Examples.Performance.Systems
{
    public static class EcsScheduler
    {
        public static EventLoopScheduler EventLoopScheduler = new EventLoopScheduler();
    }

    public class SomeSystem : IReactToEntitySystem
    {
        public IGroup Group { get; } = 
            new GroupBuilder()
                .WithComponent<Component1>()
                .WithComponent<Component2>()
                .WithComponent<Component3>()
                .Build();

        private bool _systemToggle;
        private static readonly TimeSpan Ts = TimeSpan.FromMilliseconds(16.6);

        public IObservable<IEntity> ReactToEntity(IEntity entity)
        {
            return Observable.Interval(Ts, EcsScheduler.EventLoopScheduler).Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            var component1 = entity.GetComponent<Component1>();
            var component2 = entity.GetComponent<Component2>();
            var component3 = entity.GetComponent<Component3>();
            if(component1 == null || component2 == null || component3 == null)
            { throw new Exception("Not all components are available"); }
            
            if (_systemToggle)
            {
                entity.RemoveComponent<Component2>();
                entity.AddComponents(new Component4());
            }
            else
            {
                entity.RemoveComponent<Component3>();
                entity.AddComponents(new Component5());
            }

            _systemToggle = !_systemToggle;
        }
    }

    public class SomeSystem2 : IReactToEntitySystem
    {
        public IGroup Group { get; } =
            new GroupBuilder()
                .WithComponent<Component1>()
                .WithComponent<Component3>()
                .WithComponent<Component4>()
                .Build();

        private static readonly TimeSpan Ts = TimeSpan.FromMilliseconds(16.6);

        public IObservable<IEntity> ReactToEntity(IEntity entity)
        {
            return Observable.Interval(Ts, EcsScheduler.EventLoopScheduler).Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            var component1 = entity.GetComponent<Component1>();
            var component2 = entity.GetComponent<Component3>();
            var component3 = entity.GetComponent<Component4>();
            if(component1 == null || component2 == null || component3 == null)
            { throw new Exception("Not all components are available"); }
            
            entity.RemoveComponent<Component4>();
            entity.AddComponents(new Component2());
        }
    }

    public class SomeSystem3 : IReactToEntitySystem
    {
        public IGroup Group { get; } =
            new GroupBuilder()
                .WithComponent<Component1>()
                .WithComponent<Component2>()
                .WithComponent<Component5>()
                .Build();

        private static readonly TimeSpan Ts = TimeSpan.FromMilliseconds(16.6);

        public IObservable<IEntity> ReactToEntity(IEntity entity)
        {
            return Observable.Interval(Ts, EcsScheduler.EventLoopScheduler).Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            var component1 = entity.GetComponent<Component1>();
            var component2 = entity.GetComponent<Component2>();
            var component3 = entity.GetComponent<Component5>();
            if(component1 == null || component2 == null || component3 == null)
            { throw new Exception("Not all components are available"); }
            
            entity.RemoveComponent<Component5>();
            entity.AddComponents(new Component3());
        }
    }
}