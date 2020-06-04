using System;
using System.IO;

namespace _Configuration_01_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var stream = new FileStream("appsettings.json", FileMode.Open);
            var config = JsonConfigurationFileParser.Parse(stream);
        }
    }
}
