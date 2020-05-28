using System.Threading.Tasks;

namespace PipeLineDemo
{
    public delegate Task RequestDelegate(HttpContext context);
}
