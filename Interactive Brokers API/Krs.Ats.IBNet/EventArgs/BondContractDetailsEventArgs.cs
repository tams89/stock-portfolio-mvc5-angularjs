// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BondContractDetailsEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Bond Contract Details Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Bond Contract Details Event Arguments
    /// </summary>
    [Serializable()]
    public class BondContractDetailsEventArgs : EventArgs
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
        /// Initializes a new instance of the <see cref="BondContractDetailsEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">
        /// Request Id
        /// </param>
        /// <param name="contractDetails">
        /// This structure contains a full description of the bond contract being looked up.
        /// </param>
        public BondContractDetailsEventArgs(int requestId, ContractDetails contractDetails)
        {
            this.requestId = requestId;
            this.contractDetails = contractDetails;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BondContractDetailsEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public BondContractDetailsEventArgs()
        {
            
        }

        /// <summary>
        /// This structure contains a full description of the bond contract being looked up.
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