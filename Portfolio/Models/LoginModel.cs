// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="">
//   
// </copyright>
// <summary>
//   The login model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Portfolio.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The login model.
    /// </summary>
    public class LoginModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        #endregion
    }
}