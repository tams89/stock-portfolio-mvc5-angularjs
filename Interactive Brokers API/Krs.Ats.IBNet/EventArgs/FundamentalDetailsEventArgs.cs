// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FundamentalDetailsEventArgs.cs" company="">
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
    public class FundamentalDetailsEventArgs : EventArgs
    {
        /// <summary>
        /// The data.
        /// </summary>
        private string data;

        /// <summary>
        /// The request id.
        /// </summary>
        private int requestId;

        /// <summary>
        /// Initializes a new instance of the <see cref="FundamentalDetailsEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">
        /// Request Id
        /// </param>
        /// <param name="data">
        /// Xml Data
        /// </param>
        public FundamentalDetailsEventArgs(int requestId, string data)
        {
            this.requestId = requestId;
            this.data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FundamentalDetailsEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public FundamentalDetailsEventArgs()
        {
            
        }

        /// <summary>
        /// Xml Data
        /// </summary>
        public string Data
        {
            get { return data; }
			set { data = value; }
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