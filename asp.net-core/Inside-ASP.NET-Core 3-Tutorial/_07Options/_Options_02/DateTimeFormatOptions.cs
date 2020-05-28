using System;
using System.Collections.Generic;
using System.Text;

namespace _Options_02
{
    public class DateTimeFormatOptions
    {
        public string DatePattern { get; set; }
        public string TimePattern { get; set; }
        public override string ToString() => $"Date: {DatePattern}; Time: {TimePattern}";
    }
}
