// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateAntiForgeryToken.cs" company="">
//   
// </copyright>
// <summary>
//   The validate angular post header.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Portfolio
{
    using System;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;

    /// <summary>
    /// The validate angular post header.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateAngularPostHeader : FilterAttribute, IAuthorizationFilter
    {
        #region Public Methods and Operators

        /// <summary>
        /// The on authorization.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        /// <exception cref="HttpAntiForgeryException">
        /// </exception>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                ValidateRequestHeader(filterContext.HttpContext.Request);
            }
            catch (HttpAntiForgeryException)
            {
                throw new HttpAntiForgeryException("Anti-forgery token cookie not found");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The validate request header.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        private static void ValidateRequestHeader(HttpRequestBase request)
        {
            var cookieToken = string.Empty;
            var formToken = string.Empty;
            var tokenValue = request.Headers["RequestVerificationToken"];
            if (!string.IsNullOrEmpty(tokenValue))
            {
                var tokens = tokenValue.Split(':');
                if (tokens.Length == 2)
                {
                    cookieToken = tokens[0].Trim();
                    formToken = tokens[1].Trim();
                }
            }

            AntiForgery.Validate(cookieToken, formToken);
        }

        #endregion
    }
}