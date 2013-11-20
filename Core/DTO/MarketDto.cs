using System;

namespace Core.DTOs
{
    using Core.DTO;

    public class MarketDto : DtoBase
    {
        public double AdjClose { get; set; }
        public double Close { get; set; }
        public DateTime Date { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public string Symbol { get; set; }
        public int Volume { get; set; }
    }
}
