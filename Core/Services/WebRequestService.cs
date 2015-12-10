using System;
using System.Net;
using AlgoTrader.Core.Services.Interfaces;

namespace AlgoTrader.Core.Services
{
    public class WebRequestService : IWebRequestService
    {
        private WebClient CreateWebClient()
        {
            WebRequest.DefaultWebProxy = null;
            var client = new WebClient();
            return client;
        }

        public string GetResponse(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;
            using (var client = CreateWebClient())
            {
                try
                {
                    var response = client.DownloadString(url);
                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}