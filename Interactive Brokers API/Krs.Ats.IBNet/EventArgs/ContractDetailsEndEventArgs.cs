// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContractDetailsEndEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Contract Details Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract Details Event Arguments
    /// </summary>
    [Serializable()]
    public class ContractDetailsEndEventArgs : EventArgs
    {
        /// <summary>
        /// The request id.
        /// </summary>
        private int requestId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractDetailsEndEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">
        /// Request Id
        /// </param>
        public ContractDetailsEndEventArgs(int requestId)
        {
            this.requestId = requestId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractDetailsEndEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ContractDetailsEndEventArgs()
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