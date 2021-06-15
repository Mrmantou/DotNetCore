using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.EventHandler
{
    public class DateTimeHandler : ICapSubscribe
    {
        [CapSubscribe("xxx.services.show.time")]
        public void Handle(DateTime datetime)
        {
            Console.WriteLine($"_02_App handle: {datetime}");
        }
    }
}
