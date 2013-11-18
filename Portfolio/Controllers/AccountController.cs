// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="">
//   
// </copyright>
// <summary>
//   The account controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Portfolio.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;

    using Portfolio.Filters;
    using Portfolio.Models;

    using WebMatrix.WebData;

    /// <summary>
    /// The account controller.
    /// </summary>
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        #region Enums

        /// <summary>
        /// The manage message id.
        /// </summary>
        public enum ManageMessageId
        {
            /// <summary>
            /// The change password success.
            /// </summary>
            ChangePasswordSuccess, 

            /// <summary>
            /// The set password success.
            /// </summary>
            SetPasswordSuccess, 

            /// <summary>
            /// The remove login success.
            /// </summary>
            RemoveLoginSuccess, 
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// POST: /Account/JsonLogin
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <seealso cref="InitializeSimpleMembershipAttribute"/>
        /// for implementation details.
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAngularPostHeader]
        public ActionResult JsonLogin(LoginModel model)
        {
            if (!WebSecurity.Login(model.UserName, model.Password))
            {
                return this.Json("The user name or password provided is incorrect.");
            }

            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            return this.Json(true);
        }

        /// <summary>
        /// The json register.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAngularPostHeader]
        public ActionResult JsonRegister(RegisterModel model, string returnUrl)
        {
            // Attempt to register the user
            try
            {
                WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                WebSecurity.Login(model.UserName, model.Password);
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return this.Json(new { success = true, redirect = returnUrl });
            }
            catch (MembershipCreateUserException e)
            {
                this.ModelState.AddModelError(string.Empty, ErrorCodeToString(e.StatusCode));
            }

            // If we got this far, something failed
            return this.Json(this.GetErrorsFromModelState());
        }

        /// <summary>
        /// The log off.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAngularPostHeader]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            if (this.HttpContext.Session != null)
            {
                this.HttpContext.Session.Abandon();
                this.HttpContext.Session.Clear();
            }

            return this.RedirectToAction("Index", "Main");
        }

        #endregion

        #region Methods

        /// <summary>
        /// The error code to string.
        /// </summary>
        /// <param name="createStatus">
        /// The create status.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return
                        "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return
                        "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return
                        "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return
                        "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        /// <summary>
        /// The get errors from model state.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<string> GetErrorsFromModelState()
        {
            return this.ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #endregion
    }
}