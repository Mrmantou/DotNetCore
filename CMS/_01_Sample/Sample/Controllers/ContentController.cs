using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Controllers
{
    /// <summary>
    /// Content控制器
    /// </summary>
    public class ContentController : Controller
    {
        public IActionResult Index()
        {
            var contents = new List<Content>();

            for (int i = 1; i < 11; i++)
            {
                contents.Add(new Content
                {
                    Id = i,
                    Title = $"{i}的标题",
                    ContentStr = $"{i}的内容",
                    Status = 1,
                    AddTime = DateTime.Now.AddDays(-i)
                });
            }

            return View(new ContentViewModel { Contents = contents });
        }
    }
}