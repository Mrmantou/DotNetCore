using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace AlbertGrpc
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine($"request: {request.Name}");
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
        public override async Task SayHelloService(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"request: {request.Name}");

            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
                HelloReply helloReply = new HelloReply { Message = $"response {i}" };
                await responseStream.WriteAsync(helloReply);
            }
        }
    }
}
