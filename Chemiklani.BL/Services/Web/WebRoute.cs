using System;
using System.Collections.Generic;
using System.Text;

namespace iPodnik.BL.Services.Web
{
    public class WebRoute
    {
        public string AbsoluteUri { get; }

        public string RelativeUri { get; }


        public WebRoute(string baseUri, string relativeUri)
        {
            if (relativeUri.StartsWith("~/"))
            {
                relativeUri = relativeUri.Substring(1);
            }
            if (!relativeUri.StartsWith("/"))
            {
                relativeUri = "/" + relativeUri;
            }

            RelativeUri = relativeUri;
            AbsoluteUri = baseUri.TrimEnd('/') + RelativeUri;
        }
    }
}
