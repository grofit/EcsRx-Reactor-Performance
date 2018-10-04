using System;
using Assets.Reactor.Examples.Performance;

namespace ReactorPerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var application = new SimpleTestApplication();
            application.StartApplication();
            //Console.ReadKey();
        }
    }
}