using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactorySample.GitHub
{
    public class RepoService
    {
        private readonly HttpClient client;

        public RepoService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<string>> GetRepos()
        {
            var response = await client.GetAsync("aspnet/repos");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<IEnumerable<string>>();

            return result;
        }
    }
}
