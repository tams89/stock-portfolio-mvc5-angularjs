// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GoogleFinanceAutoCompleteDto.cs" company="">
//   
// </copyright>
// <summary>
//   The google finance json.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Core.Models.Site
{
    /// <summary>
    /// The google finance json.
    /// </summary>
    public struct GoogleFinanceAutoCompleteDto
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        public string Symbol { get; set; }

        #endregion
    }
}