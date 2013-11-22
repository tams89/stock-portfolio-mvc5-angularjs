// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarketDataTypeEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   MarketDataType event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// MarketDataType event args.
    /// </summary>
    [Serializable()]
    public class MarketDataTypeEventArgs : EventArgs
    {
        /// <summary>
        /// The _request id.
        /// </summary>
        private int _requestId;

        /// <summary>
        /// The _market data type.
        /// </summary>
        private MarketDataType _marketDataType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketDataTypeEventArgs"/> class. 
        /// Constructor
        /// </summary>
        /// <param name="requestId">
        /// The requestId.
        /// </param>
        /// <param name="marketDataType">
        /// Real-time only or frozen.
        /// </param>
        public MarketDataTypeEventArgs(int requestId, MarketDataType marketDataType)
        {
            _requestId = requestId;
            _marketDataType = marketDataType;
        }

        /// <summary>
        /// The market data type.
        /// </summary>
        public MarketDataType MarketDataType
        {
            get { return _marketDataType; }
            set { _marketDataType = value; }
        }

        /// <summary>
        /// The request Id.
        /// </summary>
        public int RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }
    }
}