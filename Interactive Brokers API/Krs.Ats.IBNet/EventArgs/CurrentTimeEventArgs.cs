// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentTimeEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Current Time Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Current Time Event Arguments
    /// </summary>
    [Serializable()]
    public class CurrentTimeEventArgs : EventArgs
    {
        /// <summary>
        /// The time.
        /// </summary>
        private DateTime time;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentTimeEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="time">
        /// Current system time on the server side
        /// </param>
        public CurrentTimeEventArgs(DateTime time)
        {
            this.time = time;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentTimeEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public CurrentTimeEventArgs()
        {
            
        }

        /// <summary>
        /// Current system time on the server side in UTC
        /// </summary>
        public DateTime Time
        {
            get { return time; }
			set { time = value; }
		}
    }
}