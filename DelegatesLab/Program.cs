using System.Reflection.Metadata.Ecma335;

namespace DelegatesLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DelegateExamples delegateConsumer = new DelegateExamples(false);
        }
    }

    internal class DelegateExamples
    {
        // Formal Delegate Definitions: 
        public delegate void GreetingsDelegate(string name);
        public delegate void GoodbyeDelegate(string name);
        public delegate bool IsStringJamesDelegate(string name);

        // The new shorthand: "Func" for delegates that could have many parameters, and return a value.
        Func<string, string> StringReturnDelegate;

        // The new shorthand: "Action" for delegates that take a single parameter and do not return a value (used for side-effects).
        Action<string> GreetingsAction;
        Action<string> GoodbyeAction;

        // The new shorthand: "Predicate" for delegates that take some parameters and return a boolean!
        Predicate<string> IsStringJames;

        public DelegateExamples(bool condition)
        {
            // Example of declaring as invoking the original delegate-syntax
            GreetingsDelegate greetingsDelegate = Greetings;
            GoodbyeDelegate goodbyeDelegate = Goodbye;

            greetingsDelegate.Invoke("James");
            goodbyeDelegate.Invoke("James");


            // Example of declaring as invoking the new Func syntax
            StringReturnDelegate = FormalName;
            Console.WriteLine(StringReturnDelegate.Invoke("James"));

            // Example of declaring as invoking the new Action syntax
            GreetingsAction = Greetings;
            GoodbyeAction = Goodbye;

            GreetingsAction.Invoke("James");
            GoodbyeAction.Invoke("James");

            // Example of changing the assignment to the Func delegate
            StringReturnDelegate = GreetTheBIGDAWG;
            Console.WriteLine(StringReturnDelegate.Invoke("James"));

            // Example of changing the assignment to the Action delegate
            if (condition)
            {
                GreetingsAction = Greetings;
            }
            else
            {
                GreetingsAction = _callYouANerd;
            }

            GreetingsAction.Invoke("James");

            // Example of defining a predicate delegate and invoking them
            IsStringJames = (name) => name.Equals("James");
            Console.WriteLine(IsStringJames.Invoke("James"));
            Console.WriteLine(IsStringJames.Invoke("John"));
        }

        private void Greetings(string name)
        {
            Console.WriteLine($"Hello {name}!\n");
        }

        private void Goodbye(string name)
        {
            Console.WriteLine($"Goodbye {name}!\n");
        }

        private string FormalName(string name)
        {
            return $"Mr. {name}\n";
        }

        private string GreetTheBIGDAWG(string name)
        {
            return $"Hello BIGDAWG {name}!\n";
        }

        private string FarewellToTheBIGDAWG(string name)
        {
            return $"Farewell, BIGDAWG {name}!\n";
        }

        private Action<string> _callYouANerd = (name) => Console.WriteLine($"Hello, {name} you are a nerd!\n");

        private Func<string, string> _callYouANerdFunc = (name) => { return $"You are a nerd {name}"; };
    }

}
