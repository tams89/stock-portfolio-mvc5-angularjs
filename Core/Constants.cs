// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="">
//   
// </copyright>
// <summary>
//   The constants.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Configuration;

namespace Core
{
    /// <summary>
    /// The constants.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// The google finance json api url request a company name or symbol as argument for q.
        /// </summary>
        internal const string GoogleFinanceJsonApiUrl = "http://www.google.com/finance/match?matchtype=matchall&ei=zhbaUIDlCKSWiAL8zwE&q=";

        /// <summary>
        /// AlgoTrader database connection string.
        /// </summary>
        public static string AlgoTraderDbConnection = ConfigurationManager.ConnectionStrings["AlgoTraderDb"].ConnectionString;
    }
}