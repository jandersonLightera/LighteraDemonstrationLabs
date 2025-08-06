using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Utilities;

namespace ParallelismLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SimpleParallelism SimpleParallelismExamples = new SimpleParallelism();

            Console.WriteLine("Executing parallel tasks!");

            // The fibonnaci sequence is a classic example of the problems of parallelism vs time tradeoffs.
            // Observate what happens with different types of parallelism, versus sequential execution, at different values of 'n'.
            // You'll find that at lower values of 'n', parallelism is slower than sequential execution.
            // However: At higher values of 'n', parallelism is faster than sequential execution!
            // The reasons for this are nuanced, but reflect the importance of understanding when to use parallelism.
            //SimpleParallelismExamples.ExecuteFibonacciSequences(20);
            SimpleParallelismExamples.ExecuteFibonacciSequences(20);
        }

        internal class SimpleParallelism
        {
            Stopwatch Stopwatch = new Stopwatch();

            public IComparer<KeyValuePair<int, int>> KeyComparer  = Comparer<KeyValuePair<int, int>>.Create((x, y) => x.Key.CompareTo(y.Key));

            public void ExecuteFibonacciSequences(int fibN, bool illustrateRecursion = false)
            {
                // Lists to retain the results of the fibonacci calculations
                // We'll use this to compare the results of parallel vs sequential execution.
                List<KeyValuePair<int, int>> fibonacciPointsSeq = new List<KeyValuePair<int, int>>();
                List<KeyValuePair<int,int>> fibonacciPointsNaiveList = new List<KeyValuePair<int, int>>();
                List<KeyValuePair<int, int>> fibonacciPointsDynamicList = new List<KeyValuePair<int, int>>();

                ConcurrentDictionary<int, int> fibonacciPointsParallelNaiveConcDict = new ConcurrentDictionary<int, int>();
                ConcurrentDictionary<int, int> fibonacciPointsParallelDynamicConcDict = new ConcurrentDictionary<int, int>();

                Thread.CurrentThread.Name = "SimpleParallelism";

                Console.WriteLine("Generating fibonacci values using naive-paraellel, dynamic-parallel, and non-parallel methods!");
                fibonacciPointsSeq = GenerateFibonacciPointsSequential(fibN, illustrateRecursion);
                fibonacciPointsParallelNaiveConcDict = GenerateFibonacciPointsParallelNaive(fibN, illustrateRecursion);
                fibonacciPointsParallelDynamicConcDict = GenerateFibonacciPointsParallelDynamic(fibN, illustrateRecursion);

                // Sequential list output processing
                Console.WriteLine($"Listing GenerateFibonacciPointsSequential points ({fibonacciPointsSeq.Count}):");
                fibonacciPointsSeq.Sort(KeyComparer);
                fibonacciPointsSeq.ForEach(point => Console.WriteLine($"Sequential Fibonacci Point: {point}"));

                // Parallel Naive list output processing
                fibonacciPointsNaiveList = fibonacciPointsParallelNaiveConcDict.ToList();
                fibonacciPointsNaiveList.Sort(KeyComparer);

                Console.WriteLine($"Listing GenerateFibonacciPointsParallelNaive points ({fibonacciPointsNaiveList.Count}):");
                fibonacciPointsNaiveList.ForEach(point => Console.WriteLine($"Parallel Fibonacci Point: {point}"));

                // Parallel Dynamic list output processing
                fibonacciPointsDynamicList = fibonacciPointsParallelDynamicConcDict.ToList();
                fibonacciPointsDynamicList.Sort(KeyComparer);

                Console.WriteLine($"Listing GenerateFibonacciPointsParallelDynamic points ({fibonacciPointsDynamicList.Count}):");
                fibonacciPointsDynamicList.ForEach(point => Console.WriteLine($"Parallel Fibonacci Point: {point}"));

                // Render the fibonacci points as a graph
                RenderFibonacciGraph(fibonacciPointsParallelDynamicConcDict.ToDictionary());
            }

            // This 'naive' parallel version of the Fibonacci sequence generation.
            public ConcurrentDictionary<int, int> GenerateFibonacciPointsParallelNaive(int n, bool illustrateRecursion = false)
            {
                ConcurrentDictionary<int, int> points = new ConcurrentDictionary<int, int>();
                ParallelLoopResult loopResult;

                Console.WriteLine($"Begin: GenerateFibonacciPointsParallelNaive!");

                Stopwatch.Reset();
                Stopwatch.Start();

                loopResult = Parallel.For(0, n, () => 0, (i, loop, currentFibValue) =>
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is calculating Fibonacci({i})");

                    currentFibValue = GeneralFunctions.nthFibonacci(i, illustrateRecursion);

                    points.TryAdd(i,currentFibValue);
                    return currentFibValue;
                }, 
                currentFibValue => Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} calculated current fib value {currentFibValue}"));

                Stopwatch.Stop();

                Console.WriteLine($"Parallel Fibonacci generation took: {Stopwatch.ElapsedMilliseconds} ms");
                return points;
            }

            // This version is faster than the naive version because it uses a dynamic programming approach,
            // ...but is will stil be slower than the sequential version for lower values of 'n'!
            public ConcurrentDictionary<int, int> GenerateFibonacciPointsParallelDynamic(int n, bool illustrateRecursion = false)
            {
                ConcurrentDictionary<int, int> points = new ConcurrentDictionary<int, int>();
                ParallelLoopResult loopResult;

                Console.WriteLine($"Begin: GenerateFibonacciPointsParallelDynamic!");

                Stopwatch.Reset();
                Stopwatch.Start();

                loopResult = Parallel.For(0, n, () => 0, (i, loop, currentFibValue) =>
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is calculating Fibonacci({i})");

                    currentFibValue = GeneralFunctions.nthFibonacciDynamic(i, illustrateRecursion);

                    points.TryAdd(i,currentFibValue);
                    return currentFibValue;
                },
                currentFibValue => Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} calculated current fib value {currentFibValue}"));

                Stopwatch.Stop();

                Console.WriteLine($"Parallel Fibonacci generation took: {Stopwatch.ElapsedMilliseconds} ms");
                return points;
            }

            // The sequential version of the Fibonacci generation method is the fastest at low values of 'n',
            // ...but will be slower than the parallel versions at higher values of 'n'.
            public List<KeyValuePair<int, int>> GenerateFibonacciPointsSequential(int n, bool illustrateRecursion = false)
            {
                List<KeyValuePair<int, int>> points = new List<KeyValuePair<int, int>>();
                int currentFibValue = 0;

                Console.WriteLine($"Begin: GenerateFibonacciPointsSequential!");

                Stopwatch.Reset();
                Stopwatch.Start();

                for (int i = 0; i < n; i++)
                {
                    currentFibValue = GeneralFunctions.nthFibonacci(i,illustrateRecursion);
                    points.Add(KeyValuePair.Create(i,currentFibValue));
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} calculated Fibonacci({i}) = {currentFibValue}");
                }

                Stopwatch.Stop();
                Console.WriteLine($"Sequential Fibonacci generation took: {Stopwatch.ElapsedMilliseconds} ms");
                return points;
            }

            public void RenderFibonacciGraph(Dictionary<int,int> fibonacciPoints)
            {
                _2DRendering.Simple2DRenderer simple2DRenderer;
                Window rendererWindow;

                Thread thread = new Thread(() => {
                    simple2DRenderer = new _2DRendering.Simple2DRenderer();
                    simple2DRenderer.Render2DGraph(fibonacciPoints);
                    rendererWindow = new Window
                    {
                        Title = "Fibonacci Graph",
                        Width = 800,
                        Height = 600,
                        Content = simple2DRenderer
                    };

                    rendererWindow.ShowDialog();
                });

                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

            }   
        }
    }
}
