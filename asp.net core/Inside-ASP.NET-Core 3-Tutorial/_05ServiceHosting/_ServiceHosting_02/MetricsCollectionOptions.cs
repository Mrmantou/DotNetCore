using System;
using System.Collections.Generic;
using System.Text;

namespace _ServiceHosting_02
{
    public class MetricsCollectionOptions
    {
        public TimeSpan CaptureInterval { get; set; }
        public TransportType Transport { get; set; }
        public Endpoint DeliverTo { get; set; }
    }

    public enum TransportType
    {
        Tcp,
        Http,
        Udp
    }

    public class Endpoint
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public override string ToString() => $"{Host}:{Port}";
    }
}
