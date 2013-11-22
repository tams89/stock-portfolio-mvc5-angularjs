// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HistoricalDataEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Historical Data Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Historical Data Event Arguments
    /// </summary>
    [Serializable()]
    public class HistoricalDataEventArgs : EventArgs
    {
        /// <summary>
        /// The close.
        /// </summary>
        private decimal close;

        /// <summary>
        /// The trades.
        /// </summary>
        private int trades;

        /// <summary>
        /// The date.
        /// </summary>
        private DateTime date;

        /// <summary>
        /// The has gaps.
        /// </summary>
        private bool hasGaps;

        /// <summary>
        /// The high.
        /// </summary>
        private decimal high;

        /// <summary>
        /// The low.
        /// </summary>
        private decimal low;

        /// <summary>
        /// The open.
        /// </summary>
        private decimal open;

        /// <summary>
        /// The request id.
        /// </summary>
        private int requestId;

        /// <summary>
        /// The volume.
        /// </summary>
        private int volume;

        /// <summary>
        /// The wap.
        /// </summary>
        private double wap;

        /// <summary>
        /// The record number.
        /// </summary>
        private int recordNumber;

        /// <summary>
        /// The record total.
        /// </summary>
        private int recordTotal;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalDataEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="requestId">
        /// The ticker Id of the request to which this bar is responding.
        /// </param>
        /// <param name="date">
        /// The date-time stamp of the start of the bar.
        /// The format is determined by the reqHistoricalData() formatDate parameter.
        /// </param>
        /// <param name="open">
        /// Bar opening price.
        /// </param>
        /// <param name="high">
        /// High price during the time covered by the bar.
        /// </param>
        /// <param name="low">
        /// Low price during the time covered by the bar.
        /// </param>
        /// <param name="close">
        /// Bar closing price.
        /// </param>
        /// <param name="volume">
        /// Volume during the time covered by the bar.
        /// </param>
        /// <param name="trades">
        /// When TRADES historical data is returned, represents the number of trades that
        /// occurred during the time period the bar covers.
        /// </param>
        /// <param name="wap">
        /// Weighted average price during the time covered by the bar.
        /// </param>
        /// <param name="hasGaps">
        /// Whether or not there are gaps in the data.
        /// </param>
        /// <param name="recordNumber">
        /// Current Record Number out of Record Total.
        /// </param>
        /// <param name="recordTotal">
        /// Total Records Returned by Historical Request.
        /// </param>
        public HistoricalDataEventArgs(int requestId, DateTime date, decimal open, decimal high, decimal low, decimal close, 
                                       int volume, int trades, double wap, bool hasGaps, int recordNumber, int recordTotal)
        {
            this.requestId = requestId;
            this.hasGaps = hasGaps;
            this.wap = wap;
            this.trades = trades;
            this.volume = volume;
            this.close = close;
            this.low = low;
            this.high = high;
            this.open = open;
            this.date = date;
            this.recordNumber = recordNumber;
            this.recordTotal = recordTotal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalDataEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public HistoricalDataEventArgs()
        {
            
        }

        /// <summary>
        /// The ticker Id of the request to which this bar is responding.
        /// </summary>
        public int RequestId
        {
            get { return requestId; }
			set { requestId = value; }
		}

        /// <summary>
        /// The date-time stamp of the start of the bar.
        /// The format is determined by the reqHistoricalData() formatDate parameter.
        /// </summary>
        public DateTime Date
        {
            get { return date; }
			set { date = value; }
		}

        /// <summary>
        /// Bar opening price.
        /// </summary>
        public decimal Open
        {
            get { return open; }
			set { open = value; }
		}

        /// <summary>
        /// High price during the time covered by the bar.
        /// </summary>
        public decimal High
        {
            get { return high; }
			set { high = value; }
		}

        /// <summary>
        /// Low price during the time covered by the bar.
        /// </summary>
        public decimal Low
        {
            get { return low; }
			set { low = value; }
		}

        /// <summary>
        /// Bar closing price.
        /// </summary>
        public decimal Close
        {
            get { return close; }
			set { close = value; }
		}

        /// <summary>
        /// Volume during the time covered by the bar.
        /// </summary>
        public int Volume
        {
            get { return volume; }
			set { volume = value; }
		}

        /// <summary>
        /// When TRADES historical data is returned, represents the number of trades that
        /// occurred during the time period the bar covers.
        /// </summary>
        public int Trades
        {
            get { return trades; }
			set { trades = value; }
		}

        /// <summary>
        /// Weighted average price during the time covered by the bar.
        /// </summary>
        public double Wap
        {
            get { return wap; }
			set { wap = value; }
		}

        /// <summary>
        /// Whether or not there are gaps in the data.
        /// </summary>
        public bool HasGaps
        {
            get { return hasGaps; }
			set { hasGaps = value; }
		}

        /// <summary>
        /// Current Record Number out of Record Total
        /// </summary>
        public int RecordNumber
        {
            get { return recordNumber; }
			set { recordNumber = value; }
		}

        /// <summary>
        /// Total records returned by query
        /// </summary>
        public int RecordTotal
        {
            get { return recordTotal; }
			set { recordTotal = value; }
		} 

    }
}
