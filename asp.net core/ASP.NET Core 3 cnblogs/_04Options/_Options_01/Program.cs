using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace _Options_01
{
    class Program
    {
        static void Main(string[] args)
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
