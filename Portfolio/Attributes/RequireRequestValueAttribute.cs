// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequireRequestValueAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   The require request value attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Portfolio.Attributes
{
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// The require request value attribute.
    /// </summary>
    public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RequireRequestValueAttribute"/> class.
        /// </summary>
        /// <param name="valueName">
        /// The value name.
        /// </param>
        public RequireRequestValueAttribute(string valueName)
        {
            ValueName = valueName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the value name.
        /// </summary>
        public string ValueName { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The is valid for request.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="methodInfo">
        /// The method info.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request[ValueName] != null;
        }

        #endregion
    }
}