// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
//   
// </copyright>
// <summary>
//   The extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Core.Utilities
{
    using Roslyn.Compilers.CSharp;

    /// <summary>
    /// The extensions.
    /// </summary>
    public class Extensions
    {
        /// <summary>
        /// The empty collection if null or empty.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<T> EmptyCollectionIfNullOrEmpty<T>(this string str) where T : class, new()
        {
            return string.IsNullOrEmpty(str) ? Enumerable.Empty<T>() : false;
        }
    }
}