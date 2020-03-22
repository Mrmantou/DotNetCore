using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _Configuration_09
{
    class Program
    {
        static void Main(string[] args)
        {
            var initialSetting = new Dictionary<string, string>
            {
                ["Gender"] = "Male",
                ["Age"] = "18",
                ["ContactInfo:EmailAddress"] = "foobar@outlook.com",
                ["ContactInfo:PhoneNo"] = "123456789"
            };

            var profile = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddDataBase("DefaultDb", initialSetting)
                .Build()
                .Get<Profile>();

            Debug.Assert(profile.Gender == Gender.Male);
            Debug.Assert(profile.Age == 18);
            Debug.Assert(profile.ContactInfo.EmailAddress == "foobar@outlook.com");
            Debug.Assert(profile.ContactInfo.PhoneNo == "123456789");
        }
    }
}
