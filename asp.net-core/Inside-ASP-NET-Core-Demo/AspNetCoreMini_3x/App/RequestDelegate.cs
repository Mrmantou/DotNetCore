using System.Threading.Tasks;

namespace App
{
    public delegate Task RequestDelegate(HttpContext context);
}
