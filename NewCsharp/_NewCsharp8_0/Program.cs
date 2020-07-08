using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace _NewCsharp8_0
{
    /// <summary>
    /// What's new in C# 8.0
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            NullCoalescingAssignment();

            Console.WriteLine("Hello World!");
        }

        #region switch expression
        static RGBColor FromRainbow(Rainbow colorBand) =>
            colorBand switch
            {
                Rainbow.Red => new RGBColor(0xFF, 0x00, 0x00),
                Rainbow.Orange => new RGBColor(0xFF, 0x7F, 0x00),
                Rainbow.Yellow => new RGBColor(0xFF, 0xFF, 0x00),
                Rainbow.Green => new RGBColor(0x00, 0xFF, 0x00),
                Rainbow.Blue => new RGBColor(0x00, 0x00, 0xFF),
                Rainbow.Indigo => new RGBColor(0x4B, 0x00, 0x82),
                Rainbow.Violet => new RGBColor(0x94, 0x00, 0xD3),
                _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand))
            };
        #endregion

        #region Property patterns
        public static decimal ComputeSalesTax(Address location, decimal salePrice) => location switch
        {
            { State: "WA" } => salePrice * 0.06M,
            { State: "MN" } => salePrice * 0.075M,
            { State: "MI" } => salePrice * 0.05M,
            _ => 0M
        };
        #endregion

        #region Tuple patterns
        public static string RockPaperScissors(string first, string second) => (first, second) switch
        {
            ("rock", "paper") => "rock is covered by paper. Paper wins.",
            ("rock", "scissors") => "rock breaks scissors. Rock wins.",
            ("paper", "rock") => "paper covers rock. Paper wins.",
            ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
            ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
            ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
            (_, _) => "tie"
        };
        #endregion

        #region Positional patterns
        static Quadrant GetQuadrant(Point point) => point switch
        {
            (0, 0) => Quadrant.Origin,
            var (x, y) when x > 0 && y > 0 => Quadrant.One,
            var (x, y) when x < 0 && y > 0 => Quadrant.Two,
            var (x, y) when x < 0 && y < 0 => Quadrant.Three,
            var (x, y) when x > 0 && y < 0 => Quadrant.Four,
            var (_, _) => Quadrant.OnBorder,
            _ => Quadrant.Unknown
        };
        #endregion

        #region Using declarations
        static int WriteLinesToFile(IEnumerable<string> lines)
        {
            using var file = new System.IO.StreamWriter("WriteLines2.txt");
            // Notice how we declare skippedLines after the using statement.
            int skippedLines = 0;
            foreach (var line in lines)
            {
                if (!lines.Contains("Second"))
                {
                    file.WriteLine(line);
                }
                else
                {
                    skippedLines++;
                }
            }
            // Notice how skippedLines is in scope here.
            return skippedLines;
            // file is disposed here
        }

        static int ClassicWriteLinesToFile(IEnumerable<string> lines)
        {
            // We must declare the variable outside of the using block
            // so that it is in scope to be returned.
            int skippedLines = 0;
            using (var file = new System.IO.StreamWriter("WriteLines2.txt"))
            {
                foreach (string line in lines)
                {
                    if (!line.Contains("Second"))
                    {
                        file.WriteLine(line);
                    }
                    else
                    {
                        skippedLines++;
                    }
                }
            } // file is disposed here
            return skippedLines;
        }
        #endregion

        #region Static local functions

        /// <summary>
        /// now add the static modifier to local functions to ensure that local function doesn't capture (reference) any variables from the enclosing scope. 
        /// </summary>
        /// <returns></returns>
        int M()
        {
            //int y;
            //LocalFunction();
            //return y;

            //static void LocalFunction() => y = 0; //Error CS8421  A static local function cannot contain a reference to 'y'.

            int x = 5;
            int y = 7;

            return Add(x, y);

            static int Add(int left, int right) => left + right;
        }
        #endregion

        #region Asynchronous streams

        public static async IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }

        static async void Function()
        {
            await foreach (var item in GenerateSequence())
            {
                Console.WriteLine(item);
            }
        }

        #endregion

        #region Indices and ranges
        /// <summary>
        /// An array sequence. The 0 index is the same as sequence[0]. The ^0 index is the same as sequence[sequence.Length]. Note that sequence[^0] does throw an exception, just as sequence[sequence.Length] does. For any number n, the index ^n is the same as sequence.Length - n.
        /// A range specifies the start and end of a range. The start of the range is inclusive, but the end of the range is exclusive, meaning the start is included in the range but the end isn't included in the range. The range [0..^0] represents the entire range, just as [0..sequence.Length] represents the entire range.
        /// </summary>
        static void IndicesAndRangesDemo()
        {
            var words = new string[]
            {
                            // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
            };              // 9 (or words.Length) ^0

            Console.WriteLine($"The last word is {words[^1]}");//The last word is dog

            var quickBrownFox = words[1..4]; //Result includes words[1] through words[3]. The element words[4] isn't in the range.
            Console.WriteLine($"quickBrownFox include: {string.Join(' ', quickBrownFox)}");//quickBrownFox include: quick brown fox

            var lazyDog = words[^2..^0];
            var allWords = words[..]; // contains "The" through "dog".
            var firstPhrase = words[..4]; // contains "The" through "fox"
            var lastPhrase = words[6..]; // contains "the", "lazy" and "dog"

            Range phrase = 1..4;
            var text = words[phrase];
        }
        #endregion

        #region Null-coalescing assignment ??=
        static void NullCoalescingAssignment()
        {
            List<int> numbers = null;
            int? i = null;
            int? j = null;

            numbers ??= new List<int>();
            numbers.Add(i ??= 17);
            numbers.Add(i ??= 20);
            numbers.Add(j ?? 25);

            Console.WriteLine(string.Join(" ", numbers));  // output: 17 17 25
            Console.WriteLine($"i= {i}");  // i= 17
            Console.WriteLine($"j= {j}");  // j=
        }
        #endregion

        #region Unmanaged constructed types
        static void UnmanagedConstructedTypes()
        {
            Span<Coords<int>> coordinates = stackalloc[]
            {
                new Coords<int> { X = 0, Y = 0 },
                new Coords<int> { X = 0, Y = 3 },
                new Coords<int> { X = 4, Y = 0 }
            };
        }
        #endregion
    }

    #region Readonly members
    public struct PointStruct
    {
        public double X { get; set; }
        public double Y { get; set; }
        public readonly double Distance => Math.Sqrt(X * X + Y * Y);

        public readonly override string ToString() => $"({X}, {Y}) is {Distance} from origin";

        public readonly void Translate(int xOffset, int yOffset)
        {
            //X += xOffset;  //Error CS1604  Cannot assign to 'X' because it is read-only
            //Y += yOffset;  //Error CS1604  Cannot assign to 'Y' because it is read-only
        }
    }
    #endregion

    public struct Coords<T>
    {
        public T X;
        public T Y;
    }

    class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y) => (X, Y) = (x, y);

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }

    enum Quadrant
    {
        Unknown,
        Origin,
        One,
        Two,
        Three,
        Four,
        OnBorder
    }

    class Address
    {
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    class RGBColor
    {
        public RGBColor(int r, int g, int b) { }
    }

    enum Rainbow
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }
}
