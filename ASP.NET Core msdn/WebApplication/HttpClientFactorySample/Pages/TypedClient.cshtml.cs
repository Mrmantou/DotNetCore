using HttpClientFactorySample.GitHub;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactorySample.Pages
{
    public class TypedClientModel : PageModel
    {
        private readonly GitHubService gitHubService;

        public IEnumerable<GitHubIssue> LatestIssues { get; private set; }

        public bool HasIssue => LatestIssues.Any();

        public bool GetIssuesError { get; private set; }


        public TypedClientModel(GitHubService gitHubService)
        {
            this.gitHubService = gitHubService;
        }

        public async Task OnGet()
        {
            try
            {
                LatestIssues = await gitHubService.GetAspNetDocsIssues();
            }
            catch (HttpRequestException)
            {
                GetIssuesError = true;
                LatestIssues = Array.Empty<GitHubIssue>();
            }
        }
    }
}