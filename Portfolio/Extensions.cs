using System.Web.Helpers;

namespace AlgoTrader.Portfolio
{
    /// <summary>
    /// Helpers for the Web Application.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The get anti forgery token.
        /// </summary>
        public static string GetAntiForgeryToken()
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }
    }
}