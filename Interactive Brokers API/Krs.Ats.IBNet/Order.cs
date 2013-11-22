// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Order.cs" company="">
//   
// </copyright>
// <summary>
//   Order class passed to Interactive Brokers to place an order.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Order class passed to Interactive Brokers to place an order.
    /// </summary>
    [Serializable()]
    public class Order
    {
        #region Private Variables

        // main order fields
        /// <summary>
        /// The action.
        /// </summary>
        private ActionSide action;

        /// <summary>
        /// The all or none.
        /// </summary>
        private bool allOrNone;

        /// <summary>
        /// The auction strategy.
        /// </summary>
        private AuctionStrategy auctionStrategy; // 1=AUCTION_MATCH, 2=AUCTION_IMPROVEMENT, 3=AUCTION_TRANSPARENT

        /// <summary>
        /// The aux price.
        /// </summary>
        private decimal auxPrice;

        /// <summary>
        /// The basis points.
        /// </summary>
        private decimal basisPoints; // EFP orders only

        /// <summary>
        /// The basis points type.
        /// </summary>
        private int basisPointsType; // EFP orders only

        // extended order fields
        /// <summary>
        /// The block order.
        /// </summary>
        private bool blockOrder;

        /// <summary>
        /// The client id.
        /// </summary>
        private int clientId;

        /// <summary>
        /// The continuous update.
        /// </summary>
        private int continuousUpdate;

        /// <summary>
        /// The delta.
        /// </summary>
        private double delta;

        /// <summary>
        /// The delta neutral aux price.
        /// </summary>
        private double deltaNeutralAuxPrice;

        /// <summary>
        /// The delta neutral order type.
        /// </summary>
        private OrderType deltaNeutralOrderType;

        /// <summary>
        /// The designated location.
        /// </summary>
        private string designatedLocation; // set when slot=2 only.

        /// <summary>
        /// The delta neutral con id.
        /// </summary>
        private int deltaNeutralConId;

        /// <summary>
        /// The delta neutral settling firm.
        /// </summary>
        private string deltaNeutralSettlingFirm;

        /// <summary>
        /// The delta neutral clearing account.
        /// </summary>
        private string deltaNeutralClearingAccount;

        /// <summary>
        /// The delta neutral clearing intent.
        /// </summary>
        private string deltaNeutralClearingIntent;

        // HEDGE ORDERS ONLY
        /// <summary>
        /// The hedge type.
        /// </summary>
        private string hedgeType; // 'D' - delta, 'B' - beta, 'F' - FX, 'P' - pair

        /// <summary>
        /// The hedge param.
        /// </summary>
        private string hedgeParam; // beta value for beta hedge, ratio for pair hedge

        // SMART routing only
        /// <summary>
        /// The discretionary amt.
        /// </summary>
        private decimal discretionaryAmt;

        /// <summary>
        /// The display size.
        /// </summary>
        private int displaySize;

        /// <summary>
        /// The e trade only.
        /// </summary>
        private bool eTradeOnly;

        /// <summary>
        /// The opt out smart routing.
        /// </summary>
        private bool optOutSmartRouting;

        // Financial advisors only 
        /// <summary>
        /// The fa group.
        /// </summary>
        private string faGroup;

        /// <summary>
        /// The fa method.
        /// </summary>
        private FinancialAdvisorAllocationMethod faMethod;

        /// <summary>
        /// The fa percentage.
        /// </summary>
        private string faPercentage;

        /// <summary>
        /// The fa profile.
        /// </summary>
        private string faProfile;

        /// <summary>
        /// The firm quote only.
        /// </summary>
        private bool firmQuoteOnly;

        /// <summary>
        /// The good after time.
        /// </summary>
        private string goodAfterTime; // FORMAT: 20060505 08:00:00 {time zone}

        /// <summary>
        /// The good till date.
        /// </summary>
        private string goodTillDate; // FORMAT: 20060505 08:00:00 {time zone}

        /// <summary>
        /// The hidden.
        /// </summary>
        private bool hidden;

        /// <summary>
        /// The outside rth.
        /// </summary>
        private bool? outsideRth;

        /// <summary>
        /// The limit price.
        /// </summary>
        private decimal limitPrice;

        /// <summary>
        /// The min qty.
        /// </summary>
        private int minQty;

        /// <summary>
        /// The nbbo price cap.
        /// </summary>
        private decimal nbboPriceCap;

        /// <summary>
        /// The oca group.
        /// </summary>
        private string ocaGroup; // one cancels all group name

        /// <summary>
        /// The oca type.
        /// </summary>
        private OcaType ocaType; // 1 = CANCEL_WITH_BLOCK, 2 = REDUCE_WITH_BLOCK, 3 = REDUCE_NON_BLOCK

        // Institutional orders only
        /// <summary>
        /// The open close.
        /// </summary>
        private string openClose; // O=Open, C=Close

        /// <summary>
        /// The order id.
        /// </summary>
        private int orderId;

        /// <summary>
        /// The order ref.
        /// </summary>
        private string orderRef;

        /// <summary>
        /// The order type.
        /// </summary>
        private OrderType orderType;

        /// <summary>
        /// The origin.
        /// </summary>
        private OrderOrigin origin; // 0=Customer, 1=Firm

        /// <summary>
        /// The override percentage constraints.
        /// </summary>
        private bool overridePercentageConstraints;

        /// <summary>
        /// The parent id.
        /// </summary>
        private int parentId; // Parent order Id, to associate Auto STP or TRAIL orders with the original order.

        /// <summary>
        /// The percent offset.
        /// </summary>
        private double percentOffset; // REL orders only

        /// <summary>
        /// The perm id.
        /// </summary>
        private int permId;

        /// <summary>
        /// The reference price type.
        /// </summary>
        private int referencePriceType; // 1=Average, 2 = BidOrAsk

        /// <summary>
        /// The rule 80 a.
        /// </summary>
        private AgentDescription rule80A;


// Individual = 'I', Agency = 'A', AgentOtherMember = 'W', IndividualPTIA = 'J', AgencyPTIA = 'U', AgentOtherMemberPTIA = 'M', IndividualPT = 'K', AgencyPT = 'Y', AgentOtherMemberPT = 'N'


        /// <summary>
        /// The short sale slot.
        /// </summary>
        private ShortSaleSlot shortSaleSlot;

        // 1 if you hold the shares, 2 if they will be delivered from elsewhere.  Only for Action="SSHORT
        /// <summary>
        /// The exempt code.
        /// </summary>
        private int exemptCode; // Code for short sale exemption orders

        // BOX ORDERS ONLY
        /// <summary>
        /// The starting price.
        /// </summary>
        private decimal startingPrice;

        // pegged to stock or VOL orders
        /// <summary>
        /// The stock range lower.
        /// </summary>
        private double stockRangeLower;

        /// <summary>
        /// The stock range upper.
        /// </summary>
        private double stockRangeUpper;

        /// <summary>
        /// The stock ref price.
        /// </summary>
        private double stockRefPrice;

        /// <summary>
        /// The sweep to fill.
        /// </summary>
        private bool sweepToFill;

        /// <summary>
        /// The tif.
        /// </summary>
        private TimeInForce tif; // "Time in Force" - DAY, GTC, etc.

        /// <summary>
        /// The total quantity.
        /// </summary>
        private int totalQuantity;

        /// <summary>
        /// The trail stop price.
        /// </summary>
        private decimal trailStopPrice; // for TRAILLIMIT orders only

        /// <summary>
        /// The transmit.
        /// </summary>
        private bool transmit; // if false, order will be created but not transmited

        /// <summary>
        /// The trigger method.
        /// </summary>
        private TriggerMethod triggerMethod;

        // 0=Default, 1=Double_Bid_Ask, 2=Last, 3=Double_Last, 4=Bid_Ask, 7=Last_or_Bid_Ask, 8=Mid-point

        // VOLATILITY ORDERS ONLY
        /// <summary>
        /// The volatility.
        /// </summary>
        private double volatility;

        /// <summary>
        /// The volatility type.
        /// </summary>
        private VolatilityType volatilityType; // 1=daily, 2=annual

        // SCALE ORDERS ONLY
        /// <summary>
        /// The scale init level size.
        /// </summary>
        private int scaleInitLevelSize;

        /// <summary>
        /// The scale subs level size.
        /// </summary>
        private int scaleSubsLevelSize;

        /// <summary>
        /// The scale price increment.
        /// </summary>
        private decimal scalePriceIncrement;

        // Clearing info
        /// <summary>
        /// The account.
        /// </summary>
        private string account; // IB account

        /// <summary>
        /// The settling firm.
        /// </summary>
        private string settlingFirm;

        /// <summary>
        /// The clearing account.
        /// </summary>
        private string clearingAccount; // True beneficiary of the order

        /// <summary>
        /// The clearing intent.
        /// </summary>
        private string clearingIntent; // "" (Default), "IB", "Away", "PTA" (PostTrade)

        // ALGO ORDERS ONLY
        /// <summary>
        /// The algo strategy.
        /// </summary>
        private string algoStrategy;

        /// <summary>
        /// The algo params.
        /// </summary>
        private Collection<TagValue> algoParams;

        // What-if
        /// <summary>
        /// The what if.
        /// </summary>
        private bool whatIf;

        // Not Held
        /// <summary>
        /// The not held.
        /// </summary>
        private bool notHeld;

        // Smart combo routing params
        /// <summary>
        /// The smart combo routing params.
        /// </summary>
        private Collection<TagValue> smartComboRoutingParams;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class. 
        /// Default Constructor
        /// </summary>
        public Order()
        {
            openClose = "O";
            origin = OrderOrigin.Customer;
            transmit = true;
            tif = TimeInForce.Day;
            designatedLocation = string.Empty;
            minQty = int.MaxValue;
            percentOffset = double.MaxValue;
            nbboPriceCap = decimal.MaxValue;
            startingPrice = decimal.MaxValue;
            stockRefPrice = double.MaxValue;
            delta = double.MaxValue;
            stockRangeLower = double.MaxValue;
            stockRangeUpper = double.MaxValue;
            volatility = double.MaxValue;
            volatilityType = VolatilityType.Undefined;
            deltaNeutralOrderType = OrderType.Empty;
            deltaNeutralAuxPrice = double.MaxValue;
            referencePriceType = int.MaxValue;
            trailStopPrice = decimal.MaxValue;
            basisPoints = decimal.MaxValue;
            basisPointsType = int.MaxValue;
            scaleInitLevelSize = int.MaxValue;
            scaleSubsLevelSize = int.MaxValue;
            scalePriceIncrement = decimal.MaxValue;
            faMethod = FinancialAdvisorAllocationMethod.None;
            notHeld = false;
            exemptCode = -1;

            optOutSmartRouting = false;
            deltaNeutralConId = 0;
            deltaNeutralOrderType = OrderType.Empty;
            deltaNeutralSettlingFirm = string.Empty;
            deltaNeutralClearingAccount = string.Empty;
            deltaNeutralClearingIntent = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The id for this order.
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        /// <summary>
        /// The id of the client that placed this order.
        /// </summary>
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        /// <summary>
        /// The TWS id used to identify orders, remains the same over TWS sessions.
        /// </summary>
        public int PermId
        {
            get { return permId; }
            set { permId = value; }
        }

        /// <summary>
        /// Identifies the side. Valid values are: BUY, SELL, SSHORT
        /// </summary>
        public ActionSide Action
        {
            get { return action; }
            set { action = value; }
        }

        /// <summary>
        /// The order quantity.
        /// </summary>
        public int TotalQuantity
        {
            get { return totalQuantity; }
            set { totalQuantity = value; }
        }

        /// <summary>
        /// The order type.
        /// </summary>
        /// <seealso cref="OrderType"/>
        public OrderType OrderType
        {
            get { return orderType; }
            set { orderType = value; }
        }

        /// <summary>
        /// This is the LIMIT price, used for limit, stop-limit and relative orders.
        /// In all other cases specify zero. For relative orders with no limit price,
        /// also specify zero.
        /// </summary>
        public decimal LimitPrice
        {
            get { return limitPrice; }
            set { limitPrice = value; }
        }

        /// <summary>
        /// This is the STOP price for stop-limit orders, and the offset amount for
        /// relative orders. In all other cases, specify zero.
        /// </summary>
        public decimal AuxPrice
        {
            get { return auxPrice; }
            set { auxPrice = value; }
        }

        /// <summary>
        /// The time in force.
        /// </summary>
        /// <remarks>Valid values are: DAY, GTC, IOC, GTD.</remarks>
        /// <seealso cref="TimeInForce"/>
        public TimeInForce Tif
        {
            get { return tif; }
            set { tif = value; }
        }

        /// <summary>
        /// Identifies an OCA (one cancels all) group.
        /// </summary>
        public string OcaGroup
        {
            get { return ocaGroup; }
            set { ocaGroup = value; }
        }

        /// <summary>
        /// Tells how to handle remaining orders in an OCA group when one order or part of an order executes.
        /// </summary>
        /// <remarks>
        /// Valid values include:
        /// <list type="bullet">
        /// <item>1 = Cancel all remaining orders with block.</item>
        /// <item>2 = Remaining orders are proportionately reduced in size with block.</item>
        /// <item>3 = Remaining orders are proportionately reduced in size with no block.</item>
        /// </list>
        /// If you use a value "with block"gives your order has overfill protection. This means  that only one order in the group will be routed at a time to remove the possibility of an overfill.
        /// </remarks>
        /// <seealso cref="OcaType"/>
        public OcaType OcaType
        {
            get { return ocaType; }
            set { ocaType = value; }
        }

        /// <summary>
        /// The order reference. For institutional customers only.
        /// </summary>
        public string OrderRef
        {
            get { return orderRef; }
            set { orderRef = value; }
        }

        /// <summary>
        /// Specifies whether the order will be transmitted by TWS.
        /// If set to false, the order will be created at TWS but will not be sent.
        /// </summary>
        public bool Transmit
        {
            get { return transmit; }
            set { transmit = value; }
        }

        /// <summary>
        /// The order ID of the parent order, used for bracket and auto trailing stop orders.
        /// </summary>
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        /// <summary>
        /// If set to true, specifies that the order is an ISE Block order.
        /// </summary>
        public bool BlockOrder
        {
            get { return blockOrder; }
            set { blockOrder = value; }
        }

        /// <summary>
        /// If set to true, specifies that the order is a Sweep-to-Fill order.
        /// </summary>
        public bool SweepToFill
        {
            get { return sweepToFill; }
            set { sweepToFill = value; }
        }

        /// <summary>
        /// The publicly disclosed order size, used when placing Iceberg orders.
        /// </summary>
        public int DisplaySize
        {
            get { return displaySize; }
            set { displaySize = value; }
        }

        /// <summary>
        /// Specifies how Simulated Stop, Stop-Limit and Trailing Stop orders are triggered.
        /// </summary>
        /// <remarks>
        /// Valid values are:
        /// <list type="bullet">
        /// <item>0 - the default value. The "double bid/ask" method will be used for orders for OTC stocks and US options. All other orders will used the "last" method.</item>
        /// <item>1 - use "double bid/ask" method, where stop orders are triggered based on two consecutive bid or ask prices.</item>
        /// <item>2 - "last" method, where stop orders are triggered based on the last price.</item>
        /// <item>3 - double last method.</item>
        /// <item>4 - bid/ask method.</item>
        /// <item>7 - last or bid/ask method.</item>
        /// <item>8 - mid-point method.</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="TriggerMethod"/>
        public TriggerMethod TriggerMethod
        {
            get { return triggerMethod; }
            set { triggerMethod = value; }
        }

        /// <summary>
        /// If set to true, allows triggering of orders outside of regular trading hours.
        /// </summary>
        public bool? OutsideRth
        {
            get { return outsideRth; }
            set { outsideRth = value; }
        }

        /// <summary>
        /// If set to true, the order will not be visible when viewing the market depth.
        /// This option only applies to orders routed to the ISLAND exchange.
        /// </summary>
        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        /// <summary>
        /// The trade's "Good After Time"
        /// </summary>
        /// <remarks>format "YYYYMMDD hh:mm:ss (optional time zone)" 
        /// Use an empty String if not applicable.</remarks>
        public string GoodAfterTime
        {
            get { return goodAfterTime; }
            set { goodAfterTime = value; }
        }

        /// <summary>
        /// You must enter a Time in Force value of Good Till Date.
        /// </summary>
        /// <remarks>The trade's "Good Till Date," format is:
        /// YYYYMMDD hh:mm:ss (optional time zone)
        /// Use an empty String if not applicable.</remarks>
        public string GoodTillDate
        {
            get { return goodTillDate; }
            set { goodTillDate = value; }
        }

        /// <summary>
        /// If set, allows you to override TWS order price percentage constraints set to
        /// reject orders that deviate too far from the NBBO. This precaution was created
        /// to avoid transmitting orders with an incorrect price. 
        /// </summary>
        public bool OverridePercentageConstraints
        {
            get { return overridePercentageConstraints; }
            set { overridePercentageConstraints = value; }
        }

        /// <summary>
        /// This identifies what type of trader you are.
        /// </summary>
        /// <remarks>Rule80A required you to identify which type of trader you are.</remarks>
        /// <seealso cref="AgentDescription"/>
        public AgentDescription Rule80A
        {
            get { return rule80A; }
            set { rule80A = value; }
        }

        /// <summary>
        /// yes=1, no=0
        /// </summary>
        public bool AllOrNone
        {
            get { return allOrNone; }
            set { allOrNone = value; }
        }

        /// <summary>
        /// Identifies a minimum quantity order type.
        /// </summary>
        public int MinQty
        {
            get { return minQty; }
            set { minQty = value; }
        }

        /// <summary>
        /// The percent offset amount for relative orders.
        /// </summary>
        public double PercentOffset
        {
            get { return percentOffset; }
            set { percentOffset = value; }
        }

        /// <summary>
        /// For TRAILLIMIT orders only
        /// </summary>
        public decimal TrailStopPrice
        {
            get { return trailStopPrice; }
            set { trailStopPrice = value; }
        }

        /// <summary>
        /// The Financial Advisor group the trade will be allocated to -- use an empty String if not applicable.
        /// </summary>
        public string FAGroup
        {
            get { return faGroup; }
            set { faGroup = value; }
        }

        /// <summary>
        /// The Financial Advisor allocation profile the trade will be allocated to -- use an empty String if not applicable.
        /// </summary>
        public string FAProfile
        {
            get { return faProfile; }
            set { faProfile = value; }
        }

        /// <summary>
        /// The Financial Advisor allocation method the trade will be allocated with -- use an empty String if not applicable.
        /// </summary>
        public FinancialAdvisorAllocationMethod FAMethod
        {
            get { return faMethod; }
            set { faMethod = value; }
        }

        /// <summary>
        /// The Financial Advisor percentage concerning the trade's allocation -- use an empty String if not applicable.
        /// </summary>
        public string FAPercentage
        {
            get { return faPercentage; }
            set { faPercentage = value; }
        }

        /// <summary>
        /// Specifies whether the order is an open or close order.
        /// For institutional customers only. Valid values are O, C.
        /// </summary>
        public string OpenClose
        {
            get { return openClose; }
            set { openClose = value; }
        }

        /// <summary>
        /// The order origin.
        /// </summary>
        /// <remarks>For institutional customers only.</remarks>
        public OrderOrigin Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        /// <summary>
        /// ShortSaleSlot of Third Party requires DesignatedLocation to be specified. Non-empty DesignatedLocation values for all other cases will cause orders to be rejected.
        /// </summary>
        public ShortSaleSlot ShortSaleSlot
        {
            get { return shortSaleSlot; }
            set { shortSaleSlot = value; }
        }

        /// <summary>
        /// Use only when shortSaleSlot value = 2.
        /// </summary>
        public string DesignatedLocation
        {
            get { return designatedLocation; }
            set { designatedLocation = value; }
        }

        /// <summary>
        /// The amount off the limit price allowed for discretionary orders.
        /// </summary>
        public decimal DiscretionaryAmt
        {
            get { return discretionaryAmt; }
            set { discretionaryAmt = value; }
        }

        /// <summary>
        /// Trade with electronic quotes.
        /// </summary>
        public bool ETradeOnly
        {
            get { return eTradeOnly; }
            set { eTradeOnly = value; }
        }

        /// <summary>
        /// Trade with firm quotes.
        /// </summary>
        public bool FirmQuoteOnly
        {
            get { return firmQuoteOnly; }
            set { firmQuoteOnly = value; }
        }

        /// <summary>
        /// The maximum Smart order distance from the NBBO.
        /// </summary>
        public decimal NbboPriceCap
        {
            get { return nbboPriceCap; }
            set { nbboPriceCap = value; }
        }

        /// <summary>
        /// The auction strategy.
        /// </summary>
        /// <remarks>For BOX exchange only.</remarks>
        /// <seealso cref="AuctionStrategy"/>
        public AuctionStrategy AuctionStrategy
        {
            get { return auctionStrategy; }
            set { auctionStrategy = value; }
        }

        /// <summary>
        /// The starting price.
        /// </summary>
        /// <remarks>Valid on BOX orders only.</remarks>
        public decimal StartingPrice
        {
            get { return startingPrice; }
            set { startingPrice = value; }
        }

        /// <summary>
        /// The stock reference price.
        /// </summary>
        /// <remarks>The reference price is used for VOL orders
        /// to compute the limit price sent to an exchange (whether or not Continuous
        /// Update is selected), and for price range monitoring.</remarks>
        public double StockRefPrice
        {
            get { return stockRefPrice; }
            set { stockRefPrice = value; }
        }

        /// <summary>
        /// The stock delta.
        /// </summary>
        /// <remarks>Valid on BOX orders only.</remarks>
        public double Delta
        {
            get { return delta; }
            set { delta = value; }
        }

        /// <summary>
        /// The lower value for the acceptable underlying stock price range.
        /// </summary>
        /// <remarks>For price improvement option orders on BOX and VOL orders with dynamic management.</remarks>
        public double StockRangeLower
        {
            get { return stockRangeLower; }
            set { stockRangeLower = value; }
        }

        /// <summary>
        /// The upper value for the acceptable underlying stock price range.
        /// </summary>
        /// <remarks>For price improvement option orders on BOX and VOL orders with dynamic management.</remarks>
        public double StockRangeUpper
        {
            get { return stockRangeUpper; }
            set { stockRangeUpper = value; }
        }

        /// <summary>
        /// What the price is, computed via TWS's Options Analytics.
        /// </summary>
        /// <remarks>For VOL orders, the limit price sent to an exchange is not editable,
        /// as it is the output of a function.  Volatility is expressed as a percentage.</remarks>
        public double Volatility
        {
            get { return volatility; }
            set { volatility = value; }
        }

        /// <summary>
        /// How the volatility is calculated. 
        /// </summary>
        /// <seealso cref="VolatilityType"/>
        public VolatilityType VolatilityType
        {
            get { return volatilityType; }
            set { volatilityType = value; }
        }

        /// <summary>
        /// Used for dynamic management of volatility orders. 
        /// </summary>
        /// <remarks>Determines whether TWS is
        /// supposed to update the order price as the underlying moves.  If selected,
        /// the limit price sent to an exchange is modified by TWS if the computed price
        /// of the option changes enough to warrant doing so.  This is very helpful in
        /// keeping the limit price sent to the exchange up to date as the underlying price changes.</remarks>
        public int ContinuousUpdate
        {
            get { return continuousUpdate; }
            set { continuousUpdate = value; }
        }

        /// <summary>
        /// Used for dynamic management of volatility orders. Set to
        /// 1 = Average of National Best Bid or Ask, or set to
        /// 2 =  National Best Bid when buying a call or selling a put; and National Best Ask when selling a call or buying a put.
        /// </summary>
        public int ReferencePriceType
        {
            get { return referencePriceType; }
            set { referencePriceType = value; }
        }

        /// <summary>
        /// VOL orders only. Enter an order type to instruct TWS to submit a
        /// delta neutral trade on full or partial execution of the VOL order.
        /// For no hedge delta order to be sent, specify NONE.
        /// </summary>
        public OrderType DeltaNeutralOrderType
        {
            get { return deltaNeutralOrderType; }
            set { deltaNeutralOrderType = value; }
        }

        /// <summary>
        /// VOL orders only. Use this field to enter a value if  the value in the
        /// deltaNeutralOrderType field is an order type that requires an Aux price, such as a REL order. 
        /// </summary>
        public double DeltaNeutralAuxPrice
        {
            get { return deltaNeutralAuxPrice; }
            set { deltaNeutralAuxPrice = value; }
        }

        /// <summary>
        /// For EFP orders only
        /// </summary>
        public decimal BasisPoints
        {
            get { return basisPoints; }
            set { basisPoints = value; }
        }

        /// <summary>
        /// For EFP orders only
        /// </summary>
        public int BasisPointsType
        {
            get { return basisPointsType; }
            set { basisPointsType = value; }
        }

        /// <summary>
        /// split order into X buckets
        /// </summary>
        public int ScaleInitLevelSize
        {
            get { return scaleInitLevelSize; }
            set { scaleInitLevelSize = value; }
        }

        /// <summary>
        /// split order so each bucket is of the size X
        /// </summary>
        public int ScaleSubsLevelSize
        {
            get { return scaleSubsLevelSize; }
            set { scaleSubsLevelSize = value; }
        }

        /// <summary>
        /// price increment per bucket
        /// </summary>
        public decimal ScalePriceIncrement
        {
            get { return scalePriceIncrement; }
            set { scalePriceIncrement = value; }
        }

        /// <summary>
        /// The account. For institutional customers only.
        /// </summary>
        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        /// <summary>
        /// Institutional only.
        /// </summary>
        public string SettlingFirm
        {
            get { return settlingFirm; }
            set { settlingFirm = value; }
        }

        /// <summary>
        /// Unknown - assume institutional only.
        /// </summary>
        public string ClearingAccount
        {
            get { return clearingAccount; }
            set { clearingAccount = value; }
        }

        /// <summary>
        /// For IBExecution customers: Valid values are: 
        /// IB, Away, and PTA (post trade allocation).
        /// </summary>
        public string ClearingIntent
        {
            get { return clearingIntent; }
            set { clearingIntent = value; }
        }

        /// <summary>
        /// For information about API Algo orders, see http://www.interactivebrokers.com/en/software/api/apiguide/tables/ibalgo_parameters.htm.
        /// </summary>
        public string AlgoStrategy
        {
            get { return algoStrategy; }
            set { algoStrategy = value; }
        }

        /// <summary>
        /// Support for IBAlgo parameters.
        /// </summary>
        public Collection<TagValue> AlgoParams
        {
            get { return algoParams; }
            set { algoParams = value; }
        }

        /// <summary>
        /// When this value is set to true, margin and commission data is
        /// received back via a new OrderState() object for the openOrder() callback.
        /// </summary>
        public bool WhatIf
        {
            get { return whatIf; }
            set { whatIf = value; }
        }

        /// <summary>
        /// For IBDARK orders only. Orders routed to IBDARK are tagged as "post only" and 
        /// are held in IB's order book, where incoming SmartRouted orders from other IB customers 
        /// are eligible to trade against them.
        /// </summary>
        public bool NotHeld
        {
            get { return notHeld; }
            set { notHeld = value; }
        }

        /// <summary>
        /// Exempt Code for Short Sale Exemption Orders
        /// </summary>
        public int ExemptCode
        {
            get { return exemptCode; }
            set { exemptCode = value; }
        }

        /// <summary>
        /// Opt out of smart routing for directly routed ASX orders
        /// </summary>
        public bool OptOutSmartRouting
        {
            get { return optOutSmartRouting; }
            set { optOutSmartRouting = value; }
        }

        /// <summary>
        /// DeltaNeutralConId
        /// </summary>
        public int DeltaNeutralConId
        {
            get { return deltaNeutralConId; }
            set { deltaNeutralConId = value; }
        }

        /// <summary>
        /// DeltaNeutralSettlingFirm. Institutional only.
        /// </summary>
        public string DeltaNeutralSettlingFirm
        {
            get { return deltaNeutralSettlingFirm; }
            set { deltaNeutralSettlingFirm = value; }
        }

        /// <summary>
        /// For IBExecution customers: Specifies the true beneficiary of the order. 
        /// This value is required for FUT/FOP orders for reporting to the exchange.
        /// </summary>
        public string DeltaNeutralClearingAccount
        {
            get { return deltaNeutralClearingAccount; }
            set { deltaNeutralClearingAccount = value; }
        }

        /// <summary>
        /// For IBExecution customers: Valid values are: 
        /// IB, Away, and PTA (post trade allocation).
        /// </summary>
        public string DeltaNeutralClearingIntent
        {
            get { return deltaNeutralClearingIntent; }
            set { deltaNeutralClearingIntent = value; }
        }

        /// <summary>
        /// HedgeType For hedge orders. Possible values are:
        /// D = Delta, B = Beta, F = FX, P = Pair
        /// </summary>
        public string HedgeType
        {
            get { return hedgeType; }
            set { hedgeType = value; }
        }

        /// <summary>
        /// Beta = x for Beta hedge orders, ratio = y for Pair hedge order.
        /// </summary>
        public string HedgeParam
        {
            get { return hedgeParam; }
            set { hedgeParam = value; }
        }

        /// <summary>
        /// Support for IBAlgo parameters.
        /// </summary>
        public Collection<TagValue> SmartComboRoutingParams
        {
            get { return smartComboRoutingParams; }
            set { smartComboRoutingParams = value; }
        }

        /// <summary>
        /// Specifies whether the order is an Open or a Close order and is used when the hedge involves a CFD and and the order is clearing away.
        /// </summary>
        public string DeltaNeutralOpenClose { get; set; }

        /// <summary>
        /// Used when the hedge involves a stock and indicates whether or not it is sold short.
        /// </summary>
        public bool DeltaNeutralShortSale { get; set; }

        /// <summary>
        /// Valid values are 1 or 2. (Institutional - Non-cleared only)
        /// </summary>
        public int DeltaNeutralShortSaleSlot { get; set; }

        /// <summary>
        /// Used only when shortSaleSlot = 2. (Institutional - Non-cleared only)
        /// </summary>
        public string DeltaNeutralDesignatedLocation { get; set; }

        /// <summary>
        /// Specify the trailing amount of a trailing stop order as a percentage. 
        /// Observe the following guidelines when using the trailingPercent field:
        /// This field is mutually exclusive with the existing trailing amount. 
        /// That is, the API client can send one or the other but not both.
        /// This field is read AFTER the stop price (barrier price) as follows: 
        ///     deltaNeutralAuxPrice, stopPrice, trailingPercent, scale order attributes
        /// The field will also be sent to the API in the openOrder message if the API client version is >= 56. 
        /// It is sent after the stopPrice field as follows:
        ///     stopPrice, trailingPct, basisPoint
        /// </summary>
        public double TrailingPercent { get; set; }

        /// <summary>
        /// Holds attributes for all legs in a combo order. (Smart Combo Routing)
        /// </summary>
        public Collection<OrderComboLeg> OrderComboLegs { get; set; }

        /// <summary>
        /// For extended Scale orders.
        /// </summary>
        public double ScalePriceAdjustValue { get; set; }

        /// <summary>
        /// For extended Scale orders.
        /// </summary>
        public int ScalePriceAdjustInterval { get; set; }

        /// <summary>
        /// For extended Scale orders.
        /// </summary>
        public double ScaleProfitOffset { get; set; }

        /// <summary>
        /// For extended Scale orders.
        /// </summary>
        public bool ScaleAutoReset { get; set; }

        /// <summary>
        /// For extended Scale orders.
        /// </summary>
        public int ScaleInitPosition { get; set; }

        /// <summary>
        /// For extended Scale orders.
        /// </summary>
        public int ScaleInitFillQty { get; set; }

        /// <summary>
        /// For extended Scale orders.
        /// </summary>
        public bool ScaleRandomPercent { get; set; }

        #endregion
    }
}
