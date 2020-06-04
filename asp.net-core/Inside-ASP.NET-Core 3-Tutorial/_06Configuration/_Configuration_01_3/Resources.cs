using System;
using System.Collections.Generic;
using System.Text;

namespace _Configuration_01_3
{
    static partial class Resources
    {
        private static global::System.Resources.ResourceManager s_resourceManager;
        internal static global::System.Resources.ResourceManager ResourceManager => s_resourceManager ?? (s_resourceManager = new global::System.Resources.ResourceManager(typeof(Resources)));
        internal static global::System.Globalization.CultureInfo Culture { get; set; }
#if !NET20
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
        internal static string GetResourceString(string resourceKey, string defaultValue = null) => ResourceManager.GetString(resourceKey, Culture);

        private static string GetResourceString(string resourceKey, string[] formatterNames)
        {
            var value = GetResourceString(resourceKey);
            if (formatterNames != null)
            {
                for (var i = 0; i < formatterNames.Length; i++)
                {
                    value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
                }
            }
            return value;
        }

        /// <summary>File path must be a non-empty string.</summary>
        internal static string @Error_InvalidFilePath => GetResourceString("Error_InvalidFilePath");
        /// <summary>Could not parse the JSON file.</summary>
        internal static string @Error_JSONParseError => GetResourceString("Error_JSONParseError");
        /// <summary>A duplicate key '{0}' was found.</summary>
        internal static string @Error_KeyIsDuplicated => GetResourceString("Error_KeyIsDuplicated");
        /// <summary>A duplicate key '{0}' was found.</summary>
        internal static string FormatError_KeyIsDuplicated(object p0)
           => string.Format(Culture, GetResourceString("Error_KeyIsDuplicated"), p0);

        /// <summary>Unsupported JSON token '{0}' was found.</summary>
        internal static string @Error_UnsupportedJSONToken => GetResourceString("Error_UnsupportedJSONToken");
        /// <summary>Unsupported JSON token '{0}' was found.</summary>
        internal static string FormatError_UnsupportedJSONToken(object p0)
           => string.Format(Culture, GetResourceString("Error_UnsupportedJSONToken"), p0);


    }
}
