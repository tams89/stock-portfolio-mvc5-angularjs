
namespace Core
{
    /// <summary>
    /// The constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Retrieve the first connection string in the app.config.
        /// </summary>
        public const string AlgoTradingDbConnectionStr = "Data Source=.;Initial Catalog=AlgorithmicTrading;Integrated Security=True";

        /// <summary>
        /// The google finance json api url request a company name or symbol as argument for q.
        /// </summary>
        internal const string GoogleFinanceJsonApiUrl = "http://www.google.com/finance/match?matchtype=matchall&ei=zhbaUIDlCKSWiAL8zwE&q=";

        /// <summary>
        /// U.S. Treasury Interest Rate (circa Feb 2014).
        /// </summary>
        public const double FedIntRate = 2.99 / 100;
    }
}