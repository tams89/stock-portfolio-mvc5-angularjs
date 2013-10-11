namespace Core.Models.Portfolio
{
    public class CompanyData : EntityBase<CompanyData>
    {
        public string Symbol { get; set; }

        public string Ask { get; set; }

        public string Bid { get; set; }

        public string AverageDailyVolume { get; set; }

        public string AskRealTime { get; set; }

        public string BidRealTime { get; set; }

        public string BookValue { get; set; }

        public string Change { get; set; }

        public string Commision { get; set; }

        public string ChangeRealTime { get; set; }

        public string DividendShare { get; set; }

        public string LastTradeDate { get; set; }

        public string EarningsShare { get; set; }

        public string EPSEstimateCurrentYear { get; set; }

        public string EPSEstimateNextYear { get; set; }

        public string MarketCapitalization { get; set; }

        public string FiftydayMovingAverage { get; set; }

        public string TwoHundreddayMovingAverage { get; set; }

        public string Open { get; set; }

        public string PreviousClose { get; set; }

        public string PriceSales { get; set; }

        public string PriceBook { get; set; }

        public string PERatio { get; set; }

        public string PEGRatio { get; set; }

        public string PriceEPSEstimateCurrentYear { get; set; }

        public string PriceEPSEstimateNextYear { get; set; }

        public string ShortRatio { get; set; }

        public string OneyrTargetPrice { get; set; }

        public string Volume { get; set; }

        public string StockExchange { get; set; }

        public string DividendYield { get; set; }

        public override string ToString()
        {
            return string.Format("Symbol: {0}, Open: {1}, Close: {2}", Symbol, Open, PreviousClose);
        }
    }
}