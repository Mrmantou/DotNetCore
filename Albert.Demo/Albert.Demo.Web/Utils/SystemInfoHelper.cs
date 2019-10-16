using System.Runtime.InteropServices;

namespace Albert.Demo.Web.Utils
{
    public class SystemInfoHelper
    {
        public static string PlatformAndFrameworkInfo()
        {
            return $"{RuntimeInformation.FrameworkDescription} on {RuntimeInformation.OSDescription}";
        }
    }
}
