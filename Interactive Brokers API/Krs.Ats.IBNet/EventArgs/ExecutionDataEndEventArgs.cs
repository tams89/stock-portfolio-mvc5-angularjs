// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionDataEndEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Execution Data End Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Execution Data End Event Arguments
    /// </summary>
    [Serializable()]
    public class ExecutionDataEndEventArgs : EventArgs
    {
        /// <summary>
        /// The request id.
        /// </summary>
        private int requestId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionDataEndEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">
        /// Request Id
        /// </param>
        public ExecutionDataEndEventArgs(int requestId)
        {
            this.requestId = requestId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionDataEndEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ExecutionDataEndEventArgs()
        {
            
        }

        /// <summary>
        /// Request Id
        /// </summary>
        public int RequestId
        {
            get { return requestId; }
			set { requestId = value; }
		}
    }
}