using System;

namespace _NewCsharp7_3
{
    /// <summary>
    /// What's new in C# 7.3
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-3
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region ref local variables may be reassigned
            //ref VeryLargeStruct refLocal = ref veryLargeStruct; // initialization
            //refLocal = ref anotherVeryLargeStruct; // reassigned, refLocal refers to different storage.
            #endregion

            #region stackalloc arrays support initializers
            var arr1 = new int[3] { 1, 2, 3 };
            var arr2 = new int[] { 1, 2, 3 };

            //int* pArr = stackalloc int[3] { 1, 2, 3 };
            //int* pArr2 = stackalloc int[] { 1, 2, 3 };
            //Span<int> arr = stackalloc[] { 1, 2, 3 };
            #endregion

            Console.WriteLine("Hello World!");
        }
    }

    #region Extend expression variables in initializers
    public class B
    {
        public B(int i, out int j)
        {
            j = i;
        }
    }

    public class D : B
    {
        public D(int i) : base(i, out var j)
        {
            Console.WriteLine($"The value of 'j' is {j}");
        }
    }
    #endregion
}
