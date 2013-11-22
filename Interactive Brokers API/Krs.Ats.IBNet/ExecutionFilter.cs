// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionFilter.cs" company="">
//   
// </copyright>
// <summary>
//   Argument passed to interactive brokers when requesting execution history.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Argument passed to interactive brokers when requesting execution history.
    /// </summary>
    [Serializable()]
    public class ExecutionFilter
    {
        #region Private Variables

        /// <summary>
        /// The acct code.
        /// </summary>
        private string acctCode;

        /// <summary>
        /// The client id.
        /// </summary>
        private int clientId;

        /// <summary>
        /// The exchange.
        /// </summary>
        private string exchange;

        /// <summary>
        /// The security type.
        /// </summary>
        private SecurityType securityType;

        /// <summary>
        /// The side.
        /// </summary>
        private ActionSide side;

        /// <summary>
        /// The symbol.
        /// </summary>
        private string symbol;

        /// <summary>
        /// The time.
        /// </summary>
        private DateTime time;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionFilter"/> class. 
        /// Default Constructor
        /// </summary>
        public ExecutionFilter()
        {
            securityType = SecurityType.Undefined;
            side = ActionSide.Undefined;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionFilter"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="clientId">
        /// Filter the results of the ReqExecutions() method based on the clientId.
        /// </param>
        /// <param name="acctCode">
        /// Filter the results of the ReqExecutions() method based on an account code.
        /// </param>
        /// <param name="time">
        /// Filter the results of the ReqExecutions() method based on execution reports received after the specified time.
        /// </param>
        /// <param name="symbol">
        /// Filter the results of the ReqExecutions() method based on the order symbol.
        /// </param>
        /// <param name="securityType">
        /// Refer to the Contract struct for the list of valid security types.
        /// </param>
        /// <param name="exchange">
        /// Filter the results of the ReqExecutions() method based on the order exchange.
        /// </param>
        /// <param name="side">
        /// Filter the results of the ReqExecutions() method based on the order action.
        /// </param>
        public ExecutionFilter(int clientId, string acctCode, DateTime time, string symbol, SecurityType securityType, 
                               string exchange, ActionSide side)
        {
            this.clientId = clientId;
            this.acctCode = acctCode;
            this.time = time;
            this.symbol = symbol;
            this.securityType = securityType;
            this.exchange = exchange;
            this.side = side;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on the clientId.
        /// </summary>
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on an account code.
        /// </summary>
        /// <remarks>This is only relevant for Financial Advisor (FA) accounts.</remarks>
        public string AcctCode
        {
            get { return acctCode; }
            set { acctCode = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on execution reports received after the specified time. 
        /// </summary>
        /// <remarks>The format for timeFilter is "yyyymmdd-hh:mm:ss"</remarks>
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on the order symbol.
        /// </summary>
        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on the order security type. 
        /// </summary>
        /// <remarks>Refer to the Contract struct for the list of valid security types.</remarks>
        public SecurityType SecurityType
        {
            get { return securityType; }
            set { securityType = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on the order exchange.
        /// </summary>
        public string Exchange
        {
            get { return exchange; }
            set { exchange = value; }
        }

        /// <summary>
        /// Filter the results of the ReqExecutions() method based on the order action. 
        /// </summary>
        /// <remarks>Refer to the Order struct for the list of valid order actions.</remarks>
        public ActionSide Side
        {
            get { return side; }
            set { side = value; }
        }

        #endregion
    }
}