using System.Threading.Tasks;

namespace App
{
    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }
}
