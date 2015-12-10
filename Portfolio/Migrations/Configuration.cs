using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Security;
using AlgoTrader.Portfolio.Models;
using WebMatrix.WebData;

namespace AlgoTrader.Portfolio.Migrations
{
    /// <summary>
    /// The configuration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<UsersContext>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(UsersContext context)
        {
            WebSecurity.InitializeDatabaseConnection(
                "DefaultConnection",
                "UserProfile",
                "UserId",
                "UserName",
                autoCreateTables: true);

            if (!Roles.RoleExists("Administrator"))
            {
                Roles.CreateRole("Administrator");
            }

            if (!WebSecurity.UserExists("tamesiva89"))
            {
                WebSecurity.CreateUserAndAccount("tamesiva89", "password", true);
            }

            if (!Roles.GetRolesForUser("tamesiva89").Contains("Administrator"))
            {
                Roles.AddUsersToRoles(new[] { "tamesiva89" }, new[] { "Administrator" });
            }
        }

        #endregion
    }
}