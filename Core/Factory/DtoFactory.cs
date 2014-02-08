﻿using Core.DTO;

namespace Core.Factory
{
    using System.Collections.Generic;

    /// <summary>
    /// Factory for creating DTO objects.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public static class DtoFactory<T> where T : DtoBase, new()
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Create()
        {
            return new T();
        }

        /// <summary>
        /// The create list.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<T> CreateList()
        {
            return new List<T>();
        }
    }
}
