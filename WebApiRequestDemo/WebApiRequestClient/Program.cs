using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApiRequestClient
{
    class Program
    {
        private static string url = "http://106.13.41.218:8083/api/values";
        static void Main()
        {
            HttpClientGetTest().Wait();

            HttpClientPostTest().Wait();

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();
        }


        private static async Task HttpClientPostTest()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent httpContent = new StringContent("123456");
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json")
                {
                    CharSet = "utf-8"
                };

                var response = await client.PostAsync(url, httpContent);
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
        }

        private static async Task HttpClientGetTest()
        {
            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://106.13.41.218:8083");

                //var response = await client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                //string responseBody = await response.Content.ReadAsStringAsync();

                string responseBody = await client.GetStringAsync(url);
                Console.WriteLine(responseBody);

                responseBody = await client.GetStringAsync(url + "/159");
                Console.WriteLine(responseBody);
            }
        }
    }
}
