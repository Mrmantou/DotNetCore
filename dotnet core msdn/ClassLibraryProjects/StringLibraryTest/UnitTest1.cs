using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibraries;

namespace StringLibraryTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestStartWithUpper()
        {
            string[] words = { "Alphabet", "Zebra", "ABC", "Αθήνα", "Москва" };

            foreach (var item in words)
            {
                bool result = item.StartsWithUpper();
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void TestDoseNotStartWithUpper()
        {
            string[] words = { "alphabet", "zebra", "aBC", "aθήνα", "mосква" };

            foreach (var item in words)
            {
                bool result = item.StartsWithUpper();

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void DirectCallWithNullOrEmpty()
        {
            string[] words = { string.Empty, null };

            foreach (var item in words)
            {
                bool result = item.StartsWithUpper();

                Assert.IsFalse(result);
            }
        }
    }
}
