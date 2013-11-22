// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebRequestService.cs" company="">
//   
// </copyright>
// <summary>
//   The web request service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net;

namespace Core.Services
{
    /// <summary>
    ///     The web request service.
    /// </summary>
    public class WebRequestService
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
        public T GetResponse<T>(string url)
            where T : new()
        {

        }
    }
}