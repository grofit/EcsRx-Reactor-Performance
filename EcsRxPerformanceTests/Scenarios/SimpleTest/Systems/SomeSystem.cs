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
    public class SomeSystem : IReactToEntitySystem
    {
        public IGroup Group { get; } = 
            new GroupBuilder()
                .WithComponent<Component1>()
                .WithComponent<Component2>()
                .WithComponent<Component3>()
                .Build();

        private Random _random = new Random();

        public IObservable<IEntity> ReactToEntity(IEntity entity)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(16.6), Scheduler.CurrentThread).Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            if (_random.Next(0, 1) == 0)
            {
                entity.RemoveComponent<Component2>();
                entity.AddComponents(new Component4());
            }
            else
            {
                entity.RemoveComponent<Component3>();
                entity.AddComponents(new Component5());
            }
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


        public IObservable<IEntity> ReactToEntity(IEntity entity)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(16.6), Scheduler.CurrentThread).Select(x => entity);
        }

        public void Process(IEntity entity)
        {
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


        public IObservable<IEntity> ReactToEntity(IEntity entity)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(16.6), Scheduler.CurrentThread).Select(x => entity);
        }

        public void Process(IEntity entity)
        {
            entity.RemoveComponent<Component5>();
            entity.AddComponents(new Component3());
        }
    }
}