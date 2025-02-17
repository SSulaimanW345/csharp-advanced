// See https://aka.ms/new-console-template for more information
using CSharp.Advanced.Console.Async;
using CSharp.Advanced.Console.Delegates;
using CSharp.Advanced.Console.Generics;
using CSharp.Advanced.Console.HigherOrderFunctions;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization;

namespace CSharp {
    public class Program
    {   
        public static List<Movie> movies  = new List<Movie> ();
        private static UserService _userService = new UserService();
        public delegate bool MyFunc(int x);
        public delegate void TransformDelgate(string message);
        static ShoppingCart cart = new ShoppingCart();
        static async Task Main(string[] args)
        {
            // 1- example
            //PopulateCartWithDemoData();
            //// using functions 
            //Console.WriteLine($"The total for the cart is {cart.GenerateTotal(SubTotalAlert, CalculateDiscount, AlertUser)}");
            //// using anonymous methods
            //decimal total = cart.GenerateTotal(
            //    (subTotal) => Console.WriteLine($"The subtotal is {subTotal:C2}"),
            //    (products, subTotal) =>
            //    {
            //        if (subTotal > 100) return subTotal * 0.80m;
            //        else if (subTotal > 50) return subTotal * 0.85m;
            //        else if (subTotal > 10) return subTotal * 0.90m;
            //        else return subTotal;
            //    },
            //    (message) => Console.WriteLine($"{message}")
            //    );

            //// 2- Example
            //TransformDelgate a, b, c, d;
            //a = Method1;
            //b = new TransformDelgate(Method1); // proof it is a class
            //Func<int, bool> myDel = n => n % 2 == 0; // defining implementation when defining the delegate instance
            //Func<int, bool> myDelLk = new Func<int, bool>(n => n % 2 == 0);
            //MyFunc myDelType = n => n % 2 == 0;
            ////3- Example
            //var numbers = Enumerable.Range(1, 100);
            //var evenNumbers = numbers.Where(EvenNumbers);

            //var evenNumbersFunc = numbers.Where(new Func<int, bool>(EvenNumbers)); // func delegate

            //var evenNumbersAnonymous = numbers.Where(delegate (int x) { return x % 2 == 0; });// anonymous method

            //var evenNumbersLambda = numbers.Where(x => x % 2 == 0);//lambda expression and lambda statement block
            //var evenNumbersCompilerExpression = numbers.Where(new Func<int, bool>(x => x % 2 == 0));
            ////var evenNumbersMyDefinedType = numbers.Where(myDelType); // not right - type is different

            //// 4- example
            //var photoFilters = new PhotoFilters();
            //PhotoProcessor.PhotoHandler filterHandler = photoFilters.ApplyContrast;
            //filterHandler += photoFilters.ApplyBrightness;
            //PhotoProcessor.ProcessPhoto("example.jpg", filterHandler);

            //// HigherOrder functions

            //int requiredYear = 2019;
            //PopulateMovieCollectionDemoData();
            //var singleMovie = movies.SingleElseException(x=> x.ReleaseYear == requiredYear,
            //    (matchedMovies) => 
            //    {
            //        return new FilterMovieException($"we were expecting to find one for the year{requiredYear} , but we found {matchedMovies.Count()}");
            //    }
            //    );

            //// Discriminated C# Unions
            //var email = "example@gmail.com";
            //var password = "test123";
            //var result = _userService.CreateUser(email, password);

            //result.Switch(
            //    user => SendWelcomeEmail(user),
            //    validationError => HandleError(validationError)
            //);

            //// Generics
            //GenericTextFileProcessor.SaveToTextFile<Person>(new List<Person>(),"testing.csv");
            //GenericTextFileProcessor.SaveToTextFile<LogEntry>(new List<LogEntry>(), "testing.csv");

            //var num = new Advanced.Console.Generics.Nullable<int>();
            //Console.WriteLine("Has Value ?" + num.HasValue);
            //Console.WriteLine("Value: " + num.GetValueOrDefault());

            //await new Streaming().Refresh();
            await new Streaming().RefreshAsync();
            //Console.Write("Please press any key to exit the application...");
            //Console.ReadKey();
        }

        private static void HandleError(ValidationError validationError)
        {
            throw new NotImplementedException();
        }

        private static void SendWelcomeEmail(User user)
        {
            throw new NotImplementedException();
        }

        private static bool EvenNumbers(int x)
        {
            return x % 2 == 0;
        }

        private static void Method1(string msg)
        {
            Console.WriteLine($"This is method 1 called with message{msg}");
        }
        private static void Method2(string msg)
        {
            Console.WriteLine($"This is method 1 called with message{msg}");
        }

        private static void PopulateCartWithDemoData()
        {
            cart.Items.Add(new Advanced.Console.Delegates.Product { ItemName = "Cereal", Price = 3.63M });
            cart.Items.Add(new Advanced.Console.Delegates.Product { ItemName = "Milk", Price = 2.95M });
            cart.Items.Add(new Advanced.Console.Delegates.Product { ItemName = "Strawberries", Price = 7.51M });
            cart.Items.Add(new Advanced.Console.Delegates.Product { ItemName = "Blueberries", Price = 8.84M });
        }
        private static void PopulateMovieCollectionDemoData()
        {
            movies.Add(new Movie { Id = 1, Title = "My Test Movie 1" , ReleaseYear = 2019});
            movies.Add(new Movie { Id = 2, Title = "My Test Movie 2", ReleaseYear = 2019 });
            movies.Add(new Movie {Id = 3, Title = "My Test Movie 3", ReleaseYear = 2022 });
            movies.Add(new Movie {Id = 4, Title = "My Test Movie 4", ReleaseYear = 2024 });
        }

        private static void SubTotalAlert(decimal subTotal)
        {
            Console.WriteLine($"The subtotal is {subTotal:C2}");
        }
        private static void AlertUser(string message)
        {
            Console.WriteLine($"{message}");
        }

        private static decimal CalculateDiscount(List<Advanced.Console.Delegates.Product> items, decimal subTotal)
        {
            if (subTotal > 100) return subTotal * 0.80m;
            else if (subTotal > 50) return subTotal * 0.85m;
            else if (subTotal > 10) return subTotal * 0.90m;
            else return subTotal;
        }



    }
}

// Notes - regarding delegates

// events are based on delegates
// delegates are essentially classes
// delegates have an Invoke method
// Delegates are used to pass methods as arguments to other methods.
// Compile time - compiler converts the delgate to class definition that inherits from .NET multicastdelgate 
// In turn delgate to multiple targets
// Delegates are first class citizens and can be passed to methods as arguments and can be returned from methods
// Strongly typed function pointer
// A delegate is a type that represents references to methods with a particular parameter list and return type. 
// Delegates can be used to define callback methods.
// is created using the delegate keyword and doesn't require a name and return type.
// Hence we can say, that an anonymous method has only a body without a name, optional parameters, and return type.