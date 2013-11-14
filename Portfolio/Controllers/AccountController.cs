using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Portfolio.Filters;
using Portfolio.Models;
using WebMatrix.WebData;

namespace Portfolio.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        /// <summary>
        /// POST: /Account/JsonLogin
        /// </summary>
        /// <seealso cref="InitializeSimpleMembershipAttribute" /> for implementation details.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAngularPostHeader]
        public ActionResult JsonLogin(LoginModel model)
        {
            if (!WebSecurity.Login(model.UserName, model.Password)) return Json("The user name or password provided is incorrect.");
            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            return Json(true);
        }

        //
        // POST: /Account/LogOff
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

        //
        // POST: /Account/JsonRegister
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
                FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                return Json(new { success = true, redirect = returnUrl });
            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
            }
            // If we got this far, something failed
            return Json(GetErrorsFromModelState());
        }

        #region Helpers

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

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
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}