using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace _Configuration_04
{
    class Program
    {
        static void Main(string[] args)
        {
            TryGetValue();
            CustomTypeConvert();
            ComplexTypeBind();
            IEnumerableBind();
            IEnumerableBindFailed();
        }

        private static void TryGetValue()
        {
            var source = new Dictionary<string, string>
            {
                ["foo"] = null,
                ["bar"] = "",
                ["baz"] = "123"
            };

            var root = new ConfigurationBuilder()
                .AddInMemoryCollection(source)
                .Build();

            // for object
            Debug.Assert(root.GetValue<object>("foo") == null);
            Debug.Assert("".Equals(root.GetValue<object>("bar")));
            Debug.Assert("123".Equals(root.GetValue<object>("baz")));

            // for general type
            Debug.Assert(root.GetValue<int>("foo") == 0);
            Debug.Assert(root.GetValue<int>("baz") == 123);

            // for Nullable<T>
            Debug.Assert(root.GetValue<int?>("foo") == null);
            Debug.Assert(root.GetValue<int?>("bar") == null);
        }

        private static void CustomTypeConvert()
        {
            var source = new Dictionary<string, string>
            {
                ["point"] = "(123,456)"
            };

            var root = new ConfigurationBuilder().AddInMemoryCollection(source).Build();

            var point = root.GetValue<Point>("point");

            Debug.Assert(point.X == 123);
            Debug.Assert(point.Y == 456);
        }

        private static void ComplexTypeBind()
        {
            var source = new Dictionary<string, string>
            {
                ["gender"] = "Male",
                ["age"] = "18",
                ["contactInfo:emailAddress"] = "foobar@outlook.com",
                ["contactInfo:phoneNo"] = "123456789"
            };

            var root = new ConfigurationBuilder().AddInMemoryCollection(source).Build();

            var profile = root.Get<Profile>();

            Debug.Assert(profile.Equals(new Profile(Gender.Male, 18, "foobar@outlook.com", "123456789")));
        }

        private static void IEnumerableBind()
        {
            var source = new Dictionary<string, string>
            {
                ["foo:gender"] = "Male",
                ["foo:age"] = "18",
                ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
                ["foo:contactInfo:phoneNo"] = "123",

                ["bar:gender"] = "Male",
                ["bar:age"] = "25",
                ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
                ["bar:contactInfo:phoneNo"] = "456",

                ["baz:gender"] = "Female",
                ["baz:age"] = "36",
                ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
                ["baz:contactInfo:phoneNo"] = "789"
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(source)
                .Build();

            var profiles = new Profile[]
            {
                new Profile(Gender.Male,18,"foo@outlook.com","123"),
                new Profile(Gender.Male,25,"bar@outlook.com","456"),
                new Profile(Gender.Female,36,"baz@outlook.com","789"),
            };

            var collection = configuration.Get<IEnumerable<Profile>>();
            Debug.Assert(collection.Any(it => it.Equals(profiles[0])));
            Debug.Assert(collection.Any(it => it.Equals(profiles[1])));
            Debug.Assert(collection.Any(it => it.Equals(profiles[2])));

            var array = configuration.Get<Profile[]>();
            Debug.Assert(array[0].Equals(profiles[1]));
            Debug.Assert(array[1].Equals(profiles[2]));
            Debug.Assert(array[2].Equals(profiles[0]));
        }

        private static void IEnumerableBindFailed()
        {
            var source = new Dictionary<string, string>
            {
                ["foo:gender"] = "男",
                ["foo:age"] = "18",
                ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
                ["foo:contactInfo:phoneNo"] = "123",

                ["bar:gender"] = "Male",
                ["bar:age"] = "25",
                ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
                ["bar:contactInfo:phoneNo"] = "456",

                ["baz:gender"] = "Female",
                ["baz:age"] = "36",
                ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
                ["baz:contactInfo:phoneNo"] = "789"
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(source)
                .Build();

            var collection = configuration.Get<IEnumerable<Profile>>();
            Debug.Assert(collection.Count() == 2);

            var array = configuration.Get<Profile[]>();
            Debug.Assert(array.Length == 3);
            Debug.Assert(array[2] == null);
            //由于配置节按照Key进行排序，绑定失败的配置节为最后一个
        }

        private static void IDictionaryBind()
        {
            var source = new Dictionary<string, string>
            {
                ["foo:gender"] = "Male",
                ["foo:age"] = "18",
                ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
                ["foo:contactInfo:phoneNo"] = "123",

                ["bar:gender"] = "Male",
                ["bar:age"] = "25",
                ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
                ["bar:contactInfo:phoneNo"] = "456",

                ["baz:gender"] = "Female",
                ["baz:age"] = "36",
                ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
                ["baz:contactInfo:phoneNo"] = "789"
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(source)
                .Build();

            var profiles = configuration.Get<IDictionary<string, Profile>>();
            Debug.Assert(profiles["foo"].Equals(new Profile(Gender.Male, 18, "foo@outlook.com", "123")));
            Debug.Assert(profiles["bar"].Equals(new Profile(Gender.Male, 25, "bar@outlook.com", "456")));
            Debug.Assert(profiles["baz"].Equals(new Profile(Gender.Female, 36, "baz@outlook.com", "789")));
        }

        [TypeConverter(typeof(PointTypeConverter))]
        class Point
        {
            public double X { get; set; }
            public double Y { get; set; }
        }

        class PointTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof(string);

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                string[] split = value.ToString().Split(',');
                double x = double.Parse(split[0].Trim().TrimStart('('));
                double y = double.Parse(split[1].Trim().TrimEnd(')'));
                return new Point { X = x, Y = y };
            }
        }
    }

    public class Profile : IEquatable<Profile>
    {
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public ContactInfo ContactInfo { get; set; }

        public Profile() { }
        public Profile(Gender gender, int age, string emailAddress, string phoneNo)
        {
            Gender = gender;
            Age = age;
            ContactInfo = new ContactInfo
            {
                EmailAddress = emailAddress,
                PhoneNo = phoneNo
            };
        }

        public bool Equals(Profile other)
        {
            return other == null ? false : Gender == other.Gender && Age == other.Age && ContactInfo.Equals(other.ContactInfo);
        }
    }

    public class ContactInfo : IEquatable<ContactInfo>
    {
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }

        public bool Equals(ContactInfo other)
        {
            return other == null ? false : EmailAddress == other.EmailAddress && PhoneNo == other.PhoneNo;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
