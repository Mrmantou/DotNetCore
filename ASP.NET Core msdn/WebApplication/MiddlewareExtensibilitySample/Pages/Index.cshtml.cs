using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiddlewareExtensibilitySample.Data;
using MiddlewareExtensibilitySample.Models;

namespace MiddlewareExtensibilitySample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext db;

        public IndexModel(AppDbContext db)
        {
            this.db = db;
        }

        public List<Request> Requests { get; private set; }

        public async Task OnGetAsync()
        {
            Requests = await db.Requests.ToListAsync();
        }
    }
}
