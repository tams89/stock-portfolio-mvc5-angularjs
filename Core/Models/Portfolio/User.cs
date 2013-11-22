// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="">
//   
// </copyright>
// <summary>
//   The user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Portfolio
{
    /// <summary>
    /// The user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        public User(Guid id, string userName)
        {
            UserId = id;
            UserName = userName;
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
    }
}
