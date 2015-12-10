using AlgoTrader.Core.DTO;
using AlgoTrader.YahooApi;
using AutoMapper;

namespace AlgoTrader.Core.AutoMapper
{
    /// <summary>
    /// The auto mapper config.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// The configure.
        /// </summary>
        public static void Configure()
        {
            Mapper.CreateMap<Options.OptionsData, OptionDto>()
                .ForMember(x => x.BlackScholes, r => r.Ignore())
                .ForMember(x => x.Volatility, r => r.Ignore())
                ;

            Mapper.CreateMap<VolatilityAndMarketData.MarketData, MarketDto>();
        }
    }
}
