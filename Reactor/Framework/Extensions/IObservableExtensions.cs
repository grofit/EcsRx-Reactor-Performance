﻿using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Reactor.Extensions
{
    public static class IObservableExtensions
    {
        public static IObservable<Unit> AsTrigger<T>(this IObservable<T> observable)
        { return observable.Select(x => Unit.Default); } 
    }
}