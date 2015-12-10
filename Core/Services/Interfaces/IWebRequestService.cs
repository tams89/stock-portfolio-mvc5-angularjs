// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebRequestService.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IWebRequestService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace AlgoTrader.Core.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebRequestService
    {
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
        string GetResponse(string url);
    }
}