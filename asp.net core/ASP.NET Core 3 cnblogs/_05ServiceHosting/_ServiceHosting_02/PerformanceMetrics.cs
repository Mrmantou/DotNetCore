using System;
using System.Collections.Generic;
using System.Text;

namespace _ServiceHosting_02
{
    public class PerformanceMetrics
    {
        private static readonly Random random = new Random();

        public int Processor { get; set; }
        public long Memory { get; set; }
        public long Network { get; set; }

        public override string ToString() => $"CPU: {Processor * 100}%; Memory: {Memory / (1024 * 1024)}M; Network: {Network / (1024 * 1024)}M/s";

        public static PerformanceMetrics Create() => new PerformanceMetrics
        {
            Processor = random.Next(1, 8),
            Memory = random.Next(10, 100) * 1024 * 1024,
            Network = random.Next(10, 100) * 1024 * 1024
        };
    }
}
