using Utilities;

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
        public string Name { get; set; } = "Your Name Here";

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
        Predicate<string> IsStringYourName;

        public DelegateExamples(bool trivialBoolean)
        {
            
            // Example of declaring as invoking the original delegate-syntax
            GreetingsDelegate greetingsDelegate = SimpleFunctions.Greetings;
            GoodbyeDelegate goodbyeDelegate = SimpleFunctions.Goodbye;

            // Assigning our Predicate delegate to a simple lambda expression
            // that checks if the name provided equals the Name variable above.
            IsStringYourName = (name) => name.Equals(Name);

            // Example of invoking the original delegate-syntax
            greetingsDelegate.Invoke(Name);
            goodbyeDelegate.Invoke(Name);


            // Example of declaring as invoking the new Func syntax
            StringReturnDelegate = SimpleFunctions.FormalName;
            Console.WriteLine(StringReturnDelegate.Invoke(Name));

            // Example of declaring as invoking the new Action syntax
            GreetingsAction = SimpleFunctions.Greetings;
            GoodbyeAction = SimpleFunctions.Goodbye;

            // Invoking the newly-defined Action delegates
            GreetingsAction.Invoke(Name);
            GoodbyeAction.Invoke(Name);

            // Example of changing the assignment to the Func delegate
            StringReturnDelegate = SimpleFunctions.GreetTheBIGDAWG;

            // Invoking the newly-defined Func delegate and printing the value that is returned.
            Console.WriteLine(StringReturnDelegate.Invoke(Name));

            // Example of changing the assignment to the Action delegate,
            // based off the trivial boolean condition we passed in.
            if (trivialBoolean)
            {
                GreetingsAction = SimpleFunctions.Greetings;
            }
            else
            {
                GreetingsAction = SimpleDelegates.CallYouAGoodFriendAction;
            }

            GreetingsAction.Invoke(Name);

            // Example of defining a predicate delegate and invoking them
            Console.WriteLine(IsStringYourName.Invoke(Name));
            Console.WriteLine(IsStringYourName.Invoke("John"));
        }


    }

}
