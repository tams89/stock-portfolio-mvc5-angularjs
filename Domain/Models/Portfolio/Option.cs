using System;
using System.Globalization;

namespace Core.Models.Portfolio
{
    public class Option : EntityBase<Option>
    {
        public string Symbol { get; set; }

        public string Type { get; set; }

        public decimal StrikePrice { get; set; }

        public decimal LastPrice { get; set; }

        public decimal Change { get; set; }

        public string ChangeDirection { get; set; }

        public decimal Bid { get; set; }

        public decimal Ask { get; set; }

        public int Vol { get; set; }

        public int OpenInt { get; set; }

        public string ExpiryDate
        {
            get
            {
                string year, month, day;
                // Indicates that the option is a mini-option
                if (Symbol.Substring(4, 1) == "7")
                {
                    year = Symbol.Substring(4 + 1, 2);
                    month = Symbol.Substring(6 + 1, 2);
                    day = Symbol.Substring(8 + 1, 2);
                }
                // Option is a normal option
                else
                {
                    year = Symbol.Substring(4, 2);
                    month = Symbol.Substring(6, 2);
                    day = Symbol.Substring(8, 2);
                }
                var date = DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, year), "dd/MM/yy", CultureInfo.InvariantCulture);
                return date.ToShortDateString();
            }
        }

        /// <summary>
        /// The amount by which the ask price exceeds the bid. This is essentially the difference
        /// in price between the highest price that a buyer is willing to pay for an asset and
        /// the lowest price for which a seller is willing to sell it.
        /// </summary>
        public decimal BidAskSpread
        {
            get { return Ask - Bid; }
        }

        public override string ToString()
        {
            return string.Format(
                "Symbol: {0}, Type: {1}, StrikePrice: {2}, LastPrice: {3}, Change: {4}, Bid: {5}, Ask: {6}, Vol: {7}",
                Symbol, Type, StrikePrice, LastPrice, Change, Bid, Ask, Vol);
        }
    }
}