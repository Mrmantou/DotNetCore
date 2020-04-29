using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App
{
    public class Compiler : ICompiler
    {
        public Assembly Compile(string text, params Assembly[] referenceAssemblies)
        {
            var references = referenceAssemblies.Select(i => MetadataReference.CreateFromFile(i.Location));
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var assemblyName = "_" + Guid.NewGuid().ToString("D");
            var syntaxTrees = new SyntaxTree[] { CSharpSyntaxTree.ParseText(text) };
            var compilation = CSharpCompilation.Create(assemblyName, syntaxTrees, references, options);

            using var stream = new MemoryStream();
            var compilationResult = compilation.Emit(stream);
            if (compilationResult.Success)
            {
                stream.Seek(0, SeekOrigin.Begin);
                return Assembly.Load(stream.ToArray());
            }

            throw new InvalidOperationException("Compilation error");
        }
    }
}
