using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;

using System;
using System.Globalization;

namespace _Options_02
{
    class Program
    {
        static void Main(string[] args)
        {
            //OptiosBindDemo();
            //OptionsNameBindDemo();

            //CmdOptions(args);
            OptionsValidate(args);
        }

        private static void OptionsValidate(string[] args)
        {
            var configure = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var datePattern = configure["date"];
            var timePattern = configure["time"];

            var services = new ServiceCollection();
            services
                .AddOptions<DateTimeFormatOptions>()
                .Configure(options =>
                {
                    options.DatePattern = datePattern;
                    options.TimePattern = timePattern;
                })
                .Validate(options => Validate(options.DatePattern) && Validate(options.TimePattern), "Invalid Date or Time pattern.");

            try
            {
                var options = services.BuildServiceProvider()
                    .GetRequiredService<IOptions<DateTimeFormatOptions>>().Value;
                Console.WriteLine(options);
            }
            catch (OptionsValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            static bool Validate(string format)
            {
                var time = new DateTime(1981, 8, 24, 2, 2, 2);
                var formatted = time.ToString(format);
                return DateTimeOffset.TryParseExact(formatted, format, null, DateTimeStyles.None, out var value) && (value.Date == time.Date || value.TimeOfDay == time.TimeOfDay);
            }
        }

        private static void CmdOptions(string[] args)
        {
            var environment = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build()["env"];
            var services = new ServiceCollection();
            services
                .AddSingleton<IHostEnvironment>(new HostingEnvironment { EnvironmentName = environment })
                .AddOptions<DateTimeFormatOptions>()
                .Configure<IHostEnvironment>((options, env) =>
                {
                    if (env.IsDevelopment())
                    {
                        options.DatePattern = "dddd, MMMM d, yyyy";
                        options.TimePattern = "M/d/yyyy";
                    }
                    else
                    {
                        options.DatePattern = "M/d/yyyy";
                        options.TimePattern = "h:mm tt";
                    }
                });

            var options = services.BuildServiceProvider()
                .GetRequiredService<IOptions<DateTimeFormatOptions>>().Value;

            Console.WriteLine(options);
        }

        private static void OptionsNameBindDemo()
        {
            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>("foo", i =>
                {
                    i.Gender = Gender.Male;
                    i.Age = 18;
                    i.ContactInfo = new ContactInfo
                    {
                        PhoneNo = "123",
                        EmailAddress = "foo@outlook.com"
                    };
                })
                .Configure<Profile>("bar", i =>
                {
                    i.Gender = Gender.Female;
                    i.Age = 25;
                    i.ContactInfo = new ContactInfo
                    {
                        PhoneNo = "456",
                        EmailAddress = "bar@outlook.com"
                    };
                })
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

        private static void OptiosBindDemo()
        {
            var profile = new ServiceCollection()
                            .AddOptions()
                            .Configure<Profile>(i =>
                            {
                                i.Gender = Gender.Male;
                                i.Age = 18;
                                i.ContactInfo = new ContactInfo { EmailAddress = "foo@outlook.com", PhoneNo = "123456" };
                            })
                            .BuildServiceProvider()
                            .GetRequiredService<IOptions<Profile>>()
                            .Value;

            Console.WriteLine($"Gender: {profile.Gender}");
            Console.WriteLine($"Age: {profile.Age}");
            Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
            Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
        }
    }
}
