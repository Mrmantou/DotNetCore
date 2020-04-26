using System.Threading.Tasks;

namespace App
{
    public interface IActionInvoker
    {
        Task InvokeAsync();
    }
}
