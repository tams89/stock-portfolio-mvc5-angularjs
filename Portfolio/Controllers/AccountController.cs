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
    using Filters;
    using Models;
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
            // Check validation attributes.
            if (!ModelState.IsValid)
            {
                return Json(GetErrorsFromModelState());
            }

            if (!WebSecurity.Login(model.UserName, model.Password))
            {
                return Json("The user name or password provided is incorrect.");
            }

            FormsAuthentication.SetAuthCookie(model.UserName, false);
            return Json(true);
        }

        /// <summary>
        /// The json register.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAngularPostHeader]
        public ActionResult JsonRegister(RegisterModel model)
        {
            // Check validation attributes.
            if (!ModelState.IsValid)
            {
                return Json(GetErrorsFromModelState());
            }

            // Attempt to register the user
            try
            {
                WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                WebSecurity.Login(model.UserName, model.Password);
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return Json(new { success = true });
            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError(string.Empty, ErrorCodeToString(e.StatusCode));
            }

            // If we got this far, something failed
            return Json("Oops, this is embarrassing...");
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
            if (HttpContext.Session != null)
            {
                HttpContext.Session.Abandon();
                HttpContext.Session.Clear();
            }

            return RedirectToAction("Index", "Main");
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
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #endregion
    }
}