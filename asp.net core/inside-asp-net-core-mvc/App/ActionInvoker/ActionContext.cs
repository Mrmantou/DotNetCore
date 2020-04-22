using Microsoft.AspNetCore.Http;

namespace App
{
    public class ActionContext
    {
        public ActionDescriptor ActionDescriptor { get; set; }
        public HttpContext HttpContext { get; set; }
    }
}
