using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace ConsoleLab
{
    internal class Program
    {
        public static void Main()
        {
            ExplicitTasks explicitTasks = new ExplicitTasks();
            
            explicitTasks.ExecuteWaitingTasks();
            explicitTasks.ExecuteContinuingTasks();
            explicitTasks.ExecuteParallelTasksViaInvoke();
            explicitTasks.HandleExceptionsInTasks();
        }
        private class PromiseBasedTasks()
        {

        }

        private class ExplicitTasks()
        {

            /// <summary>
            /// This method demonstrates creating and starting tasks explicitly,
            /// as well as demonstrating waiting for their completion and retrieving results.
            /// </summary>
            public void ExecuteWaitingTasks()
            {
                Thread.CurrentThread.Name = "ExplicitTasks";

                Task<string> TimeWasterInstance1 = new Task<string>(SimpleFunctions.TimeWaster1);
                Task<string> TimeWasterInstance2 = new Task<string>(SimpleFunctions.TimeWaster2);

                TimeWasterInstance1.Start();
                TimeWasterInstance2.Start();

                Console.WriteLine("Waiting for TimeWasterInstance1 to complete...");
                TimeWasterInstance1.Wait();
                Console.WriteLine("TimeWasterInstance1 ran to completion!");

                Console.WriteLine("Waiting for TimeWasterInstance2 to complete...");
                TimeWasterInstance2.Wait();
                Console.WriteLine("TimeWasterInstance2 ran to completion!");

                string result1 = TimeWasterInstance1.Result;
                string result2 = TimeWasterInstance2.Result;

                Console.WriteLine($"Value of result1: {result1}");
                Console.WriteLine($"Value of result2: {result2}");
            }

            /// <summary>
            /// This class method demonstrates creating tasks that continue from the completion of other tasks.
            /// Important to note: When using ContinueWith - you must retain the created Task<> object in order to retrieve the result.
            /// </summary>
            public void ExecuteContinuingTasks()
            {
                Console.WriteLine($"ExecuteContinuingTasks: Creating Task1!\n");

                Task<string> Task1 = new Task<string>(SimpleFunctions.TimeWaster1);
                Task<string> Task2 = new Task<string>(SimpleFunctions.TimeWaster2);

                Console.WriteLine($"ExecuteContinuingTasks: Assigning 'ContinueWith' Tasks!\n");

                /*Using ContinueWith here is how we 'chain' tasks together.
                * Note that in order to retrieve the result of a task that is continued from another task,
                * you must retain the Task<> object that is returned from ContinueWith!
                */
                Task<string> Task3 = Task1.ContinueWith<string>((previousTask) =>
                {
                    Console.WriteLine($"ExecuteContinuingTasks: Previous Task Result from Task1 was: {previousTask.Result}\n");

                    Console.WriteLine($"ExecuteContinuingTasks: Starting Task2...\n");
                    Task2.Start();
                    Task2.Wait();
                    return Task2.Result + previousTask.Result;
                });

                Console.WriteLine($"ExecuteContinuingTasks:Starting and waiting for Task1...\n");

                Task1.Start();
                Task1.Wait();

                Console.WriteLine($"ExecuteContinuingTasks:Task1 result was:\n{Task1.Result}\n");
                Console.WriteLine($"ExecuteContinuingTasks:Task2 result was:\n{Task2.Result}\n");
                Console.WriteLine($"ExecuteContinuingTasks:Task3 result was:\n{Task3.Result}\n");

            }
            /// <summary>
            /// Demonstrates how to execute Functions in parallel using Parallel.Invoke.
            /// </summary>
            public void ExecuteParallelTasksViaInvoke()
            {
                Console.WriteLine($"ExecuteParallelTasksViaInvoke:Executing TimeWaster3 and TimeWaster4 in-parallel!\n");

                Parallel.Invoke(
                    () => SimpleFunctions.TimeWaster1(),
                    () => SimpleFunctions.TimeWaster2()
                );
            }

            /// <summary>
            /// Demonstrates handling exceptions that occur within tasks.
            /// https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-based-asynchronous-programming#handling-exceptions-in-tasks
            /// </summary>
            public void HandleExceptionsInTasks()
            {
                int[] Numbers = { 1, 2, 3, 4, 5, 0, 6, 7, 8, 9 };

                // This task will fault because it attempts to access an index that is out of bounds.
                Task<int> FaultyTask = new Task<int>(() => Numbers[10]);

                try
                {
                    FaultyTask.Start();
                    FaultyTask.Wait();
                }
                catch (AggregateException ae)
                {
                    foreach (var ex in ae.InnerExceptions)
                    {
                        Console.WriteLine($"We knew this would happen!\nCaught exception: {ex.Message}\n");
                    }
                }
            }
        }



    }
}
