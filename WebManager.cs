using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DDOCrawler
{
    internal static class WebManager
    {
        private static readonly string url = "https://ordnet.dk/ddo/ordbog?query=";

        public static async Task<IDocument> DownloadDocument(string query)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            var document = await context.OpenAsync($"{url}{query}");
            if (document == null)
            {
                throw new Exception("Document is null");
            }



            return document;
        }



    }
}
