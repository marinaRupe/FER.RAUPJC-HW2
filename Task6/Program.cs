using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Task6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Example1();
            Example2();
            Example3();
            Example4();
            Example5();
            Example6();
            Example7();
        }

        public static void Example1()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            LongOperation("A");
            LongOperation("B");
            LongOperation("C");
            LongOperation("D");
            LongOperation("E");
            stopwatch.Stop();

            Console.WriteLine("Synchronous  long  operation  calls  finished  {0} sec.",
                stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine();
        }

        public static void Example2()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            Parallel.Invoke(() => LongOperation("A"),
            () => LongOperation("B"),
            () => LongOperation("C"),
            () => LongOperation("D"),
            () => LongOperation("E"));

            stopwatch.Stop();

            Console.WriteLine("Parallel  long  operation  calls  finished  {0} sec.",
                stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine();
        }

        public static void Example3()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            Parallel.For(0, 1000, (i) =>
            {
                var x = 2;
                var y = 2;
                var sum = x + y;
            });

            stopwatch.Stop();

            Console.WriteLine("Parallel  calls  finished  {0} ms.",
                stopwatch.Elapsed.TotalMilliseconds);

            stopwatch.Restart();

            for (int i = 0; i < 1000; i++)
            {
                int x = 2;
                int y = 2;
                int sum = x + y;
            }

            stopwatch.Stop();

            Console.WriteLine("Sync  operation  calls  finished  {0} ms.",
                stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine();
        }


        public static void Example4()
        {
            int counter = 0;
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                counter += 1;
            });

            Console.WriteLine("Counter  should  be 100.  Counter  is {0}", counter);
            Console.WriteLine();
        }

        public static void Example5()
        {
            int counter = 0;
            object objectUsedForLock = new object();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                lock (objectUsedForLock)
                {
                    counter += 1;
                }
            });

            Console.WriteLine("Counter  should  be 100.  Counter  is {0}", counter);
            Console.WriteLine();
        }

        public static void Example6()
        {
            List<int> results = new List<int>();
            object objectUsedForLock = new object();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                lock (objectUsedForLock)
                {
                    results.Add(i * i);
                }
            });
            Console.WriteLine("Bag  length  should  be 100.  Length  is {0}", results.Count);
            Console.WriteLine();
        }

        public static void Example7()
        {
            ConcurrentBag<int> iterations = new ConcurrentBag<int>();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                iterations.Add(i);
            });

            Console.WriteLine("Bag  length  should  be 100.  Length  is {0}", iterations.Count);
            Console.WriteLine();
        }

        public static void LongOperation(string taskName)
        {
            Thread.Sleep(1000);
            Console.WriteLine("{0}  Finished. Executing  Thread: {1}", taskName,
            Thread.CurrentThread.ManagedThreadId);
        }
    }
}
