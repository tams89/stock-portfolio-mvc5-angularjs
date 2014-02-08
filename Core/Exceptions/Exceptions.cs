using System;

namespace Core.Exceptions
{
    /// <summary>
    /// Invalid Symbol Exception.
    /// </summary>
    public class InvalidSymbolException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public InvalidSymbolException()
            : base("An invalid symbol was specified.")
        {

        }
    }
}