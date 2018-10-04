using System;
using EcsRxPerformanceTests.Scenarios;

namespace EcsRxPerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //var application = new SimpleTestApplication();
            var application = new SimpleOptimizedTestApplication();
            
            application.StartApplication();

            //Console.ReadKey();
        }
    }
}