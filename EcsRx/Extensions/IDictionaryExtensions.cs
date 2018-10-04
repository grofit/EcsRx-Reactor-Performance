using System;
using System.Collections.Generic;

namespace EcsRx.Extensions
{
    public static class IDictionaryExtensions
    {       
        public static void RemoveAndDispose<T>(this IDictionary<T, IDisposable> disposables, T key)
        {
            if(!disposables.ContainsKey(key)){ return; }
            disposables[key].Dispose();
            disposables.Remove(key);
        }
        
        public static void RemoveAndDisposeAll<T>(this IDictionary<T, IDisposable> disposables)
        {
            disposables.ForEachRun(x => x.Value.Dispose());
            disposables.Clear();
        }
    }
}