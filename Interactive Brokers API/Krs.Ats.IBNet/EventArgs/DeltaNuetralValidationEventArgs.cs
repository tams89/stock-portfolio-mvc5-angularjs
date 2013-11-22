// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeltaNuetralValidationEventArgs.cs" company="">
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
    public class DeltaNuetralValidationEventArgs : EventArgs
    {
        /// <summary>
        /// The request id.
        /// </summary>
        private int requestId;

        /// <summary>
        /// The under comp.
        /// </summary>
        private UnderComp underComp;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeltaNuetralValidationEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">
        /// Request Id
        /// </param>
        /// <param name="underComp">
        /// Underlying Component
        /// </param>
        public DeltaNuetralValidationEventArgs(int requestId, UnderComp underComp)
        {
            this.requestId = requestId;
            this.underComp = underComp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeltaNuetralValidationEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public DeltaNuetralValidationEventArgs()
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

        /// <summary>
        /// Underlying Component
        /// </summary>
        public UnderComp UnderComp
        {
            get { return underComp; }
			set { underComp = value; }
		}
    }
}