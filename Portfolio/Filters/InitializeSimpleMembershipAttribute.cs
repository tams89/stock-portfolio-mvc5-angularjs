using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using AlgoTrader.Portfolio.Models;
using WebMatrix.WebData;

namespace AlgoTrader.Portfolio.Filters
{
    // See for implementation.
    // http://techbrij.com/angularjs-antiforgerytoken-asp-net-mvc
    // http://techbrij.com/angularjs-asp-net-mvc-username-check
    /// <summary>
    /// The initialize simple membership attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The _initializer.
        /// </summary>
        private static SimpleMembershipInitializer _initializer;

        /// <summary>
        /// The _initializer lock.
        /// </summary>
        private static object _initializerLock = new object();

        /// <summary>
        /// The _is initialized.
        /// </summary>
        private static bool _isInitialized;

        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        /// <summary>
        /// The simple membership initializer.
        /// </summary>
        private class SimpleMembershipInitializer
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SimpleMembershipInitializer"/> class.
            /// </summary>
            /// <exception cref="InvalidOperationException">
            /// </exception>
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);

                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection(
                        "DefaultConnection",
                        "UserProfile",
                        "UserId",
                        "UserName",
                        autoCreateTables: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        "The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588",
                        ex);
                }
            }
        }
    }
}