using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _FileSystem_01
{
    public interface IFileManager
    {
        void ShowStructure(Action<int, string> render);

        Task<string> ReadAllTextAsync(string path);
    }
}
