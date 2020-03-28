using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;

namespace _Options_01
{
    class Program
    {
        static void Main(string[] args)
        {
            OptiosBindDemo();
            Console.WriteLine();
            OptionsNameBindDemo();
            Console.WriteLine();
            OptiosBindDemoReload();
            OptionsNameBindDemoReload();

            Console.WriteLine("press any key to exit......");
            Console.ReadKey();

        }

        private static void OptionsNameBindDemo()
        {
            var configuration = new ConfigurationBuilder()
                            .AddJsonFile("profiles.json")
                            .Build();

            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>("foo", configuration.GetSection("foo"))
                .Configure<Profile>("bar", configuration.GetSection("bar"))
                .BuildServiceProvider();

            var optionsAccessor = serviceProvider.GetRequiredService<IOptionsSnapshot<Profile>>();

            Print(optionsAccessor.Get("foo"));
            Print(optionsAccessor.Get("bar"));

            static void Print(Profile profile)
            {
                Console.WriteLine($"Gender: {profile.Gender}");
                Console.WriteLine($"Age: {profile.Age}");
                Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
                Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
            }
        }

        private static void OptionsNameBindDemoReload()
        {
            var configuration = new ConfigurationBuilder()
                            .AddJsonFile("profiles.json", optional: false, reloadOnChange: true)
                            .Build();

            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>("foo", configuration.GetSection("foo"))
                .Configure<Profile>("bar", configuration.GetSection("bar"))
                .BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<Profile>>()
                .OnChange((profile, name) =>
                {
                    Console.WriteLine($"Name: {name}");
                    Console.WriteLine($"Gender: {profile.Gender}");
                    Console.WriteLine($"Age: {profile.Age}");
                    Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
                    Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
                });
        }

        private static void OptiosBindDemo()
        {
            var configuration = new ConfigurationBuilder()
                            .AddJsonFile("profile.json")
                            .Build();
            var profile = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>(configuration)
                .BuildServiceProvider()
                .GetRequiredService<IOptions<Profile>>()
                .Value;

            Console.WriteLine($"Gender: {profile.Gender}");
            Console.WriteLine($"Age: {profile.Age}");
            Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
            Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}");
        }

        private static void OptiosBindDemoReload()
        {
            var configuration = new ConfigurationBuilder()
                            .AddJsonFile("profile.json", optional: false, reloadOnChange: true)
                            .Build();
            var profile = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>(configuration)
                .BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<Profile>>()
                .OnChange(profile =>
                {
                    Console.WriteLine($"Gender: {profile.Gender}");
                    Console.WriteLine($"Age: {profile.Age}");
                    Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
                    Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
                });
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
