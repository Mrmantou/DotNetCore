using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace AlbertGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(new HelloRequest { Name = "albert" });

            Console.WriteLine(response.Message);

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }
    }
}
