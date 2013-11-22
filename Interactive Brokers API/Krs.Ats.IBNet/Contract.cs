// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Contract.cs" company="">
//   
// </copyright>
// <summary>
//   Class to describe a financial security.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Class to describe a financial security.
    /// </summary>
    /// <seealso href="http://www.interactivebrokers.com/php/apiUsersGuide/apiguide/java/contract.htm">Interactive Brokers Contract Documentation</seealso>
    [Serializable()]
    public class Contract
    {
        #region Private Variables

        /// <summary>
        /// The combo legs.
        /// </summary>
        private Collection<ComboLeg> comboLegs = new Collection<ComboLeg>();

        /// <summary>
        /// The contract id.
        /// </summary>
        private int contractId;

        /// <summary>
        /// The combo legs description.
        /// </summary>
        private string comboLegsDescription; // received in open order version 14 and up for all combos

        /// <summary>
        /// The currency.
        /// </summary>
        private string currency;

        /// <summary>
        /// The exchange.
        /// </summary>
        private string exchange;

        /// <summary>
        /// The expiry.
        /// </summary>
        private string expiry;

        /// <summary>
        /// The include expired.
        /// </summary>
        private bool includeExpired; // can not be set to true for orders.

        /// <summary>
        /// The local symbol.
        /// </summary>
        private string localSymbol;

        /// <summary>
        /// The multiplier.
        /// </summary>
        private string multiplier;

        /// <summary>
        /// The sec id type.
        /// </summary>
        private SecurityIdType secIdType; // CUSIP;SEDOL;ISIN;RIC

        /// <summary>
        /// The sec id.
        /// </summary>
        private string secId;

        /// <summary>
        /// The primary exchange.
        /// </summary>
        private string primaryExchange;


// pick a non-aggregate (ie not the SMART exchange) exchange that the contract trades on.  DO NOT SET TO SMART.

        /// <summary>
        /// The right.
        /// </summary>
        private RightType right;

        /// <summary>
        /// The security type.
        /// </summary>
        private SecurityType securityType;

        /// <summary>
        /// The strike.
        /// </summary>
        private double strike;

        /// <summary>
        /// The symbol.
        /// </summary>
        private string symbol;

        /// <summary>
        /// The underlying component.
        /// </summary>
        private UnderlyingComponent underlyingComponent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Contract"/> class. 
        /// Undefined Contract Constructor
        /// </summary>
        public Contract() :
            this(
            0, null, SecurityType.Undefined, null, 0, RightType.Undefined, null, null, null, null, null, 
            SecurityIdType.None, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contract"/> class. 
        /// Futures Contract Constructor
        /// </summary>
        /// <param name="symbol">
        /// This is the symbol of the underlying asset.
        /// </param>
        /// <param name="exchange">
        /// The order destination, such as Smart.
        /// </param>
        /// <param name="securityType">
        /// This is the security type.
        /// </param>
        /// <param name="currency">
        /// Specifies the currency.
        /// </param>
        /// <param name="expiry">
        /// The expiration date. Use the format YYYYMM.
        /// </param>
        public Contract(string symbol, string exchange, SecurityType securityType, string currency, string expiry) :
            this(
            0, symbol, securityType, expiry, 0, RightType.Undefined, null, exchange, currency, null, null, 
            SecurityIdType.None, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contract"/> class. 
        /// Indice Contract Constructor
        /// </summary>
        /// <param name="symbol">
        /// This is the symbol of the underlying asset.
        /// </param>
        /// <param name="exchange">
        /// The order destination, such as Smart.
        /// </param>
        /// <param name="securityType">
        /// This is the security type.
        /// </param>
        /// <param name="currency">
        /// Specifies the currency.
        /// </param>
        public Contract(string symbol, string exchange, SecurityType securityType, string currency)
            :
                this(
                0, symbol, securityType, null, 0, RightType.Undefined, null, exchange, currency, null, null, 
                SecurityIdType.None, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contract"/> class. 
        /// Default Contract Constructor
        /// </summary>
        /// <param name="contractId">
        /// The unique contract identifier.
        /// </param>
        /// <param name="symbol">
        /// This is the symbol of the underlying asset.
        /// </param>
        /// <param name="securityType">
        /// This is the security type.
        /// </param>
        /// <param name="expiry">
        /// The expiration date. Use the format YYYYMM.
        /// </param>
        /// <param name="strike">
        /// The strike price.
        /// </param>
        /// <param name="right">
        /// Specifies a Put or Call.
        /// </param>
        /// <param name="multiplier">
        /// Allows you to specify a future or option contract multiplier.
        /// This is only necessary when multiple possibilities exist.
        /// </param>
        /// <param name="exchange">
        /// The order destination, such as Smart.
        /// </param>
        /// <param name="currency">
        /// Specifies the currency.
        /// </param>
        /// <param name="localSymbol">
        /// This is the local exchange symbol of the underlying asset.
        /// </param>
        /// <param name="primaryExchange">
        /// Identifies the listing exchange for the contract (do not list SMART).
        /// </param>
        /// <param name="secIdType">
        /// Security identifier, when querying contract details or when placing orders.
        /// </param>
        /// <param name="secId">
        /// Unique identifier for the secIdType.
        /// </param>
        public Contract(int contractId, string symbol, SecurityType securityType, string expiry, double strike, 
            RightType right, 
            string multiplier, string exchange, string currency, string localSymbol, string primaryExchange, 
            SecurityIdType secIdType, string secId)
        {
            this.contractId = contractId;
            this.symbol = symbol;
            this.securityType = securityType;
            this.expiry = expiry;
            this.strike = strike;
            this.right = right;
            this.multiplier = multiplier;
            this.exchange = exchange;

            this.currency = currency;
            this.localSymbol = localSymbol;
            this.primaryExchange = primaryExchange;
            this.secIdType = secIdType;
            this.secId = secId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contract"/> class. 
        /// Get a Contract by its unique contractId
        /// </summary>
        /// <param name="contractId">
        /// </param>
        public Contract(int contractId)
        {
            this.contractId = contractId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// This is the symbol of the underlying asset.
        /// </summary>
        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        /// <summary>
        /// This is the security type.
        /// </summary>
        /// <remarks>Valid security types are:
        /// <list type="bullet">
        /// <item>Stock</item>
        /// <item>Option</item>
        /// <item>Future</item>
        /// <item>Indice</item>
        /// <item>Option on Future</item>
        /// <item>Cash</item>
        /// <item>Bag</item>
        /// <item>Bond</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="IBNet.SecurityType"/>
        public SecurityType SecurityType
        {
            get { return securityType; }
            set { securityType = value; }
        }

        /// <summary>
        /// The expiration date. Use the format YYYYMM.
        /// </summary>
        public string Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }

        /// <summary>
        /// The strike price.
        /// </summary>
        public double Strike
        {
            get { return strike; }
            set { strike = value; }
        }

        /// <summary>
        /// Specifies a Put or Call.
        /// </summary>
        /// <remarks>Valid values are:
        /// <list type="bullet">
        /// <item>Put - the right to sell a security.</item>
        /// <item>Call - the right to buy a security.</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="RightType"/>
        public RightType Right
        {
            get { return right; }
            set { right = value; }
        }

        /// <summary>
        /// Allows you to specify a future or option contract multiplier.
        /// This is only necessary when multiple possibilities exist.
        /// </summary>
        public string Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        /// <summary>
        /// The order destination, such as Smart.
        /// </summary>
        public string Exchange
        {
            get { return exchange; }
            set { exchange = value; }
        }

        /// <summary>
        /// Specifies the currency.
        /// </summary>
        /// <remarks>
        /// Ambiguities may require that this field be specified,
        /// for example, when SMART is the exchange and IBM is being requested
        /// (IBM can trade in GBP or USD).  Given the existence of this kind of ambiguity,
        /// it is a good idea to always specify the currency.
        /// </remarks>
        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        /// <summary>
        /// This is the local exchange symbol of the underlying asset.
        /// </summary>
        public string LocalSymbol
        {
            get { return localSymbol; }
            set { localSymbol = value; }
        }

        /// <summary>
        /// Identifies the listing exchange for the contract (do not list SMART).
        /// </summary>
        public string PrimaryExchange
        {
            get { return primaryExchange; }
            set { primaryExchange = value; }
        }

        /// <summary>
        /// If set to true, contract details requests and historical data queries
        /// can be performed pertaining to expired contracts.
        /// 
        /// Historical data queries on expired contracts are limited to the
        /// last year of the contracts life, and are initially only supported for
        /// expired futures contracts,
        /// </summary>
        public bool IncludeExpired
        {
            get { return includeExpired; }
            set { includeExpired = value; }
        }

        /// <summary>
        /// Description for combo legs
        /// </summary>
        public string ComboLegsDescription
        {
            get { return comboLegsDescription; }
            set { comboLegsDescription = value; }
        }

        /// <summary>
        /// Dynamic memory structure used to store the leg definitions for this contract.
        /// </summary>
        public Collection<ComboLeg> ComboLegs
        {
            get { return comboLegs; }
            set { comboLegs = value; }
        }

        /// <summary>
        /// The unique contract identifier.
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// Underlying Component
        /// </summary>
        public UnderlyingComponent UnderlyingComponent
        {
            get { return underlyingComponent; }
            set { underlyingComponent = value; }
        }

        /// <summary>
        /// Security identifier, when querying contract details or when placing orders. Supported identifiers are:
        /// ISIN (Example: Apple: US0378331005)
        /// CUSIP (Example: Apple: 037833100)
        /// SEDOL (Consists of 6-AN + check digit. Example: BAE: 0263494)
        /// RIC (Consists of exchange-independent RIC Root and a suffix identifying the exchange. Example: AAPL.O for Apple on NASDAQ.)
        /// </summary>
        public SecurityIdType SecIdType
        {
            get { return secIdType; }
            set { secIdType = value; }
        }

        /// <summary>
        /// Unique identifier for the secIdType.
        /// </summary>
        public string SecId
        {
            get { return secId; }
            set { secId = value; }
        }

        #endregion
    }
}