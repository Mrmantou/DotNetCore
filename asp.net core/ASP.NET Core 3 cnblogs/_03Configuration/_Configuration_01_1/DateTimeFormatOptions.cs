using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace _Configuration_01_1
{
    public class DateTimeFormatOptions
    {
        public DateTimeFormatOptions(IConfiguration config)
        {
            LongDatePattern = config["LongDatePattern"];
            LongTimePattern = config["LongTimePattern"];
            ShortDatePattern = config["ShortDatePattern"];
            ShortTimePattern = config["ShortTimePattern"];
        }

        public string LongDatePattern { get; set; }
        public string LongTimePattern { get; set; }
        public string ShortDatePattern { get; set; }
        public string ShortTimePattern { get; set; }
    }
}
