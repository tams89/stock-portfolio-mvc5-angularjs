using System;

namespace Core.DTOs
{
    using Core.DTO;

    public class OptionDto : DtoBase
    {
        public string Symbol { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Type { get; set; }
        public decimal StrikePrice { get; set; }
        public decimal LastPrice { get; set; }
        public string ChangeDirection { get; set; }
        public decimal Change { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public int Vol { get; set; }
        public int OpenInt { get; set; }
        public decimal Close { get; set; }
    }
}
