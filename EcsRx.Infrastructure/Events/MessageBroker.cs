using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace EcsRx.Infrastructure.Events
{
    public interface IMessageBroker
    {
        void Publish<T>(T message);
        IObservable<T> Receive<T>();
        void Dispose();
    }

    public class MessageBroker : IMessageBroker, IDisposable
    {
        /// <summary>
        /// MessageBroker in Global scope.
        /// </summary>
        public static readonly IMessageBroker Default = new MessageBroker();

        bool isDisposed = false;
        readonly Dictionary<Type, object> notifiers = new Dictionary<Type, object>();

        public void Publish<T>(T message)
        {
            object notifier;
            lock (notifiers)
            {
                if (isDisposed) return;

                if (!notifiers.TryGetValue(typeof(T), out notifier))
                {
                    return;
                }
            }
            ((ISubject<T>)notifier).OnNext(message);
        }

        public IObservable<T> Receive<T>()
        {
            object notifier;
            lock (notifiers)
            {
                if (isDisposed) throw new ObjectDisposedException("MessageBroker");

                if (!notifiers.TryGetValue(typeof(T), out notifier))
                {
                    var n = new Subject<T>();
                    notifier = n;
                    notifiers.Add(typeof(T), notifier);
                }
            }
            return ((IObservable<T>)notifier);
        }

        public void Dispose()
        {
            lock (notifiers)
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    notifiers.Clear();
                }
            }
        }
    }
}