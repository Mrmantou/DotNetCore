using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace App
{
    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        private readonly HttpListenerContext context;

        public HttpListenerFeature(HttpListenerContext context) => this.context = context;

        Uri IHttpRequestFeature.Url => context.Request.Url;
        NameValueCollection IHttpRequestFeature.Headers => context.Request.Headers;
        Stream IHttpRequestFeature.Body => context.Request.InputStream;
        
        NameValueCollection IHttpResponseFeature.Headers => context.Response.Headers;
        Stream IHttpResponseFeature.Body => context.Response.OutputStream;
        int IHttpResponseFeature.StatusCode { get { return context.Response.StatusCode; } set { context.Response.StatusCode = value; } }
    }
}
