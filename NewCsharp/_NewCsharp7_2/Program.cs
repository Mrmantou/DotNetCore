using System;

namespace _NewCsharp7_2
{
    /// <summary>
    /// What's new in C# 7.2
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-2
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region Leading underscores in numeric literals
            int binaryValue = 0b_0101_0101;
            #endregion

            #region 'private protected' access modifier
            //A new compound access modifier: 'private protected' indicates that a member may be accessed by containing class or derived classes that are declared in the same assembly. While 'protected internal' allows access by derived classes or classes that are in the same assembly, 'private protected' limits access to derived types declared in the same assembly.

            // 'public': Access is not restricted.
            // 'protected': Access is limited to the containing class or types derived from the containing class.
            // 'internal': Access is limited to the current assembly.
            // 'protected internal': Access is limited to the current assembly or types derived from the containing class.
            // 'private': Access is limited to the containing type.
            // 'private protected': Access is limited to the containing class or types derived from the containing class within the current assembly.
            #endregion

            #region Conditional ref expressions
            int[] array1 = new int[] { 1, 2, 3 };
            int[] array2 = new int[] { 4, 5, 6, };

            ref var r = ref (array1 != null ? ref array1[0] : ref array2[0]);
            #endregion

            Console.WriteLine("Hello World!");
        }
    }
}
