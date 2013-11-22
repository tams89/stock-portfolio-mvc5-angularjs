// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersContext.cs" company="">
//   
// </copyright>
// <summary>
//   The users context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Portfolio.Models
{
    using System.Data.Entity;

    /// <summary>
    /// The users context.
    /// </summary>
    public class UsersContext : DbContext
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersContext"/> class.
        /// </summary>
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the user profiles.
        /// </summary>
        public DbSet<UserProfile> UserProfiles { get; set; }

        #endregion
    }
}