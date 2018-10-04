using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Assets.Reactor.Examples.Performance.Components;
using Reactor.Entities;
using Reactor.Groups;
using Reactor.Systems;
using Reactor.Systems.Executor.Handlers;


namespace Assets.Reactor.Examples.Performance.Systems
{
    public static class EcsScheduler
    {
        public static EventLoopScheduler EventLoopScheduler = new EventLoopScheduler();
    }

    public class SomeSystem : IEntityReactionSystem
    {
        public IGroup TargetGroup { get; } = 
            new GroupBuilder()
                .WithComponent<Component1>()
                .WithComponent<Component2>()
                .WithComponent<Component3>()
                .Build();

        private readonly Random _random = new Random();
        private static readonly TimeSpan Ts = TimeSpan.FromMilliseconds(16.6);

        public IObservable<IEntity> Impact(IEntity entity)
        {
            return Observable.Timer(Ts, EcsScheduler.EventLoopScheduler).Select(x => entity);
        }

        public void Reaction(IEntity entity)
        {
            //Console.Write(Thread.CurrentThread.ManagedThreadId);
            if (_random.Next(0, 2) == 0)
            {
                entity.RemoveComponent<Component2>();
                entity.AddComponent(new Component4());
            }
            else
            {
                entity.RemoveComponent<Component3>();
                entity.AddComponent(new Component5());
            }
        }
    }

    public class SomeSystem2 : IEntityReactionSystem
    {
        public IGroup TargetGroup { get; } =
            new GroupBuilder()
                .WithComponent<Component1>()
                .WithComponent<Component3>()
                .WithComponent<Component4>()
                .Build();

        private static readonly TimeSpan Ts = TimeSpan.FromMilliseconds(16.6);

        public IObservable<IEntity> Impact(IEntity entity)
        {
            return Observable.Timer(Ts, EcsScheduler.EventLoopScheduler).Select(x => entity);
        }

        public void Reaction(IEntity entity)
        {
            //Console.Write(Thread.CurrentThread.ManagedThreadId);
            entity.RemoveComponent<Component4>();
            entity.AddComponent(new Component2());
        }
    }

    public class SomeSystem3 : IEntityReactionSystem
    {
        public IGroup TargetGroup { get; } =
            new GroupBuilder()
                .WithComponent<Component1>()
                .WithComponent<Component2>()
                .WithComponent<Component5>()
                .Build();

        private static readonly TimeSpan Ts = TimeSpan.FromMilliseconds(16.6);

        public IObservable<IEntity> Impact(IEntity entity)
        {
            return Observable.Timer(Ts, EcsScheduler.EventLoopScheduler).Select(x => entity);
        }

        public void Reaction(IEntity entity)
        {
            //Console.Write(Thread.CurrentThread.ManagedThreadId);
            entity.RemoveComponent<Component5>();
            entity.AddComponent(new Component3());
        }
    }
}