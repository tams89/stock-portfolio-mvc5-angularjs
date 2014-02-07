using System.Security.Principal;

namespace Core.Services
{
    /// <summary>
    /// The service base.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ServiceBase<T> where T : class, new()
    {
        /// <summary>
        ///     Gets the authenticated user.
        /// </summary>
        protected string AuthenticatedUser
        {
            get
            {
                var currentIdent = WindowsIdentity.GetCurrent();
                return currentIdent != null ? currentIdent.Name : string.Empty;
            }
        }
    }
}