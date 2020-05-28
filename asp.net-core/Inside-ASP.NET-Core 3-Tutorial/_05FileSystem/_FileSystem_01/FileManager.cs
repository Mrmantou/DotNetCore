using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _FileSystem_01
{
    public class FileManager : IFileManager
    {
        private readonly IFileProvider fileProvider;

        public FileManager(IFileProvider fileProvider) => this.fileProvider = fileProvider;

        public void ShowStructure(Action<int, string> render)
        {
            int indent = -1;

            Render("");

            void Render(string subPath)
            {
                indent++;
                foreach (var fileInfo in fileProvider.GetDirectoryContents(subPath))
                {
                    render(indent, fileInfo.Name);
                    if (fileInfo.IsDirectory)
                    {
                        Render($@"{subPath}\{fileInfo.Name}".TrimStart('\\'));
                    }
                }
                indent--;
            }
        }

        public async Task<string> ReadAllTextAsync(string path)
        {
            byte[] buffer;
            using (Stream stream = fileProvider.GetFileInfo(path).CreateReadStream())
            {
                buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
            }

            return Encoding.Default.GetString(buffer);
        }
    }
}
