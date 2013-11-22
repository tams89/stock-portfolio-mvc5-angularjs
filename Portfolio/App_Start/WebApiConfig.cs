// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="">
//   
// </copyright>
// <summary>
//   The web api config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Portfolio.App_Start
{
    using System.Web.Http;

    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The web api config.
    /// </summary>
    public static class WebApiConfig
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public static void Register(HttpConfiguration config)
        {
            // config.Routes.MapHttpRoute(
            // name: "DefaultApi",
            // routeTemplate: "api/{controller}/{id}",
            // defaults: new { id = RouteParameter.Optional }
            // );
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            // config.EnableQuerySupport();
        }

        #endregion
    }
}