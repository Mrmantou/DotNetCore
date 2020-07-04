using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace _NewCsharp7_0
{
    /// <summary>
    /// What's new in C# 7.0
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            new Program().RefLocalsAndReturns();
            Console.WriteLine("press any key to exit......");
        }

        /// <summary>
        /// now declare out variables in the argument list of a method call, rather than writing a separate declaration statement
        /// </summary>
        private void OutVariables()
        {
            if (int.TryParse("123", out int result))
            {
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Could not parse input");
            }

            if (int.TryParse("456", out var answer))
            {
                Console.WriteLine(answer);
            }
            else
            {
                Console.WriteLine("Could not parse input");
            }
        }

        /// <summary>
        /// C# provides a rich syntax for classes and structs that is used to explain your design intent. But sometimes that rich syntax requires extra work with minimal benefit. You may often write methods that need a simple structure containing more than one data element. To support these scenarios tuples were added to C#. Tuples are lightweight data structures that contain multiple fields to represent the data members. The fields aren't validated, and you can't define your own methods
        /// </summary>
        private void Tuples()
        {
            // tuple variable
            (string Alpha, string Beta) namedLetters = ("a", "b");
            Console.WriteLine($"{namedLetters.Alpha}, {namedLetters.Beta}");

            var alphabetStart = (Alpha: "a", Beta: "b");
            Console.WriteLine($"{alphabetStart.Alpha}, {alphabetStart.Beta}");

            // tuple method
            (int max, int min) = Range(new int[] { 1, 3, 5 });
            Console.WriteLine($"max: {max}, min: {min}");

            var p = new Point(3.14, 2.71);
            (double X, double Y) = p;

            (int, int) Range(IEnumerable<int> numbers)
            {
                var max = numbers.Max();
                var min = numbers.Min();

                return (max, min);
            }
        }

        /// <summary>
        /// Often when deconstructing a tuple or calling a method with out parameters, you're forced to define a variable whose value you don't care about and don't intend to use. C# adds support for discards to handle this scenario. A discard is a write-only variable whose name is _ (the underscore character); you can assign all of the values that you intend to discard to the single variable. A discard is like an unassigned variable; apart from the assignment statement, the discard can't be used in code.
        /// Discards are supported in the following scenarios:
        /// 1 When deconstructing tuples or user-defined types.
        /// 2 When calling methods with out parameters.
        /// 3 In a pattern matching operation with the is and switch statements.
        /// 4 As a standalone identifier when you want to explicitly identify the value of an assignment as a discard.
        /// </summary>
        private void Discards()
        {
            var (_, _, _, pop1, _, pop2) = QueryCityDataForYears("New York City", 1960, 2010);

            Console.WriteLine($"Population change, 1960 to 2010: {pop2 - pop1:N0}");

            (string, double, int, int, int, int) QueryCityDataForYears(string name, int year1, int year2)
            {
                int population1 = 0, population2 = 0;
                double area = 0;

                if (name == "New York City")
                {
                    area = 468.48;
                    if (year1 == 1960)
                    {
                        population1 = 7781984;
                    }

                    if (year2 == 2010)
                    {
                        population2 = 8175133;
                    }

                    return (name, area, year1, population1, year2, population2);
                }

                return ("", 0, 0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Pattern matching is a feature that allows you to implement method dispatch on properties other than the type of an object. You're probably already familiar with method dispatch based on the type of an object. In object-oriented programming, virtual and override methods provide language syntax to implement method dispatching based on an object's type. Base and Derived classes provide different implementations. Pattern matching expressions extend this concept so that you can easily implement similar dispatch patterns for types and data elements that aren't related through an inheritance hierarchy.
        /// Pattern matching supports 'is' expressions and 'switch' expressions.Each enables inspecting an object and its properties to determine if that object satisfies the sought pattern.You use the 'when' keyword to specify additional rules to the pattern.
        /// The 'is' pattern expression extends the familiar is operator to query an object about its type and assign the result in one instruction.
        /// The 'switch' match expression has a familiar syntax, based on the 'switch' statement already part of the C# language. The updated switch statement has several new constructs:
        /// 1 The governing type of a 'switch' expression is no longer restricted to integral types, 'Enum' types, 'string', or a nullable type corresponding to one of those types. Any type may be used.
        /// 2 You can test the type of the 'switch' expression in each 'case' label.As with the 'is' expression, you may assign a new variable to that type.
        /// 3 You may add a 'when' clause to further test conditions on that variable.
        /// 4 The order of 'case' labels is now important. The first branch to match is executed; others are skipped.
        /// </summary>
        private void PatternMatching()
        {
            var a = 5;
            if (a is int b)
            {
                Console.WriteLine(b);
            }

            var sum = SumPositivelNumbers(new object[] { 0, 1, -1, new int[] { 4, 5, 6 }, 8, 7 });
            Console.WriteLine(sum);

            int SumPositivelNumbers(IEnumerable<object> sequence)
            {
                int sum = 0;
                foreach (var i in sequence)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case IEnumerable<int> childSequence:
                            {
                                foreach (var item in childSequence)
                                {
                                    sum += (item > 0) ? item : 0;
                                }
                                break;
                            }
                        case int n when n > 0:
                            sum += n;
                            break;
                        case null:
                            throw new NullReferenceException("Null found in sequence");
                        default:
                            throw new InvalidOperationException("Unrecognized type");
                    }
                }
                return sum;
            }
        }

        /// <summary>
        /// This feature enables algorithms that use and return references to variables defined elsewhere. One example is working with large matrices, and finding a single location with certain characteristics.
        /// The C# language has several rules that protect you from misusing the ref locals and returns:
        /// You must add the ref keyword to the method signature and to all return statements in a method.
        /// ---That makes it clear the method returns by reference throughout the method.
        /// A ref return may be assigned to a value variable, or a ref variable.
        /// ---The caller controls whether the return value is copied or not.Omitting the ref modifier when assigning the return value indicates that the caller wants a copy of the value, not a reference to the storage.
        /// You can't assign a standard method return value to a ref local variable.
        /// ---That disallows statements like ref int i = sequence.Count();
        /// You can't return a ref to a variable whose lifetime doesn't extend beyond the execution of the method.
        /// ---That means you can't return a reference to a local variable or a variable with a similar scope.
        /// ref locals and returns can't be used with async methods.
        /// ---The compiler can't know if the referenced variable has been set to its final value when the async method returns.
        /// The addition of ref locals and ref returns enables algorithms that are more efficient by avoiding copying values, or performing dereferencing operations multiple times.
        /// </summary>
        private void RefLocalsAndReturns()
        {
            var matrix = new int[,] { { 1, 2, 3 }, { 4, 42, 6 }, { 7, 8, 9 } };
            ref var item = ref Find(matrix, val => val == 42);

            Console.WriteLine(item);
            item = 24;
            Console.WriteLine(matrix[1, 1]);

            ref int Find(int[,] matrix, Func<int, bool> predicate)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (predicate(matrix[i, j]))
                        {
                            return ref matrix[i, j];
                        }
                    }
                }

                throw new InvalidOperationException("Not found");
            }
        }

        /// <summary>
        /// Many designs for classes include methods that are called from only one location. These additional private methods keep each method small and focused. Local functions enable you to declare methods inside the context of another method. Local functions make it easier for readers of the class to see that the local method is only called from the context in which it is declared.
        /// </summary>
        private void LocalFunctions()
        {
            Function();

            void Function()
            {
                Console.WriteLine("this is a local function");
            }
        }

        #region More expression-bodied members
        /// <summary>
        /// C# 7.0 expands the allowed members that can be implemented as expressions. In C# 7.0, you can implement constructors, finalizers, and get and set accessors on properties and indexers. 
        /// </summary>
        class ExpressionMembersExample
        {
            private string label;
            // Expression-bodied constructor
            public ExpressionMembersExample(string label) => this.label = label;
            // Expression-bodied finalizer
            ~ExpressionMembersExample() => Console.Error.WriteLine("Finalized");

            // Expression-bodied get / set accessors.
            public string Label
            {
                get => label;
                set => this.label = value ?? "Default label";
            }
        }
        #endregion

        /// <summary>
        /// In C#, throw has always been a statement. Because throw is a statement, not an expression, there were C# constructs where you couldn't use it. These included conditional expressions, null coalescing expressions, and some lambda expressions. The addition of expression-bodied members adds more locations where throw expressions would be useful.
        /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/throw#the-throw-expression
        /// </summary>
        private void ThrowExpressions(string[] args)
        {
            DisplayFirstNumber(args);

            void DisplayFirstNumber(string[] args)
            {
                string arg = args.Length >= 1 ? args[0] :
                                           throw new ArgumentException("You must supply an argument");
                if (Int64.TryParse(arg, out var number))
                    Console.WriteLine($"You entered {number:F0}");
                else
                    Console.WriteLine($"{arg} is not a number.");
            }
        }

        #region Generalized async return types
        async ValueTask<int> Func()
        {
            await Task.Delay(100);
            return 5;
        }
        #endregion

        #region Numeric literal syntax improvements
        // C# 7.0 includes two new features to write numbers in the most readable fashion for the intended use: binary literals, and digit separators.
        // The 0b at the beginning of the constant indicates that the number is written as a binary number. Binary numbers can get long, so it's often easier to see the bit patterns by introducing the _ as a digit separator, as shown above in the binary constant. The digit separator can appear anywhere in the constant.

        public const int Sixteen = 0b0001_0000;
        public const int ThirtyTwo = 0b0010_0000;
        public const int SixtyFour = 0b0100_0000;
        public const int OneHundredTwentyEight = 0b1000_0000;

        public const long BillionsAndBillions = 100_000_000_000;

        public const double AvogadroConstant = 6.022_140_857_747_474e23;
        public const decimal GoldenRatio = 1.618_033_988_749_894_848_204_586_834_365_638_117_720_309_179M;
        #endregion

        class Point
        {
            public Point(double x, double y) => (X, Y) = (x, y);

            public double X { get; }
            public double Y { get; }

            public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);
        }
    }
}
