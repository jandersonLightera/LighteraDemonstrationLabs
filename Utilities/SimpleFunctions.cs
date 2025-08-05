using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities
{
    public static class SimpleFunctions
    {
        public static async Task<string> TimeWaster1()
        {
            Console.WriteLine($"Timewaster 1 wasting time for 10 seconds...\n");

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                Console.WriteLine($"TimeWaster1: {i + 1} seconds elapsed.\n");
            }

            return "TimeWaster1 Completed!\n";
        }

        public static async Task<string> TimeWaster2()
        {
            Console.WriteLine($"Timewaster 2 wasting time for 5 seconds...\n");

            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(1000);
                Console.WriteLine($"TimeWaster2: {i + 1} seconds elapsed.\n");
            }

            return "TimeWaster2 Completed!\n";
        }

        public static string TimeWaster3()
        {
            Console.WriteLine($"Timewaster 3 wasting time for 10 seconds...\n");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"TimeWaster3: {i + 1} seconds elapsed.\n");
            }

            return "TimeWaster3 Completed!\n";
        }

        public static string TimeWaster4()
        {
            Console.WriteLine($"Timewaster 4 wasting time for 5 seconds...\n");

            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"TimeWaster4: {i + 1} seconds elapsed.\n");
            }

            return "TimeWaster4 Completed!\n";
        }

    }


}
