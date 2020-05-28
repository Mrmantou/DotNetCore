using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace _Options_06
{
    public class JsonFileConfigureOptions<TOptions> : IConfigureNamedOptions<TOptions> where TOptions : class, new()
    {
        private readonly IFileProvider fileProvider;
        private readonly string path;
        private readonly string name;

        public JsonFileConfigureOptions(string name, string path, IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
            this.name = name;
            this.path = path;
        }

        public void Configure(string name, TOptions options)
        {
            if (name != null && name != this.name)
            {
                return;
            }

            byte[] bytes;
            using (var stream = fileProvider.GetFileInfo(path).CreateReadStream())
            {
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
            }

            var contents = Encoding.Default.GetString(bytes);
            contents = contents.Substring(contents.IndexOf('{'));
            var newOptions = JsonConvert.DeserializeObject<TOptions>(contents);
            Bind(newOptions, options);
        }

        public void Configure(TOptions options) => Configure(Options.DefaultName, options);

        private void Bind(object from, object to)
        {
            var type = from.GetType();

            if (type.IsDictionary())
            {
                var dest = (IDictionary)to;
                var src = (IDictionary)from;
                foreach (var key in src.Keys)
                {
                    dest.Add(key, src[key]);
                }
                return;
            }

            if (type.IsCollection())
            {
                var dest = (IList)to;
                var src = (IList)from;
                foreach (var item in src)
                {
                    dest.Add(item);
                }
            }

            foreach (var property in type.GetProperties())
            {
                if (property.IsSpecialName || property.GetMethod == null || property.Name == "Item" || property.DeclaringType != type)
                {
                    continue;
                }

                var src = property.GetValue(from);
                var propertyType = src?.GetType() ?? property.PropertyType;

                if ((propertyType.IsValueType || src is string || src == null) && property.SetMethod != null)
                {
                    property.SetValue(to, src);
                    continue;
                }

                var dest = property.GetValue(to);
                if (null != dest && !propertyType.IsArray())
                {
                    Bind(src, dest);
                    continue;
                }

                if (property.SetMethod != null)
                {
                    var destType = propertyType.IsDictionary()
                        ? typeof(Dictionary<,>).MakeGenericType(propertyType.GetGenericArguments())
                        : propertyType.IsArray()
                                ? typeof(List<>).MakeGenericType(propertyType.GetElementType())
                                : propertyType.IsCollection()
                                        ? typeof(List<>).MakeGenericType(propertyType.GetGenericArguments())
                                        : propertyType;

                    dest = Activator.CreateInstance(destType);
                    Bind(src, dest);

                    if (propertyType.IsArray())
                    {
                        IList list = (IList)dest;
                        dest = Array.CreateInstance(propertyType.GetElementType(), list.Count);
                        list.CopyTo((Array)dest, 0);
                    }
                    property.SetValue(to, src);
                }
            }
        }
    }

    internal static class Extentions
    {
        public static bool IsDictionary(this Type type) => type.IsGenericType && typeof(IDictionary).IsAssignableFrom(type) && type.GetGenericArguments().Length == 2;

        public static bool IsCollection(this Type type) => typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);

        public static bool IsArray(this Type type) => typeof(Array).IsAssignableFrom(type);
    }
}
