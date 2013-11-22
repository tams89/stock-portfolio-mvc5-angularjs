// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionClosedEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Connection Closed Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Connection Closed Event Arguments
    /// </summary>
    [Serializable()]
    public class ConnectionClosedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionClosedEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ConnectionClosedEventArgs()
        {
            
        }
    }
}