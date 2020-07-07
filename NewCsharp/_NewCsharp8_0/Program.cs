using System;
using System.Runtime.CompilerServices;

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
            #region More patterns in more places


            #endregion

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
    }

    #region Readonly members
    public struct Point
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
