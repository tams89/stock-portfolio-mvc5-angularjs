// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScannerSubscription.cs" company="">
//   
// </copyright>
// <summary>
//   Scanner Subscription details to pass to Interactive Brokers
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Scanner Subscription details to pass to Interactive Brokers
    /// </summary>
    [Serializable()]
    public class ScannerSubscription
    {
        #region Private Variables

        /// <summary>
        /// The above price.
        /// </summary>
        private double abovePrice = double.MaxValue;

        /// <summary>
        /// The above volume.
        /// </summary>
        private int aboveVolume = int.MaxValue;

        /// <summary>
        /// The average option volume above.
        /// </summary>
        private int averageOptionVolumeAbove = int.MaxValue;

        /// <summary>
        /// The below price.
        /// </summary>
        private double belowPrice = double.MaxValue;

        /// <summary>
        /// The coupon rate above.
        /// </summary>
        private double couponRateAbove = double.MaxValue;

        /// <summary>
        /// The coupon rate below.
        /// </summary>
        private double couponRateBelow = double.MaxValue;

        /// <summary>
        /// The exclude convertible.
        /// </summary>
        private string excludeConvertible;

        /// <summary>
        /// The instrument.
        /// </summary>
        private string instrument;

        /// <summary>
        /// The location code.
        /// </summary>
        private string locationCode;

        /// <summary>
        /// The market cap above.
        /// </summary>
        private double marketCapAbove = double.MaxValue;

        /// <summary>
        /// The market cap below.
        /// </summary>
        private double marketCapBelow = double.MaxValue;

        /// <summary>
        /// The maturity date above.
        /// </summary>
        private string maturityDateAbove;

        /// <summary>
        /// The maturity date below.
        /// </summary>
        private string maturityDateBelow;

        /// <summary>
        /// The moody rating above.
        /// </summary>
        private string moodyRatingAbove;

        /// <summary>
        /// The moody rating below.
        /// </summary>
        private string moodyRatingBelow;

        /// <summary>
        /// The number of rows.
        /// </summary>
        private int numberOfRows = -1; // No row number specified

        /// <summary>
        /// The scan code.
        /// </summary>
        private string scanCode;

        /// <summary>
        /// The scanner setting pairs.
        /// </summary>
        private string scannerSettingPairs;

        /// <summary>
        /// The sp rating above.
        /// </summary>
        private string spRatingAbove;

        /// <summary>
        /// The sp rating below.
        /// </summary>
        private string spRatingBelow;

        /// <summary>
        /// The stock type filter.
        /// </summary>
        private string stockTypeFilter;

        #endregion

        #region Properties

        /// <summary>
        /// Defines the number of rows of data to return for a query.
        /// </summary>
        public int NumberOfRows
        {
            get { return numberOfRows; }
            set { numberOfRows = value; }
        }

        /// <summary>
        /// Defines the instrument type for the scan.
        /// </summary>
        public string Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }

        /// <summary>
        /// The location, currently the only valid location is US stocks.
        /// </summary>
        public string LocationCode
        {
            get { return locationCode; }
            set { locationCode = value; }
        }

        /// <summary>
        /// Can be left blank. 
        /// </summary>
        public string ScanCode
        {
            get { return scanCode; }
            set { scanCode = value; }
        }

        /// <summary>
        /// Filter out contracts with a price lower than this value.
        /// Can be left blank.
        /// </summary>
        public double AbovePrice
        {
            get { return abovePrice; }
            set { abovePrice = value; }
        }

        /// <summary>
        /// Filter out contracts with a price higher than this value.
        /// Can be left blank. 
        /// </summary>
        public double BelowPrice
        {
            get { return belowPrice; }
            set { belowPrice = value; }
        }

        /// <summary>
        /// Filter out contracts with a volume lower than this value.
        /// Can be left blank.
        /// </summary>
        public int AboveVolume
        {
            get { return aboveVolume; }
            set { aboveVolume = value; }
        }

        /// <summary>
        /// Can leave empty. 
        /// </summary>
        public int AverageOptionVolumeAbove
        {
            get { return averageOptionVolumeAbove; }
            set { averageOptionVolumeAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with a market cap lower than this value.
        /// Can be left blank.
        /// </summary>
        public double MarketCapAbove
        {
            get { return marketCapAbove; }
            set { marketCapAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with a market cap above this value.
        /// Can be left blank.
        /// </summary>
        public double MarketCapBelow
        {
            get { return marketCapBelow; }
            set { marketCapBelow = value; }
        }

        /// <summary>
        /// Filter out contracts with a Moody rating below this value.
        /// Can be left blank.
        /// </summary>
        public string MoodyRatingAbove
        {
            get { return moodyRatingAbove; }
            set { moodyRatingAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with a Moody rating above this value.
        /// Can be left blank.
        /// </summary>
        public string MoodyRatingBelow
        {
            get { return moodyRatingBelow; }
            set { moodyRatingBelow = value; }
        }

        /// <summary>
        /// Filter out contracts with an SP rating below this value.
        /// Can be left blank.
        /// </summary>
        public string SPRatingAbove
        {
            get { return spRatingAbove; }
            set { spRatingAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with an SP rating above this value.
        /// Can be left blank.
        /// </summary>
        public string SPRatingBelow
        {
            get { return spRatingBelow; }
            set { spRatingBelow = value; }
        }

        /// <summary>
        /// Filter out contracts with a maturity date earlier than this value.
        /// Can be left blank.
        /// </summary>
        public string MaturityDateAbove
        {
            get { return maturityDateAbove; }
            set { maturityDateAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with a maturity date later than this value.
        /// Can be left blank.
        /// </summary>
        public string MaturityDateBelow
        {
            get { return maturityDateBelow; }
            set { maturityDateBelow = value; }
        }

        /// <summary>
        /// Filter out contracts with a coupon rate lower than this value.
        /// Can be left blank.
        /// </summary>
        public double CouponRateAbove
        {
            get { return couponRateAbove; }
            set { couponRateAbove = value; }
        }

        /// <summary>
        /// Filter out contracts with a coupon rate higher than this value.
        /// Can be left blank.
        /// </summary>
        public double CouponRateBelow
        {
            get { return couponRateBelow; }
            set { couponRateBelow = value; }
        }

        /// <summary>
        /// Filter out convertible bonds.
        /// Can be left blank.
        /// </summary>
        public string ExcludeConvertible
        {
            get { return excludeConvertible; }
            set { excludeConvertible = value; }
        }

        /// <summary>
        /// Can leave empty. For example, a pairing "Annual, true" used on the
        /// "top Option Implied Vol % Gainers" scan would return annualized volatilities.
        /// </summary>
        public string ScannerSettingPairs
        {
            get { return scannerSettingPairs; }
            set { scannerSettingPairs = value; }
        }

        /// <summary>
        /// ALL (excludes nothing)
        /// STOCK (excludes ETFs)
        /// ETF (includes ETFs)
        /// </summary>
        public string StockTypeFilter
        {
            get { return stockTypeFilter; }
            set { stockTypeFilter = value; }
        }

        #endregion
    }
}