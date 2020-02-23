using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlbertGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region 一元调用
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(new HelloRequest { Name = "albert" });

            Console.WriteLine(response.Message);
            #endregion

            #region 服务器流式处理
            var responseStream = client.SayHelloService(new HelloRequest { Name = "albert" });

            var readStream = responseStream.ResponseStream;

            while (await readStream.MoveNext())
            {
                Thread.Sleep(1000);
                HelloReply helloReply = readStream.Current;
                Console.WriteLine($"reply {helloReply.Message}");
            }
            #endregion

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }
    }
}
