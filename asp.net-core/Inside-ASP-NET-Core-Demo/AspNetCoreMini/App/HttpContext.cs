using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class HttpContext
    {
        public HttpRequest Request { get; }
        public HttpResponse Response { get; }

        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }
    }

    public class HttpRequest
    {
        private readonly IHttpRequestFeature feature;

        public Uri Url => feature.Url;
        public NameValueCollection Headers => feature.Headers;
        public Stream Body => feature.Body;

        public HttpRequest(IFeatureCollection features) => feature = features.Get<IHttpRequestFeature>();
    }

    public class HttpResponse
    {
        private readonly IHttpResponseFeature feature;

        public NameValueCollection Headers => feature.Headers;
        public Stream Body => feature.Body;
        public int StatusCode { get => feature.StatusCode; set => feature.StatusCode = value; }

        public HttpResponse(IFeatureCollection features) => feature = features.Get<IHttpResponseFeature>();
    }

    public static partial class Extensions
    {
        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}