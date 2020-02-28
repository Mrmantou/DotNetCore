using System;
using System.Collections.Generic;
using System.Text;

namespace _FileSystem_01
{
    public interface IFileManager
    {
        void ShowStructure(Action<int, string> render);
    }
}
