using System.Web.Security;
using WebMatrix.WebData;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Portfolio.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.UsersContext context)
        {
            WebSecurity.InitializeDatabaseConnection(
                        "DefaultConnection",
                        "UserProfile",
                        "UserId",
                        "UserName", autoCreateTables: true);

            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!WebSecurity.UserExists("tamesiva89"))
                WebSecurity.CreateUserAndAccount("tamesiva89", "password", true);

            if (!Roles.GetRolesForUser("tamesiva89").Contains("Administrator"))
                Roles.AddUsersToRoles(new[] { "tamesiva89" }, new[] { "Administrator" });
        }
    }
}