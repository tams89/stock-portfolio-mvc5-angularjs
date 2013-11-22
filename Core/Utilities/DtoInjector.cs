// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DtoInjector.cs" company="">
//   
// </copyright>
// <summary>
//   The dto injector.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Core.Factory;
using Omu.ValueInjecter;

namespace Core.Utilities
{
    /// <summary>
    /// The dto injector.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="DtoT">
    /// </typeparam>
    public class DtoInjector<T, DtoT> where DtoT : class, new()
    {
        /// <summary>
        /// The inject list.
        /// </summary>
        /// <param name="injectFrom">
        /// The inject from.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<DtoT> InjectList(IEnumerable<T> injectFrom)
        {
            var dto = DtoFactory<DtoT>.Create();
            return injectFrom.Select(obj => dto.InjectFrom(obj) as DtoT);
        }

        /// <summary>
        /// The inject.
        /// </summary>
        /// <param name="injectFrom">
        /// The inject from.
        /// </param>
        /// <returns>
        /// The <see cref="DtoT"/>.
        /// </returns>
        public static DtoT Inject(object injectFrom)
        {
            var dto = DtoFactory<DtoT>.Create();
            var injectedDto = dto.InjectFrom(injectFrom) as DtoT;
            return injectedDto;
        }
    }
}
