using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities
{
    public static class SimpleFunctions
    {

        public static async Task<string> TimeWasterAsync1()
        {
            Console.WriteLine($"TimeWasterAsync1 wasting time for 10 seconds...\n");

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                Console.WriteLine($"TimeWasterAsync1: {i + 1} seconds elapsed.\n");
            }

            return "TimeWasterAsync1 Completed!\n";
        }

        public static async Task<string> TimeWasterAsync2()
        {
            Console.WriteLine($"TimeWasterAsync2 wasting time for 5 seconds...\n");

            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(1000);
                Console.WriteLine($"TimeWasterAsync2: {i + 1} seconds elapsed.\n");
            }

            return "TimeWasterAsync2 Completed!\n";
        }

        public static string TimeWaster1()
        {
            Console.WriteLine($"TimeWaster1 wasting time for 10 seconds...\n");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"TimeWaster1: {i + 1} seconds elapsed.\n");
            }

            return "TimeWaster1 Completed!\n";
        }

        public static string TimeWaster2()
        {
            Console.WriteLine($"TimeWaster2 wasting time for 5 seconds...\n");

            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"TimeWaster2: {i + 1} seconds elapsed.\n");
            }

            return "TimeWaster2 Completed!\n";
        }

        public static string TimeWasterN(int n)
        {
            Console.WriteLine($"TimeWasterN wasting time for {n} seconds...\n");

            for (int i = 0; i < n; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"TimeWasterN: {i + 1} seconds elapsed.\n");
            }

            return "TimeWasterN Completed!\n";
        }

        public static void Greetings(string name)
        {
            Console.WriteLine($"Hello {name}!\n");
        }

        public static void Goodbye(string name)
        {
            Console.WriteLine($"Goodbye {name}!\n");
        }

        public static string FormalName(string name)
        {
            return $"The Honorable {name}\n";
        }

        public static string GreetTheBIGDAWG(string name)
        {
            return $"Hello, BIG DAWG {name}!\n";
        }

        public static string FarewellToTheBIGDAWG(string name)
        {
            return $"Farewell, BIG DAWG {name}!\n";
        }

    }

    public static class SimpleDelegates
    {
        public static Action<string> CallYouAGoodFriendAction = (name) => Console.WriteLine($"Hello, {name} you are a good friend!\n");

        public static Func<string, string> CallYouAGoodFriendFunc = (name) => { return $"You are a good friend {name}."; };
    }

    public static class GeneralFunctions
    {
        public static ConcurrentDictionary<int,int> FibonacciCache = new ConcurrentDictionary<int, int>();

        // Typical fibonnaci number function using recursion. Taken from GeeksForGeeks.
        // https://www.geeksforgeeks.org/dsa/program-for-nth-fibonacci-number/
        public static int nthFibonacci(int n, bool illustrateRecursion = false)
        {
            if (illustrateRecursion)
            {
                Console.WriteLine($"nthFibonacci: Calculating Fibonacci({n})");
            }
            // Base case: if n is 0 or 1, return n
            if (n <= 1)
            {
                return n;
            }
            // Recursive case: sum of the two preceding
            // Fibonacci numbers

            if (illustrateRecursion)
            {
                Console.WriteLine($"nthFibonacci: Calculating Fibonacci({n-1}) and Fibonacci({n - 2})");
            }

            return nthFibonacci(n - 1,illustrateRecursion) + nthFibonacci(n - 2, illustrateRecursion);
        }

        // Dynamic programming approach to calculate nth Fibonacci number.
        // Note that we retain a cache of previously calculated Fibonacci numbers!
        public static int nthFibonacciDynamic(int n, bool illustrateRecursion = false)
        {
            if(n <= 1)
            {
                return n;
            } else
            {
                if(FibonacciCache.ContainsKey(n))
                {
                    if (illustrateRecursion)
                    {
                        Console.WriteLine($"nthFibonacciDynamic: Found Fibonacci({n}) in cache!");
                    }
                    return FibonacciCache[n];
                }
                else
                {
                    if (illustrateRecursion)
                    {
                        Console.WriteLine($"nthFibonacciDynamic: Did not find Fibonacci({n}) in cache!\n" +
                            $"Calculating fib({n-1}) and fib({n-2})");
                    }

                    int fibValue = nthFibonacciDynamic(n - 1) + nthFibonacciDynamic(n - 2);
                    FibonacciCache.TryAdd(n, fibValue);
                    return fibValue;
                }
            }
        }
    }

}
