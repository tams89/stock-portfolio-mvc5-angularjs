// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContractDetailsEventArgs.cs" company="">
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
    public class ContractDetailsEventArgs : EventArgs
    {
        /// <summary>
        /// The contract details.
        /// </summary>
        private ContractDetails contractDetails;

        /// <summary>
        /// The request id.
        /// </summary>
        private int requestId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractDetailsEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">
        /// Request Id
        /// </param>
        /// <param name="contractDetails">
        /// This structure contains a full description of the contract being looked up.
        /// </param>
        public ContractDetailsEventArgs(int requestId, ContractDetails contractDetails)
        {
            this.requestId = requestId;
            this.contractDetails = contractDetails;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractDetailsEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ContractDetailsEventArgs()
        {
            
        }

        /// <summary>
        /// This structure contains a full description of the contract being looked up.
        /// </summary>
        public ContractDetails ContractDetails
        {
            get { return contractDetails; }
			set { contractDetails = value; }
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