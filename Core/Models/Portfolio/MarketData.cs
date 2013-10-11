using System;

namespace Core.Models.Portfolio
{
    public class MarketData : EntityBase<MarketData>
    {
        public string Symbol { get; set; }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date.Date; }
            set { _date = value; }
        }

        public double Open { get; set; }

        public double Close { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Volume { get; set; }

        public double AdjClose { get; set; }
    }
}