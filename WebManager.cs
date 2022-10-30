using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DDOAPI
{
    internal static class WebManager
    {
        private static readonly string url = "https://ordnet.dk/ddo/ordbog?query=";

        public static string DownloadString(string query)
        {
            // Downloads a source file from a webpage 


            // best practice to create one HttpClient per Application and inject it
            HttpClient client = new HttpClient();

            using (HttpResponseMessage response = client.GetAsync(url+query).Result)
            {
                using (HttpContent content = response.Content)
                {
                    return content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}
