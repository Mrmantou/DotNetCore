//https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/attributes

using System;

namespace HelloWorld
{
    //This attibute that can be placed on program entities to provide links to their associated documentation.
    public class HelpAttribute : Attribute
    {
        string url;
        string topic;
        public HelpAttribute(string url)
        {
            this.url = url;
        }

        public string Url => url;

        public string Topic
        {
            get { return topic; }
            set { topic = value; }
        }
    }
}