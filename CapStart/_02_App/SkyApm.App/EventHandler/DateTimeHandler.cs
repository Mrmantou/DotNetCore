using DotNetCore.CAP;
using System;

namespace SkyApm.App.EventHandler
{
    public class DateTimeHandler : ICapSubscribe
    {
        private static int retryCount = 0;

        [CapSubscribe("xxx.services.show.time")]
        public void Handle(DateTime datetime)
        {
            if (retryCount++ < 3)
            {
                throw new Exception("DateTimeHandler throw exception");
            }

            Console.WriteLine($"App handle: {datetime}");
        } 
    }
}
