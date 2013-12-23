namespace Core
{
    using AlgoTrader.YahooApi;
    using AutoMapper;
    using Core.DTO;

    /// <summary>
    /// The auto mapper config.
    /// </summary>
    public class AutoMapperConfig
    {
        /// <summary>
        /// The configure.
        /// </summary>
        public static void Configure()
        {
            Mapper.CreateMap<Options.OptionsData, OptionDto>()
                .ForMember(x => x.BlackScholes, r => r.Ignore())
                .ForMember(x => x.BlackScholesMonteCarlo, r => r.Ignore())
                .ForMember(x => x.Volatility, r => r.Ignore());

            Mapper.CreateMap<VolatilityAndMarketData.MarketData, MarketDto>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}
