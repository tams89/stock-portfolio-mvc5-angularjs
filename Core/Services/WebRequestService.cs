// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebRequestService.cs" company="">
//   
// </copyright>
// <summary>
//   The web request service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using AlgoTrader.Core.Services.Interfaces;

namespace AlgoTrader.Core.Services
{
    /// <summary>
    /// The web request service.
    /// </summary>
    public class WebRequestService : IWebRequestService
    {
        /// <summary>
        ///     The create web client.
        /// </summary>
        /// <returns>
        ///     The <see cref="WebClient" />.
        /// </returns>
        private WebClient CreateWebClient()
        {
            WebRequest.DefaultWebProxy = null;
            var client = new WebClient();
            return client;
        }

        /// <summary>
        /// The get response.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
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