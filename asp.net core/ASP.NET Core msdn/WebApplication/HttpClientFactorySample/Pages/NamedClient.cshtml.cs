using HttpClientFactorySample.GitHub;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactorySample.Pages
{
    public class NamedClientModel : PageModel
    {
        private IHttpClientFactory clientFactory;

        public IEnumerable<GitHubPullRequest> PullRequests { get; private set; }

        public bool GetPullRequestsError { get; private set; }

        public bool HasPullRequests => PullRequests.Any();

        public NamedClientModel(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "repos/aspnet/docs/pulls");

            var client = clientFactory.CreateClient("github");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                PullRequests = await response.Content.ReadAsAsync<IEnumerable<GitHubPullRequest>>();
            }
            else
            {
                GetPullRequestsError = true;
                PullRequests = Array.Empty<GitHubPullRequest>();
            }
        }
    }
}