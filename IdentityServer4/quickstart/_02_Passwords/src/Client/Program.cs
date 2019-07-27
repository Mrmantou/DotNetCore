using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            // discover endpoints from metadata
            var discovery = await client.GetDiscoveryDocumentAsync("http://localhost:5000");

            if (discovery.IsError)
            {
                Console.WriteLine(discovery.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discovery.TokenEndpoint,

                ClientId = "ro.client",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "password",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine();

            // call api
            var requestClient = new HttpClient();
            requestClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await requestClient.GetAsync("http://localhost:5001/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
