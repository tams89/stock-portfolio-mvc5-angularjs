// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBClient.cs" company="">
//   
// </copyright>
// <summary>
//   Interactive Brokers Client
//   Handles all communications to and from the TWS.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Krs.Ats.IBNet
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    /// <summary>
    ///     Interactive Brokers Client
    ///     Handles all communications to and from the TWS.
    /// </summary>
    public class IBClient : IDisposable
    {
        #region Constants

        /// <summary>
        ///     The client version.
        /// </summary>
        private const int clientVersion = 60;

        /// <summary>
        ///     The minimum server version.
        /// </summary>
        private const int minimumServerVersion = 38;

        #endregion

        #region Static Fields

        /// <summary>
        ///     The eol.
        /// </summary>
        private static readonly byte[] EOL = { 0 };

        #endregion

        #region Fields

        /// <summary>
        ///     The ib trace.
        /// </summary>
        private readonly GeneralTracer ibTrace = new GeneralTracer("ibInfo", "Interactive Brokers Parameter Info");

        /// <summary>
        ///     The read thread.
        /// </summary>
        private readonly Thread readThread;

        /// <summary>
        ///     Lock covering stopping and stopped
        /// </summary>
        private readonly object stopLock = new object();

        /// <summary>
        ///     The connected.
        /// </summary>
        private bool connected; // true if we are connected

        /// <summary>
        ///     The dis.
        /// </summary>
        private BinaryReader dis;

        /// <summary>
        ///     The dos.
        /// </summary>
        private BinaryWriter dos; // the ibSocket output stream

        /// <summary>
        ///     The ib socket.
        /// </summary>
        private TcpClient ibSocket; // the ibSocket

        /// <summary>
        ///     The ib tick trace.
        /// </summary>
        private GeneralTracer ibTickTrace = new GeneralTracer("ibTicks", "Interactive Brokers Tick Info");

        /// <summary>
        ///     The server version.
        /// </summary>
        private int serverVersion;

        /// <summary>
        ///     Whether or not the worker thread has stopped
        /// </summary>
        private bool stopped;

        /// <summary>
        ///     Whether or not the worker thread has been asked to stop
        /// </summary>
        private bool stopping;

        /// <summary>
        ///     The throw exceptions.
        /// </summary>
        private bool throwExceptions;

        /// <summary>
        ///     The tws time.
        /// </summary>
        private string twsTime;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="IBClient" /> class.
        ///     Default Constructor
        /// </summary>
        public IBClient()
        {
            readThread = new Thread(Run);
            readThread.IsBackground = true;
            readThread.Name = "IBClient Read Thread";
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     Called once all Account Details for a given request are received.
        /// </summary>
        public event EventHandler<AccountDownloadEndEventArgs> AccountDownloadEnd;

        /// <summary>
        ///     This event fires in response to the <see cref="RequestContractDetails" /> method called on a bond contract.
        /// </summary>
        public event EventHandler<BondContractDetailsEventArgs> BondContractDetails;

        /// <summary>
        ///     Called on a commission report call back.
        /// </summary>
        public event EventHandler<CommissionReportEventArgs> CommissionReport;

        /// <summary>
        ///     This method is called when TWS closes the sockets connection, or when TWS is shut down.
        /// </summary>
        public event EventHandler<ConnectionClosedEventArgs> ConnectionClosed;

        /// <summary>
        ///     This event fires in response to the <see cref="RequestContractDetails" /> method.
        /// </summary>
        public event EventHandler<ContractDetailsEventArgs> ContractDetails;

        /// <summary>
        ///     Called once all contract details for a given request are received.
        ///     This, for example, helps to define the end of an option chain.
        /// </summary>
        public event EventHandler<ContractDetailsEndEventArgs> ContractDetailsEnd;

        /// <summary>
        ///     This method receives the current system time on the server side.
        /// </summary>
        public event EventHandler<CurrentTimeEventArgs> CurrentTime;

        /// <summary>
        ///     Called once all execution data for a given request are received.
        /// </summary>
        public event EventHandler<DeltaNuetralValidationEventArgs> DeltaNuetralValidation;

        /// <summary>
        ///     This event is fired when there is an error with the communication or when TWS wants to send a message to the
        ///     client.
        /// </summary>
        public event EventHandler<ErrorEventArgs> Error;

        /// <summary>
        ///     This event fires in response to the <see cref="RequestExecutions" /> method or after an order is placed.
        /// </summary>
        public event EventHandler<ExecDetailsEventArgs> ExecDetails;

        /// <summary>
        ///     Called once all contract details for a given request are received.
        ///     This, for example, helps to define the end of an option chain.
        /// </summary>
        public event EventHandler<ExecutionDataEndEventArgs> ExecutionDataEnd;

        /// <summary>
        ///     Reuters global fundamental market data
        /// </summary>
        public event EventHandler<FundamentalDetailsEventArgs> FundamentalData;

        /// <summary>
        ///     This method receives the requested historical data results
        /// </summary>
        public event EventHandler<HistoricalDataEventArgs> HistoricalData;

        /// <summary>
        ///     This method is called when a successful connection is made to a Financial Advisor account.
        ///     It is also called when the reqManagedAccts() method is invoked.
        /// </summary>
        public event EventHandler<ManagedAccountsEventArgs> ManagedAccounts;

        /// <summary>
        ///     Called on a market data type call back.
        /// </summary>
        public event EventHandler<MarketDataTypeEventArgs> MarketDataType;

        /// <summary>
        ///     This method is called after a successful connection to TWS.
        /// </summary>
        public event EventHandler<NextValidIdEventArgs> NextValidId;

        /// <summary>
        ///     This method is called to feed in open orders.
        /// </summary>
        public event EventHandler<OpenOrderEventArgs> OpenOrder;

        /// <summary>
        ///     Called once all the open orders for a given request are received.
        /// </summary>
        public event EventHandler<EventArgs> OpenOrderEnd;

        /// <summary>
        ///     This methodis called whenever the status of an order changes. It is also fired after reconnecting
        ///     to TWS if the client has any open orders.
        /// </summary>
        public event EventHandler<OrderStatusEventArgs> OrderStatus;

        /// <summary>
        ///     This method receives the realtime bars data results.
        /// </summary>
        public event EventHandler<RealTimeBarEventArgs> RealTimeBar;

        /// <summary>
        ///     This method receives previously requested FA configuration information from TWS.
        /// </summary>
        public event EventHandler<ReceiveFAEventArgs> ReceiveFA;

        /// <summary>
        ///     This method is triggered for any exceptions caught.
        /// </summary>
        public event EventHandler<ReportExceptionEventArgs> ReportException;

        /// <summary>
        ///     This method receives the requested market scanner data results
        /// </summary>
        public event EventHandler<ScannerDataEventArgs> ScannerData;

        /// <summary>
        ///     This method receives the requested market scanner data results
        /// </summary>
        public event EventHandler<ScannerDataEndEventArgs> ScannerDataEnd;

        /// <summary>
        ///     This method receives an XML document that describes the valid parameters that a scanner subscription can have
        /// </summary>
        public event EventHandler<ScannerParametersEventArgs> ScannerParameters;

        /// <summary>
        ///     This method is called when the market data changes. Values are updated immediately with no delay.
        /// </summary>
        public event EventHandler<TickEfpEventArgs> TickEfp;

        /// <summary>
        ///     This method is called when the market data changes. Values are updated immediately with no delay.
        /// </summary>
        public event EventHandler<TickGenericEventArgs> TickGeneric;

        /// <summary>
        ///     This method is called when the market in an option or its underlier moves.
        ///     TWS’s option model volatilities, prices, and deltas, along with the present
        ///     value of dividends expected on that option’s underlier are received.
        /// </summary>
        public event EventHandler<TickOptionComputationEventArgs> TickOptionComputation;

        /// <summary>
        ///     This event is called when the market data changes. Prices are updated immediately with no delay.
        /// </summary>
        public event EventHandler<TickPriceEventArgs> TickPrice;

        /// <summary>
        ///     This event is called when the market data changes. Sizes are updated immediately with no delay.
        /// </summary>
        public event EventHandler<TickSizeEventArgs> TickSize;

        /// <summary>
        ///     Called once the tick snap shot is complete.
        /// </summary>
        public event EventHandler<TickSnapshotEndEventArgs> TickSnapshotEnd;

        /// <summary>
        ///     This method is called when the market data changes. Values are updated immediately with no delay.
        /// </summary>
        public event EventHandler<TickStringEventArgs> TickString;

        /// <summary>
        ///     This method is called only when reqAccountUpdates() method on the EClientSocket object has been called.
        /// </summary>
        public event EventHandler<UpdateAccountTimeEventArgs> UpdateAccountTime;

        /// <summary>
        ///     This method is called only when reqAccountUpdates() method on the EClientSocket object has been called.
        /// </summary>
        public event EventHandler<UpdateAccountValueEventArgs> UpdateAccountValue;

        /// <summary>
        ///     This method is called when the market depth changes.
        /// </summary>
        public event EventHandler<UpdateMarketDepthEventArgs> UpdateMarketDepth;

        /// <summary>
        ///     This method is called when the Level II market depth changes.
        /// </summary>
        public event EventHandler<UpdateMarketDepthL2EventArgs> UpdateMarketDepthL2;

        /// <summary>
        ///     This method is triggered for each new bulletin if the client has subscribed (i.e. by calling the reqNewsBulletins()
        ///     method.
        /// </summary>
        public event EventHandler<UpdateNewsBulletinEventArgs> UpdateNewsBulletin;

        /// <summary>
        ///     This method is called only when reqAccountUpdates() method on the EClientSocket object has been called.
        /// </summary>
        public event EventHandler<UpdatePortfolioEventArgs> UpdatePortfolio;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Returns the client version of the TWS API
        /// </summary>
        public static int ClientVersion
        {
            get
            {
                return clientVersion;
            }
        }

        /// <summary>
        ///     Returns the status of the connection to TWS.
        /// </summary>
        public bool Connected
        {
            get
            {
                return connected;
            }
        }

        /// <summary>
        ///     Thread that is reading and parsing the network stream
        /// </summary>
        public Thread ReadThread
        {
            get
            {
                return readThread;
            }
        }

        /// <summary>
        ///     Returns the version of the TWS instance the API application is connected to
        /// </summary>
        public int ServerVersion
        {
            get
            {
                return serverVersion;
            }
        }

        /// <summary>
        ///     Returns whether the worker thread has stopped.
        /// </summary>
        public bool Stopped
        {
            get
            {
                lock (stopLock)
                {
                    return stopped;
                }
            }
        }

        /// <summary>
        ///     Returns whether the worker thread has been asked to stop.
        ///     This continues to return true even after the thread has stopped.
        /// </summary>
        public bool Stopping
        {
            get
            {
                lock (stopLock)
                {
                    return stopping;
                }
            }
        }

        /// <summary>
        ///     Used to control the exception handling.
        ///     If true, all exceptions are thrown, else only throw non network exceptions.
        /// </summary>
        public bool ThrowExceptions
        {
            get
            {
                return throwExceptions;
            }

            set
            {
                throwExceptions = value;
            }
        }

        /// <summary>
        ///     Returns the time the API application made a connection to TWS
        /// </summary>
        public string TwsConnectionTime
        {
            get
            {
                return twsTime;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Call this function to cancel a request to calculate volatility for a supplied option price and underlying price.
        /// </summary>
        /// <param name="reqId">
        /// The Ticker Id.
        /// </param>
        public virtual void CancelCalculateImpliedVolatility(int reqId)
        {
            if (!connected)
            {
                error(ErrorMessage.NotConnected);
                return;
            }

            if (serverVersion < MinServerVersion.CancelCalculateImpliedVolatility)
            {
                error(reqId, ErrorMessage.UpdateTws, "It does not support calculate implied volatility cancellation.");
                return;
            }

            const int version = 1;

            try
            {
                // send cancel calculate implied volatility msg
                send((int)OutgoingMessage.CancelCalcImpliedVolatility);
                send(version);
                send(reqId);
            }
            catch (Exception e)
            {
                error(reqId, ErrorMessage.FailSendCancelCalculateImpliedVolatility, e);
                close();
            }
        }

        /// <summary>
        /// Call this function to cancel a request to calculate option price and greek values for a supplied volatility and
        ///     underlying price.
        /// </summary>
        /// <param name="reqId">
        /// The ticker id.
        /// </param>
        public virtual void CancelCalculateOptionPrice(int reqId)
        {
            if (!connected)
            {
                error(ErrorMessage.NotConnected);
                return;
            }

            if (serverVersion < MinServerVersion.CancelCalculateOptionPrice)
            {
                error(reqId, ErrorMessage.UpdateTws, "It does not support calculate option price cancellation.");
                return;
            }

            const int version = 1;

            try
            {
                // send cancel calculate option price msg
                send((int)OutgoingMessage.CancelCalcOptionPrice);
                send(version);
                send(reqId);
            }
            catch (Exception e)
            {
                error(reqId, ErrorMessage.FailSendCancelCalculateOptionPrice, e);
                close();
            }
        }

        /// <summary>
        /// Call this method to stop receiving Reuters global fundamental data.
        /// </summary>
        /// <param name="requestId">
        /// The ID of the data request.
        /// </param>
        public virtual void CancelFundamentalData(int requestId)
        {
            lock (this)
            {
                if (!connected)
                {
                    error(requestId, ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < MinServerVersion.FundamentalData)
                {
                    error(requestId, ErrorMessage.UpdateTws, "It does not support fundamental data requests.");
                    return;
                }

                var version = 1;

                try
                {
                    // send req mkt data msg
                    send((int)OutgoingMessage.CancelFundamentalData);
                    send(version);
                    send(requestId);
                }
                catch (Exception e)
                {
                    error(requestId, ErrorMessage.FailSendCancelFundData, string.Empty + e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the CancelHistoricalData method to stop receiving historical data results.
        /// </summary>
        /// <param name="tickerId">
        /// the Id that was specified in the call to
        ///     <see cref="RequestHistoricalData(int,Contract,DateTime,TimeSpan,BarSize,HistoricalDataType,int)"/>.
        /// </param>
        public void CancelHistoricalData(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < 24)
                {
                    error(ErrorMessage.UpdateTws, "It does not support historical data query cancellation.");
                    return;
                }

                var version = 1;

                // send cancel mkt data msg
                try
                {
                    send((int)OutgoingMessage.CancelHistoricalData);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendCancelHistoricalData, e);
                    close();
                }
            }
        }

        /// <summary>
        /// After calling this method, market data for the specified Id will stop flowing.
        /// </summary>
        /// <param name="tickerId">
        /// the Id that was specified in the call to reqMktData().
        /// </param>
        public void CancelMarketData(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                var version = 1;

                // send cancel mkt data msg
                try
                {
                    send((int)OutgoingMessage.CancelMarketData);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendCancelMarket, e);
                    close();
                }
            }
        }

        /// <summary>
        /// After calling this method, market depth data for the specified Id will stop flowing.
        /// </summary>
        /// <param name="tickerId">
        /// the Id that was specified in the call to reqMktDepth().
        /// </param>
        public void CancelMarketDepth(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >=6
                if (serverVersion < 6)
                {
                    error(ErrorMessage.UpdateTws, "It does not support canceling market depth.");
                    return;
                }

                var version = 1;

                // send cancel mkt data msg
                try
                {
                    send((int)OutgoingMessage.CancelMarketDepth);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendCancelMarketDepth, e);
                    close();
                }
            }
        }

        /// <summary>
        ///     Call this method to stop receiving news bulletins.
        /// </summary>
        public void CancelNewsBulletins()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                var version = 1;

                // send cancel order msg
                try
                {
                    send((int)OutgoingMessage.CancelNewsBulletins);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendCancelOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to cancel an order.
        /// </summary>
        /// <param name="orderId">
        /// Call this method to cancel an order.
        /// </param>
        public void CancelOrder(int orderId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(orderId, ErrorMessage.NotConnected);
                    return;
                }

                var version = 1;

                // send cancel order msg
                try
                {
                    send((int)OutgoingMessage.CancelOrder);
                    send(version);
                    send(orderId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(orderId, ErrorMessage.FailSendCancelOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the CancelRealTimeBars() method to stop receiving real time bar results.
        /// </summary>
        /// <param name="tickerId">
        /// The Id that was specified in the call to <see cref="RequestRealTimeBars"/>.
        /// </param>
        public void CancelRealTimeBars(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // 34 is the minimum server version for real time bars
                if (serverVersion < MinServerVersion.RealTimeBars)
                {
                    error(ErrorMessage.UpdateTws, "It does not support realtime bar data query cancellation.");
                    return;
                }

                var version = 1;

                // send cancel mkt data msg
                try
                {
                    send((int)OutgoingMessage.CancelRealTimeBars);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) | throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendCancelRealTimeBars, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the cancelScannerSubscription() method to stop receiving market scanner results.
        /// </summary>
        /// <param name="tickerId">
        /// the Id that was specified in the call to reqScannerSubscription().
        /// </param>
        public void CancelScannerSubscription(int tickerId)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < 24)
                {
                    error(ErrorMessage.UpdateTws, "It does not support API scanner subscription.");
                    return;
                }

                var version = 1;

                // send cancel mkt data msg
                try
                {
                    send((int)OutgoingMessage.CancelScannerSubscription);
                    send(version);
                    send(tickerId);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendCancelScanner, e);
                    close();
                }
            }
        }

        /// <summary>
        /// This function must be called before any other. There is no feedback for a successful connection, but a subsequent
        ///     attempt to connect will return the message "Already connected."
        /// </summary>
        /// <param name="host">
        /// host name or IP address of the machine where TWS is running. Leave blank to connect to the local host.
        /// </param>
        /// <param name="port">
        /// must match the port specified in TWS on the Configure&gt;API&gt;Socket Port field.
        /// </param>
        /// <param name="clientId">
        /// A number used to identify this client connection. All orders placed/modified from this client will be associated
        ///     with this client identifier.
        ///     Each client MUST connect with a unique clientId.
        /// </param>
        public void Connect(string host, int port, int clientId)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }

            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                throw new ArgumentOutOfRangeException("port");
            }

            lock (this)
            {
                // already connected?
                host = checkConnected(host);
                if (host == null)
                {
                    return;
                }

                var socket = new TcpClient(host, port);
                connect(socket, clientId);
            }
        }

        /// <summary>
        ///     Call this method to terminate the connections with TWS. Calling this method does not cancel orders that have
        ///     already been sent.
        /// </summary>
        public void Disconnect()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    return;
                }

                try
                {
                    // stop Reader thread
                    Stop();
                    readThread.Abort();

                    // close ibSocket
                    if (ibSocket != null)
                    {
                        ibSocket.Close();
                    }
                }
                catch
                {
                }

                connected = false;
            }
        }

        /// <summary>
        ///     Dispose() calls Dispose(true)
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Call the exerciseOptions() method to exercise options.
        ///     “SMART” is not an allowed exchange in exerciseOptions() calls, and that TWS does a moneyness request for the
        ///     position in question whenever any API initiated exercise or lapse is attempted.
        /// </summary>
        /// <param name="tickerId">
        /// the Id for the exercise request.
        /// </param>
        /// <param name="contract">
        /// this structure contains a description of the contract to be exercised.  If no multiplier is specified, a default of
        ///     100 is assumed.
        /// </param>
        /// <param name="exerciseAction">
        /// this can have two values:
        ///     1 = specifies exercise
        ///     2 = specifies lapse
        /// </param>
        /// <param name="exerciseQuantity">
        /// the number of contracts to be exercised
        /// </param>
        /// <param name="account">
        /// specifies whether your setting will override the system's natural action. For example, if your action is "exercise"
        ///     and the option is not in-the-money, by natural action the option would not exercise. If you have override set to
        ///     "yes" the natural action would be overridden and the out-of-the money option would be exercised. Values are:
        ///     0 = no
        ///     1 = yes
        /// </param>
        /// <param name="overrideRenamed">
        /// specifies whether your setting will override the system's natural action. For example, if your action is "exercise"
        ///     and the option is not in-the-money, by natural action the option would not exercise. If you have override set to
        ///     "yes" the natural action would be overridden and the out-of-the money option would be exercised. Values are:
        ///     0 = no
        ///     1 = yes
        /// </param>
        public void ExerciseOptions(
            int tickerId, 
            Contract contract, 
            int exerciseAction, 
            int exerciseQuantity, 
            string account, 
            int overrideRenamed)
        {
            if (contract == null)
            {
                throw new ArgumentNullException("contract");
            }

            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(tickerId, ErrorMessage.NotConnected);
                    return;
                }

                var version = 1;

                try
                {
                    if (serverVersion < 21)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support options exercise from the API.");
                        return;
                    }

                    send((int)OutgoingMessage.ExerciseOptions);
                    send(version);
                    send(tickerId);

                    // Send Contract Fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(
                        (contract.Right == RightType.Undefined)
                            ? string.Empty
                            : EnumDescConverter.GetEnumDescription(contract.Right));
                    send(contract.Multiplier);
                    send(contract.Exchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    send(exerciseAction);
                    send(exerciseQuantity);
                    send(account);
                    send(overrideRenamed);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendRequestMarket, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to place an order. The order status will be returned by the orderStatus event.
        /// </summary>
        /// <param name="orderId">
        /// the order Id. You must specify a unique value. When the order status returns, it will be identified by this tag.
        ///     This tag is also used when canceling the order.
        /// </param>
        /// <param name="contract">
        /// this structure contains a description of the contract which is being traded.
        /// </param>
        /// <param name="order">
        /// this structure contains the details of the order.
        ///     Each client MUST connect with a unique clientId.
        /// </param>
        public void PlaceOrder(int orderId, Contract contract, Order order)
        {
            if (contract == null)
            {
                throw new ArgumentNullException("contract");
            }

            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(orderId, ErrorMessage.NotConnected);
                    return;
                }

                // Scale Orders Minimum Version is 35
                if (serverVersion < MinServerVersion.ScaleOrders)
                {
                    if (order.ScaleInitLevelSize != int.MaxValue || order.ScalePriceIncrement != int.MaxValue
                        || order.ScalePriceIncrement != decimal.MaxValue)
                    {
                        error(orderId, ErrorMessage.UpdateTws, "It does not support Scale orders.");
                        return;
                    }
                }

                // Minimum Sell Short Combo Leg Order is 35
                if (serverVersion < MinServerVersion.SshortComboLegs)
                {
                    if (!(contract.ComboLegs.Count == 0))
                    {
                        ComboLeg comboLeg;
                        for (var i = 0; i < contract.ComboLegs.Count; ++i)
                        {
                            comboLeg = contract.ComboLegs[i];
                            if (comboLeg.ShortSaleSlot != 0 || (!string.IsNullOrEmpty(comboLeg.DesignatedLocation)))
                            {
                                error(
                                    orderId, 
                                    ErrorMessage.UpdateTws, 
                                    "It does not support SSHORT flag for combo legs.");
                                return;
                            }
                        }
                    }
                }

                if (serverVersion < MinServerVersion.WhatIfOrders)
                {
                    if (order.WhatIf)
                    {
                        error(orderId, ErrorMessage.UpdateTws, "It does not support what if orders.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.FundamentalData)
                {
                    if (contract.UnderlyingComponent != null)
                    {
                        error(orderId, ErrorMessage.UpdateTws, "It does not support delta-neutral orders.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.ScaleOrders2)
                {
                    if (order.ScaleSubsLevelSize != int.MaxValue)
                    {
                        error(
                            orderId, 
                            ErrorMessage.UpdateTws, 
                            "It does not support Subsequent Level Size for Scale orders.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.AlgoOrders)
                {
                    if (!string.IsNullOrEmpty(order.AlgoStrategy))
                    {
                        error(orderId, ErrorMessage.UpdateTws, "It does not support algo orders.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.NotHeld)
                {
                    if (order.NotHeld)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support notHeld parameter.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.SecIdType)
                {
                    if (contract.SecIdType != SecurityIdType.None || !string.IsNullOrEmpty(contract.SecId))
                    {
                        error(ErrorMessage.UpdateTws, "It does not support secIdType and secId parameters.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.PlaceOrderConId)
                {
                    if (contract.ContractId > 0)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support conId parameter.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.Sshortx)
                {
                    if (order.ExemptCode != -1)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support exemptCode parameter.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.Sshortx)
                {
                    if (contract.ComboLegs.Count > 0)
                    {
                        foreach (var comboLeg in contract.ComboLegs)
                        {
                            if (comboLeg.ExemptCode != -1)
                            {
                                error(ErrorMessage.UpdateTws, "It does not support exemptCode parameter.");
                                return;
                            }
                        }
                    }
                }

                if (serverVersion < MinServerVersion.HedgeOrders)
                {
                    if (!string.IsNullOrEmpty(order.HedgeType))
                    {
                        error(ErrorMessage.UpdateTws, "It does not support hedge orders.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.OptOutSmartRouting)
                {
                    if (order.OptOutSmartRouting)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support optOutSmartRouting parameter.");
                        return;
                    }
                }

                if (serverVersion < MinServerVersion.DeltaNeutralConId)
                {
                    if (order.DeltaNeutralConId > 0 || !string.IsNullOrEmpty(order.DeltaNeutralSettlingFirm)
                        || !string.IsNullOrEmpty(order.DeltaNeutralClearingAccount)
                        || !string.IsNullOrEmpty(order.DeltaNeutralClearingIntent))
                    {
                        error(
                            ErrorMessage.UpdateTws, 
                            "It does not support deltaNeutral parameters: ConId, SettlingFirm, ClearingAccount, ClearingIntent");
                        return;
                    }
                }

                var version = (serverVersion < MinServerVersion.NotHeld) ? 27 : 35;

                // send place order msg
                try
                {
                    send((int)OutgoingMessage.PlaceOrder);
                    send(version);
                    send(orderId);

                    // send contract fields
                    if (serverVersion >= 46)
                    {
                        send(contract.ContractId);
                    }

                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(
                        (contract.Right == RightType.Undefined)
                            ? string.Empty
                            : EnumDescConverter.GetEnumDescription(contract.Right));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }

                    send(contract.Exchange);
                    if (serverVersion >= 14)
                    {
                        send(contract.PrimaryExchange);
                    }

                    send(contract.Currency);
                    if (serverVersion >= 2)
                    {
                        send(contract.LocalSymbol);
                    }

                    if (serverVersion >= 45)
                    {
                        send(EnumDescConverter.GetEnumDescription(contract.SecIdType));
                        send(contract.SecId);
                    }

                    // send main order fields
                    send(EnumDescConverter.GetEnumDescription(order.Action));
                    send(order.TotalQuantity);
                    send(EnumDescConverter.GetEnumDescription(order.OrderType));
                    send(order.LimitPrice);
                    send(order.AuxPrice);

                    // send extended order fields
                    send(EnumDescConverter.GetEnumDescription(order.Tif));
                    send(order.OcaGroup);
                    send(order.Account);
                    send(order.OpenClose);
                    send((int)order.Origin);
                    send(order.OrderRef);
                    send(order.Transmit);
                    if (serverVersion >= 4)
                    {
                        send(order.ParentId);
                    }

                    if (serverVersion >= 5)
                    {
                        send(order.BlockOrder);
                        send(order.SweepToFill);
                        send(order.DisplaySize);
                        send((int)order.TriggerMethod);
                        if (serverVersion < 38)
                        {
                            // will never happen
                            send(false);
                        }
                        else
                        {
                            send(order.OutsideRth);
                        }
                    }

                    if (serverVersion >= 7)
                    {
                        send(order.Hidden);
                    }

                    // Send combo legs for BAG requests
                    if (serverVersion >= 8 && contract.SecurityType == SecurityType.Bag)
                    {
                        if (contract.ComboLegs == null)
                        {
                            send(0);
                        }
                        else
                        {
                            send(contract.ComboLegs.Count);

                            ComboLeg comboLeg;
                            for (var i = 0; i < contract.ComboLegs.Count; i++)
                            {
                                comboLeg = contract.ComboLegs[i];
                                send(comboLeg.ConId);
                                send(comboLeg.Ratio);
                                send(EnumDescConverter.GetEnumDescription(comboLeg.Action));
                                send(comboLeg.Exchange);
                                send((int)comboLeg.OpenClose);

                                // Min Combo Leg Short Sale Server Version is 35
                                if (serverVersion >= 35)
                                {
                                    send((int)comboLeg.ShortSaleSlot);
                                    send(comboLeg.DesignatedLocation);
                                }

                                if (serverVersion >= 51)
                                {
                                    send(comboLeg.ExemptCode);
                                }
                            }
                        }
                    }

                    if (serverVersion >= MinServerVersion.SmartComboRoutingParams
                        && contract.SecurityType == SecurityType.Bag)
                    {
                        var smartComboRoutingParams = order.SmartComboRoutingParams;
                        var smartComboRoutingParamsCount = smartComboRoutingParams == null
                                                               ? 0
                                                               : smartComboRoutingParams.Count;
                        send(smartComboRoutingParamsCount);
                        if (smartComboRoutingParamsCount > 0)
                        {
                            for (var i = 0; i < smartComboRoutingParamsCount; ++i)
                            {
                                var tagValue = smartComboRoutingParams[i];
                                send(tagValue.Tag);
                                send(tagValue.Value);
                            }
                        }
                    }

                    if (serverVersion >= 9)
                    {
                        send(string.Empty);
                    }

                    if (serverVersion >= 10)
                    {
                        send(order.DiscretionaryAmt);
                    }

                    if (serverVersion >= 11)
                    {
                        send(order.GoodAfterTime);
                    }

                    if (serverVersion >= 12)
                    {
                        send(order.GoodTillDate);
                    }

                    if (serverVersion >= 13)
                    {
                        send(order.FAGroup);
                        send(EnumDescConverter.GetEnumDescription(order.FAMethod));
                        send(order.FAPercentage);
                        send(order.FAProfile);
                    }

                    if (serverVersion >= 18)
                    {
                        // institutional short sale slot fields.
                        send((int)order.ShortSaleSlot); // 0 only for retail, 1 or 2 only for institution.
                        send(order.DesignatedLocation); // only populate when order.shortSaleSlot = 2.
                    }

                    if (serverVersion >= 51)
                    {
                        send(order.ExemptCode);
                    }

                    if (serverVersion >= 19)
                    {
                        send((int)order.OcaType);
                        if (serverVersion < 38)
                        {
                            // will never happen
                            send(false);
                        }

                        send(EnumDescConverter.GetEnumDescription(order.Rule80A));
                        send(order.SettlingFirm);
                        send(order.AllOrNone);
                        sendMax(order.MinQty);
                        sendMax(order.PercentOffset);
                        send(order.ETradeOnly);
                        send(order.FirmQuoteOnly);
                        sendMax(order.NbboPriceCap);
                        sendMax((int)order.AuctionStrategy);
                        sendMax(order.StartingPrice);
                        sendMax(order.StockRefPrice);
                        sendMax(order.Delta);

                        // Volatility orders had specific watermark price attribs in server version 26
                        var lower = (serverVersion == 26 && order.OrderType.Equals(OrderType.Volatility))
                                        ? double.MaxValue
                                        : order.StockRangeLower;
                        var upper = (serverVersion == 26 && order.OrderType.Equals(OrderType.Volatility))
                                        ? double.MaxValue
                                        : order.StockRangeUpper;
                        sendMax(lower);
                        sendMax(upper);
                    }

                    if (serverVersion >= 22)
                    {
                        send(order.OverridePercentageConstraints);
                    }

                    if (serverVersion >= 26)
                    {
                        // Volatility orders
                        sendMax(order.Volatility);
                        sendMax((int)order.VolatilityType);
                        if (serverVersion < 28)
                        {
                            send(order.DeltaNeutralOrderType.Equals(OrderType.Market));
                        }
                        else
                        {
                            send(EnumDescConverter.GetEnumDescription(order.DeltaNeutralOrderType));
                            sendMax(order.DeltaNeutralAuxPrice);

                            if (serverVersion >= MinServerVersion.DeltaNeutralConId
                                && order.DeltaNeutralOrderType != OrderType.Empty)
                            {
                                send(order.DeltaNeutralConId);
                                send(order.DeltaNeutralSettlingFirm);
                                send(order.DeltaNeutralClearingAccount);
                                send(order.DeltaNeutralClearingIntent);
                            }
                        }

                        send(order.ContinuousUpdate);
                        if (serverVersion == 26)
                        {
                            // Volatility orders had specific watermark price attribs in server version 26
                            var lower = order.OrderType.Equals(OrderType.Volatility)
                                            ? order.StockRangeLower
                                            : double.MaxValue;
                            var upper = order.OrderType.Equals(OrderType.Volatility)
                                            ? order.StockRangeUpper
                                            : double.MaxValue;
                            sendMax(lower);
                            sendMax(upper);
                        }

                        sendMax(order.ReferencePriceType);
                    }

                    if (serverVersion >= 30)
                    {
                        // TRAIL_STOP_LIMIT stop price
                        sendMax(order.TrailStopPrice);
                    }

                    // Scale Orders require server version 35 or higher.
                    if (serverVersion >= MinServerVersion.ScaleOrders)
                    {
                        if (serverVersion >= MinServerVersion.ScaleOrders2)
                        {
                            sendMax(order.ScaleInitLevelSize);
                            sendMax(order.ScaleSubsLevelSize);
                        }
                        else
                        {
                            send(string.Empty);
                            sendMax(order.ScaleInitLevelSize);
                        }

                        sendMax(order.ScalePriceIncrement);
                    }

                    if (serverVersion >= MinServerVersion.HedgeOrders)
                    {
                        send(order.HedgeType);
                        if (!string.IsNullOrEmpty(order.HedgeType))
                        {
                            send(order.HedgeParam);
                        }
                    }

                    if (serverVersion >= MinServerVersion.OptOutSmartRouting)
                    {
                        send(order.OptOutSmartRouting);
                    }

                    if (serverVersion >= MinServerVersion.PtaOrders)
                    {
                        send(order.ClearingAccount);
                        send(order.ClearingIntent);
                    }

                    if (serverVersion >= MinServerVersion.NotHeld)
                    {
                        send(order.NotHeld);
                    }

                    if (serverVersion >= MinServerVersion.UnderComp)
                    {
                        if (contract.UnderlyingComponent != null)
                        {
                            var underComp = contract.UnderlyingComponent;
                            send(true);
                            send(underComp.ContractId);
                            send(underComp.Delta);
                            send(underComp.Price);
                        }
                        else
                        {
                            send(false);
                        }
                    }

                    if (serverVersion >= MinServerVersion.AlgoOrders)
                    {
                        send(order.AlgoStrategy);
                        if (!string.IsNullOrEmpty(order.AlgoStrategy))
                        {
                            if (order.AlgoParams == null)
                            {
                                send(0);
                            }
                            else
                            {
                                send(order.AlgoParams.Count);
                                foreach (var tagValue in order.AlgoParams)
                                {
                                    send(tagValue.Tag);
                                    send(tagValue.Value);
                                }
                            }
                        }
                    }

                    if (serverVersion >= MinServerVersion.WhatIfOrders)
                    {
                        send(order.WhatIf);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(orderId, ErrorMessage.FailSendOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request FA configuration information from TWS. The data returns in an XML string via a
        ///     "receiveFA" ActiveX event.
        /// </summary>
        /// <param name="faDataType">
        /// specifies the type of Financial Advisor configuration data being requested. Valid values include:
        ///     1 = GROUPS
        ///     2 = PROFILE
        ///     3 = ACCOUNT ALIASES
        /// </param>
        /// <param name="xml">
        /// the XML string containing the new FA configuration information.
        /// </param>
        public void ReplaceFA(FADataType faDataType, string xml)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >= 13
                if (serverVersion < 13)
                {
                    error(ErrorMessage.UpdateTws, "Does not support Replace FA.");
                    return;
                }

                var version = 1;

                try
                {
                    send((int)OutgoingMessage.ReplaceFA);
                    send(version);
                    send((int)faDataType);
                    send(xml);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendFAReplace, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this function to start getting account values, portfolio, and last update time information.
        /// </summary>
        /// <param name="subscribe">
        /// If set to TRUE, the client will start receiving account and portfolio updates. If set to FALSE, the client will
        ///     stop receiving this information.
        /// </param>
        /// <param name="acctCode">
        /// the account code for which to receive account and portfolio updates.
        /// </param>
        public void RequestAccountUpdates(bool subscribe, string acctCode)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                var version = 2;

                // send cancel order msg
                try
                {
                    send((int)OutgoingMessage.RequestAccountData);
                    send(version);
                    send(subscribe);

                    // Send the account code. This will only be used for FA clients
                    if (serverVersion >= 9)
                    {
                        send(acctCode);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendAccountUpdate, e);
                    close();
                }
            }
        }

        /// <summary>
        ///     Call this method to request the open orders that were placed from all clients and also from TWS. Each open order
        ///     will be fed back through the openOrder() and orderStatus() functions on the EWrapper.
        ///     No association is made between the returned orders and the requesting client.
        /// </summary>
        public void RequestAllOpenOrders()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                var version = 1;

                // send req all open orders msg
                try
                {
                    send((int)OutgoingMessage.RequestAllOpenOrders);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendOpenOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request that newly created TWS orders be implicitly associated with the client. When a new TWS
        ///     order is created, the order will be associated with the client and fed back through the openOrder() and
        ///     orderStatus() methods on the EWrapper.
        ///     TWS orders can only be bound to clients with a clientId of “0”.
        /// </summary>
        /// <param name="autoBind">
        /// If set to TRUE, newly created TWS orders will be implicitly associated with the client. If set to FALSE, no
        ///     association will be made.
        /// </param>
        public void RequestAutoOpenOrders(bool autoBind)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                var version = 1;

                // send req open orders msg
                try
                {
                    send((int)OutgoingMessage.RequestAutoOpenOrders);
                    send(version);
                    send(autoBind);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendOpenOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Calculates the Implied Volatility based on the user-supplied option and underlying prices.
        ///     The calculated implied volatility is returned by tickOptionComputation( ) in a new tick type,
        ///     CUST_OPTION_COMPUTATION, which is described below.
        /// </summary>
        /// <param name="requestId">
        /// Request Id
        /// </param>
        /// <param name="contract">
        /// Contract
        /// </param>
        /// <param name="optionPrice">
        /// Price of the option
        /// </param>
        /// <param name="underPrice">
        /// Price of teh underlying of the option
        /// </param>
        public virtual void RequestCalculateImpliedVolatility(
            int requestId, 
            Contract contract, 
            double optionPrice, 
            double underPrice)
        {
            lock (this)
            {
                if (!connected)
                {
                    error(requestId, ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < MinServerVersion.RequestCalculateImpliedVolatility)
                {
                    error(ErrorMessage.UpdateTws, "It does not support calculate implied volatility requests.");
                    return;
                }

                const int version = 1;

                try
                {
                    // send calculate implied volatility msg
                    send((int)OutgoingMessage.RequestCalcImpliedVolatility);
                    send(version);
                    send(requestId);

                    // send contract fields
                    send(contract.ContractId);
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(
                        (contract.Right == RightType.Undefined)
                            ? string.Empty
                            : EnumDescConverter.GetEnumDescription(contract.Right));
                    send(contract.Multiplier);
                    send(contract.Exchange);
                    send(contract.PrimaryExchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);

                    send(optionPrice);
                    send(underPrice);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(requestId, ErrorMessage.FailSendReqCalcImpliedVolatility, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this function to calculate option price and greek values for a supplied volatility and underlying price.
        /// </summary>
        /// <param name="reqId">
        /// The ticker ID.
        /// </param>
        /// <param name="contract">
        /// Describes the contract.
        /// </param>
        /// <param name="volatility">
        /// The volatility.
        /// </param>
        /// <param name="underPrice">
        /// Price of the underlying.
        /// </param>
        public virtual void RequestCalculateOptionPrice(
            int reqId, 
            Contract contract, 
            double volatility, 
            double underPrice)
        {
            if (!connected)
            {
                error(ErrorMessage.NotConnected);
                return;
            }

            if (serverVersion < MinServerVersion.RequestCalculateOptionPrice)
            {
                error(reqId, ErrorMessage.UpdateTws, "It does not support calculate option price requests.");
                return;
            }

            const int version = 1;

            try
            {
                // send calculate option price msg
                send((int)OutgoingMessage.RequestCalcOptionPrice);
                send(version);
                send(reqId);

                // send contract fields
                send(contract.ContractId);
                send(contract.Symbol);
                send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                send(contract.Expiry);
                send(contract.Strike);
                send(EnumDescConverter.GetEnumDescription(contract.Right));
                send(contract.Multiplier);
                send(contract.Exchange);
                send(contract.PrimaryExchange);
                send(contract.Currency);
                send(contract.LocalSymbol);

                send(volatility);
                send(underPrice);
            }
            catch (Exception e)
            {
                error(reqId, ErrorMessage.FailSendRequestCalcOptionPrice, e);
                close();
            }
        }

        /// <summary>
        /// Call this function to download all details for a particular underlying. the contract details will be received via
        ///     the contractDetails() function on the EWrapper.
        /// </summary>
        /// <param name="requestId">
        /// Request Id for Contract Details
        /// </param>
        /// <param name="contract">
        /// summary description of the contract being looked up.
        /// </param>
        public void RequestContractDetails(int requestId, Contract contract)
        {
            if (contract == null)
            {
                throw new ArgumentNullException("contract");
            }

            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >=4
                if (serverVersion < 4)
                {
                    error(ErrorMessage.UpdateTws, "Does not support Request Contract Details.");
                    return;
                }

                if (serverVersion < MinServerVersion.SecIdType)
                {
                    if (contract.SecIdType != SecurityIdType.None || !string.IsNullOrEmpty(contract.SecId))
                    {
                        error(ErrorMessage.UpdateTws, "It does not support secIdType and secId parameters.");
                        return;
                    }
                }

                const int version = 6;

                try
                {
                    // send req mkt data msg
                    send((int)OutgoingMessage.RequestContractData);
                    send(version);

                    // MIN_SERVER_VER_CONTRACT_DATA_CHAIN = 40
                    if (serverVersion >= 40)
                    {
                        send(requestId);
                    }

                    if (serverVersion >= 37)
                    {
                        send(contract.ContractId);
                    }

                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(EnumDescConverter.GetEnumDescription(contract.Right));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }

                    send(contract.Exchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    if (serverVersion >= 31)
                    {
                        send(contract.IncludeExpired);
                    }

                    if (serverVersion >= 45)
                    {
                        send(EnumDescConverter.GetEnumDescription(contract.SecIdType));
                        send(contract.SecId);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendRequestContract, e);
                    close();
                }
            }
        }

        /// <summary>
        ///     Returns the current system time on the server side.
        /// </summary>
        public void RequestCurrentTime()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >= 33
                if (serverVersion < 33)
                {
                    error(ErrorMessage.UpdateTws, "It does not support current time requests.");
                    return;
                }

                var version = 1;

                try
                {
                    send((int)OutgoingMessage.RequestCurrentTime);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendRequestCurrentTime, e);
                    close();
                }
            }
        }

        /// <summary>
        /// When this method is called, the execution reports that meet the filter criteria are downloaded to the client via
        ///     the execDetails() method.
        /// </summary>
        /// <param name="requestId">
        /// Id of the request
        /// </param>
        /// <param name="filter">
        /// the filter criteria used to determine which execution reports are returned.
        /// </param>
        public void RequestExecutions(int requestId, ExecutionFilter filter)
        {
            if (filter == null)
            {
                filter = new ExecutionFilter(
                    0, 
                    string.Empty, 
                    DateTime.MinValue, 
                    string.Empty, 
                    SecurityType.Undefined, 
                    string.Empty, 
                    ActionSide.Undefined);
            }

            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                var version = 3;

                // send cancel order msg
                try
                {
                    send((int)OutgoingMessage.RequestExecutions);
                    send(version);

                    if (serverVersion >= 42)
                    {
                        send(requestId);
                    }

                    // Send the execution rpt filter data
                    if (serverVersion >= 9)
                    {
                        send(filter.ClientId);
                        send(filter.AcctCode);

                        // The valid format for time is "yyyymmdd-hh:mm:ss"
                        send(filter.Time.ToUniversalTime().ToString("yyyyMMdd-HH:mm:ss", CultureInfo.InvariantCulture));
                        send(filter.Symbol);
                        send(EnumDescConverter.GetEnumDescription(filter.SecurityType));
                        send(filter.Exchange);
                        send(EnumDescConverter.GetEnumDescription(filter.Side));
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendExecution, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request FA configuration information from TWS. The data returns in an XML string via the
        ///     receiveFA() method.
        /// </summary>
        /// <param name="faDataType">
        /// faDataType - specifies the type of Financial Advisor configuration data being requested. Valid values include:
        ///     1 = GROUPS
        ///     2 = PROFILE
        ///     3 =ACCOUNT ALIASES
        /// </param>
        public void RequestFA(FADataType faDataType)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >= 13
                if (serverVersion < 13)
                {
                    error(ErrorMessage.UpdateTws, "Does not support request FA.");
                    return;
                }

                var version = 1;

                try
                {
                    send((int)OutgoingMessage.RequestFA);
                    send(version);
                    send((int)faDataType);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendFARequest, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Request Fundamental Data
        /// </summary>
        /// <param name="requestId">
        /// Request Id
        /// </param>
        /// <param name="contract">
        /// Contract to request fundamental data for
        /// </param>
        /// <param name="reportType">
        /// Report Type
        /// </param>
        public virtual void RequestFundamentalData(int requestId, Contract contract, string reportType)
        {
            lock (this)
            {
                if (!connected)
                {
                    error(requestId, ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < MinServerVersion.FundamentalData)
                {
                    error(requestId, ErrorMessage.UpdateTws, "It does not support fundamental data requests.");
                    return;
                }

                var version = 1;

                try
                {
                    // send req fund data msg
                    send((int)OutgoingMessage.RequestFundamentalData);
                    send(version);
                    send(requestId);

                    // send contract fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Exchange);
                    send(contract.PrimaryExchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);

                    send(reportType);
                }
                catch (Exception e)
                {
                    error(requestId, ErrorMessage.FailSendRequestFundData, string.Empty + e);
                    close();
                }
            }
        }

        /// <summary>
        ///     Request Global Cancel.
        /// </summary>
        public virtual void RequestGlobalCancel()
        {
            // not connected?
            if (!connected)
            {
                error(ErrorMessage.NotConnected);
                return;
            }

            if (serverVersion < MinServerVersion.RequestGlobalCancel)
            {
                error(ErrorMessage.UpdateTws, "It does not support globalCancel requests.");
                return;
            }

            const int version = 1;

            // send request global cancel msg
            try
            {
                send((int)OutgoingMessage.RequestGlobalCancel);
                send(version);
            }
            catch (Exception e)
            {
                error(ErrorMessage.FailSendRequestGlobalCancel, e);
                close();
            }
        }

        /// <summary>
        /// Call the reqHistoricalData() method to start receiving historical data results through the historicalData()
        ///     EWrapper method.
        /// </summary>
        /// <param name="tickerId">
        /// the Id for the request. Must be a unique value. When the data is received, it will be identified by this Id. This
        ///     is also used when canceling the historical data request.
        /// </param>
        /// <param name="contract">
        /// this structure contains a description of the contract for which market data is being requested.
        /// </param>
        /// <param name="endDateTime">
        /// Date is sent after a .ToUniversalTime, so make sure the kind property is set correctly, and assumes GMT timezone.
        ///     Use the format yyyymmdd hh:mm:ss tmz, where the time zone is allowed (optionally) after a space at the end.
        /// </param>
        /// <param name="duration">
        /// This is the time span the request will cover, and is specified using the format:
        ///     <integer/> <unit/>, i.e., 1 D, where valid units are:
        ///     S (seconds)
        ///     D (days)
        ///     W (weeks)
        ///     M (months)
        ///     Y (years)
        ///     If no unit is specified, seconds are used. "years" is currently limited to one.
        /// </param>
        /// <param name="barSizeSetting">
        /// specifies the size of the bars that will be returned (within IB/TWS limits). Valid values include:
        ///     <list type="table">
        /// <listheader>
        /// <term>Bar Size</term>
        /// <description>Parametric Value</description>
        /// </listheader>
        /// <item>
        /// <term>1 sec</term>
        /// <description>1</description>
        /// </item>
        /// <item>
        /// <term>5 secs</term>
        /// <description>2</description>
        /// </item>
        /// <item>
        /// <term>15 secs</term>
        /// <description>3</description>
        /// </item>
        /// <item>
        /// <term>30 secs</term>
        /// <description>4</description>
        /// </item>
        /// <item>
        /// <term>1 min</term>
        /// <description>5</description>
        /// </item>
        /// <item>
        /// <term>2 mins</term>
        /// <description>6</description>
        /// </item>
        /// <item>
        /// <term>5 mins</term>
        /// <description>7</description>
        /// </item>
        /// <item>
        /// <term>15 mins</term>
        /// <description>8</description>
        /// </item>
        /// <item>
        /// <term>30 mins</term>
        /// <description>9</description>
        /// </item>
        /// <item>
        /// <term>1 hour</term>
        /// <description>10</description>
        /// </item>
        /// <item>
        /// <term>1 day</term>
        /// <description>11</description>
        /// </item>
        /// <item>
        /// <term>1 week</term>
        /// <description></description>
        /// </item>
        /// <item>
        /// <term>1 month</term>
        /// <description></description>
        /// </item>
        /// <item>
        /// <term>3 months</term>
        /// <description></description>
        /// </item>
        /// <item>
        /// <term>1 year</term>
        /// <description></description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="whatToShow">
        /// determines the nature of data being extracted. Valid values include:
        ///     TRADES
        ///     MIDPOINT
        ///     BID
        ///     ASK
        ///     BID/ASK
        /// </param>
        /// <param name="useRth">
        /// determines whether to return all data available during the requested time span, or only data that falls within
        ///     regular trading hours. Valid values include:
        ///     0 - all data is returned even where the market in question was outside of its regular trading hours.
        ///     1 - only data within the regular trading hours is returned, even if the requested time span falls partially or
        ///     completely outside of the RTH.
        /// </param>
        public void RequestHistoricalData(
            int tickerId, 
            Contract contract, 
            DateTime endDateTime, 
            TimeSpan duration, 
            BarSize barSizeSetting, 
            HistoricalDataType whatToShow, 
            int useRth)
        {
            var beginDateTime = endDateTime.Subtract(duration);

            var dur = ConvertPeriodtoIb(beginDateTime, endDateTime);
            RequestHistoricalData(tickerId, contract, endDateTime, dur, barSizeSetting, whatToShow, useRth);
        }

        /// <summary>
        /// Call the reqHistoricalData() method to start receiving historical data results through the historicalData()
        ///     EWrapper method.
        /// </summary>
        /// <param name="tickerId">
        /// the Id for the request. Must be a unique value. When the data is received, it will be identified by this Id. This
        ///     is also used when canceling the historical data request.
        /// </param>
        /// <param name="contract">
        /// this structure contains a description of the contract for which market data is being requested.
        /// </param>
        /// <param name="endDateTime">
        /// Date is sent after a .ToUniversalTime, so make sure the kind property is set correctly, and assumes GMT timezone.
        ///     Use the format yyyymmdd hh:mm:ss tmz, where the time zone is allowed (optionally) after a space at the end.
        /// </param>
        /// <param name="duration">
        /// This is the time span the request will cover, and is specified using the format:
        ///     <integer/> <unit/>, i.e., 1 D, where valid units are:
        ///     S (seconds)
        ///     D (days)
        ///     W (weeks)
        ///     M (months)
        ///     Y (years)
        ///     If no unit is specified, seconds are used. "years" is currently limited to one.
        /// </param>
        /// <param name="barSizeSetting">
        /// specifies the size of the bars that will be returned (within IB/TWS limits). Valid values include:
        ///     <list type="table">
        /// <listheader>
        /// <term>Bar Size</term>
        /// <description>Parametric Value</description>
        /// </listheader>
        /// <item>
        /// <term>1 sec</term>
        /// <description>1</description>
        /// </item>
        /// <item>
        /// <term>5 secs</term>
        /// <description>2</description>
        /// </item>
        /// <item>
        /// <term>15 secs</term>
        /// <description>3</description>
        /// </item>
        /// <item>
        /// <term>30 secs</term>
        /// <description>4</description>
        /// </item>
        /// <item>
        /// <term>1 min</term>
        /// <description>5</description>
        /// </item>
        /// <item>
        /// <term>2 mins</term>
        /// <description>6</description>
        /// </item>
        /// <item>
        /// <term>5 mins</term>
        /// <description>7</description>
        /// </item>
        /// <item>
        /// <term>15 mins</term>
        /// <description>8</description>
        /// </item>
        /// <item>
        /// <term>30 mins</term>
        /// <description>9</description>
        /// </item>
        /// <item>
        /// <term>1 hour</term>
        /// <description>10</description>
        /// </item>
        /// <item>
        /// <term>1 day</term>
        /// <description>11</description>
        /// </item>
        /// <item>
        /// <term>1 week</term>
        /// <description></description>
        /// </item>
        /// <item>
        /// <term>1 month</term>
        /// <description></description>
        /// </item>
        /// <item>
        /// <term>3 months</term>
        /// <description></description>
        /// </item>
        /// <item>
        /// <term>1 year</term>
        /// <description></description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="whatToShow">
        /// determines the nature of data being extracted. Valid values include:
        ///     TRADES
        ///     MIDPOINT
        ///     BID
        ///     ASK
        ///     BID/ASK
        /// </param>
        /// <param name="useRth">
        /// determines whether to return all data available during the requested time span, or only data that falls within
        ///     regular trading hours. Valid values include:
        ///     0 - all data is returned even where the market in question was outside of its regular trading hours.
        ///     1 - only data within the regular trading hours is returned, even if the requested time span falls partially or
        ///     completely outside of the RTH.
        /// </param>
        public void RequestHistoricalData(
            int tickerId, 
            Contract contract, 
            DateTime endDateTime, 
            string duration, 
            BarSize barSizeSetting, 
            HistoricalDataType whatToShow, 
            int useRth)
        {
            if (contract == null)
            {
                throw new ArgumentNullException("contract");
            }

            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(tickerId, ErrorMessage.NotConnected);
                    return;
                }

                var version = 4;

                try
                {
                    if (serverVersion < 16)
                    {
                        error(ErrorMessage.UpdateTws, "It does not support historical data backfill.");
                        return;
                    }

                    send((int)OutgoingMessage.RequestHistoricalData);
                    send(version);
                    send(tickerId);

                    // Send Contract Fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(
                        (contract.Right == RightType.Undefined)
                            ? string.Empty
                            : EnumDescConverter.GetEnumDescription(contract.Right));
                    send(contract.Multiplier);
                    send(contract.Exchange);
                    send(contract.PrimaryExchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    if (serverVersion >= 31)
                    {
                        send(contract.IncludeExpired ? 1 : 0);
                    }

                    if (serverVersion >= 20)
                    {
                        // yyyymmdd hh:mm:ss tmz
                        send(
                            endDateTime.ToUniversalTime().ToString("yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture)
                            + " GMT");
                        send(EnumDescConverter.GetEnumDescription(barSizeSetting));
                    }

                    send(duration);
                    send(useRth);
                    send(EnumDescConverter.GetEnumDescription(whatToShow));
                    if (serverVersion > 16)
                    {
                        // Send date times as seconds since 1970
                        send(2);
                    }

                    if (contract.SecurityType == SecurityType.Bag)
                    {
                        if (contract.ComboLegs == null)
                        {
                            send(0);
                        }
                        else
                        {
                            send(contract.ComboLegs.Count);

                            ComboLeg comboLeg;
                            for (var i = 0; i < contract.ComboLegs.Count; i++)
                            {
                                comboLeg = contract.ComboLegs[i];
                                send(comboLeg.ConId);
                                send(comboLeg.Ratio);
                                send(EnumDescConverter.GetEnumDescription(comboLeg.Action));
                                send(comboLeg.Exchange);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendRequestHistoricalData, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Returns one next valid Id...
        /// </summary>
        /// <param name="numberOfIds">
        /// Has No Effect
        /// </param>
        public void RequestIds(int numberOfIds)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                var version = 1;

                try
                {
                    send((int)OutgoingMessage.RequestIds);
                    send(version);
                    send(numberOfIds);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendCancelOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        ///     Call this method to request the list of managed accounts. The list will be returned by the managedAccounts()
        ///     function on the EWrapper.
        ///     This request can only be made when connected to a Financial Advisor (FA) account.
        /// </summary>
        public void RequestManagedAccts()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                var version = 1;

                // send req FA managed accounts msg
                try
                {
                    send((int)OutgoingMessage.RequestManagedAccounts);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendOpenOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to request market data. The market data will be returned by the tickPrice, tickSize,
        ///     tickOptionComputation(), tickGeneric(), tickString() and tickEFP() methods.
        /// </summary>
        /// <param name="tickerId">
        /// the ticker id. Must be a unique value. When the market data returns, it will be identified by this tag. This is
        ///     also used when canceling the market data.
        /// </param>
        /// <param name="contract">
        /// this structure contains a description of the contract for which market data is being requested.
        /// </param>
        /// <param name="genericTickList">
        /// comma delimited list of generic tick types.  Tick types can be found here: (new Generic Tick Types page)
        /// </param>
        /// <param name="snapshot">
        /// Allows client to request snapshot market data.
        /// </param>
        /// <param name="marketDataOff">
        /// Market Data Off - used in conjunction with RTVolume Generic tick type causes only volume data to be sent.
        /// </param>
        public void RequestMarketData(
            int tickerId, 
            Contract contract, 
            Collection<GenericTickType> genericTickList, 
            bool snapshot, 
            bool marketDataOff)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // 35 is the minimum version for snapshots
                if (serverVersion < MinServerVersion.ScaleOrders && snapshot)
                {
                    error(tickerId, ErrorMessage.UpdateTws, "It does not support snapshot market data requests.");
                    return;
                }

                // 40 is the minimum version for the Underlying Component class
                if (serverVersion < MinServerVersion.UnderComp)
                {
                    if (contract.UnderlyingComponent != null)
                    {
                        error(tickerId, ErrorMessage.UpdateTws, "It does not support delta-neutral orders.");
                        return;
                    }
                }

                // 46 is the minimum version for requesting contracts by conid
                if (serverVersion < MinServerVersion.RequestMarketDataConId)
                {
                    if (contract.ContractId > 0)
                    {
                        error(tickerId, ErrorMessage.UpdateTws, "It does not support conId parameter.");
                        return;
                    }
                }

                var version = 9;

                try
                {
                    // send req mkt data msg
                    send((int)OutgoingMessage.RequestMarketData);
                    send(version);
                    send(tickerId);
                    if (serverVersion >= 47)
                    {
                        send(contract.ContractId);
                    }

                    // Send Contract Fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(
                        (contract.Right == RightType.Undefined)
                            ? string.Empty
                            : EnumDescConverter.GetEnumDescription(contract.Right));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }

                    send(contract.Exchange);
                    if (serverVersion >= 14)
                    {
                        send(contract.PrimaryExchange);
                    }

                    send(contract.Currency);
                    if (serverVersion >= 2)
                    {
                        send(contract.LocalSymbol);
                    }

                    if (serverVersion >= 8 && contract.SecurityType == SecurityType.Bag)
                    {
                        if (contract.ComboLegs == null)
                        {
                            send(0);
                        }
                        else
                        {
                            send(contract.ComboLegs.Count);

                            ComboLeg comboLeg;
                            for (var i = 0; i < contract.ComboLegs.Count; i++)
                            {
                                comboLeg = contract.ComboLegs[i];
                                send(comboLeg.ConId);
                                send(comboLeg.Ratio);
                                send(EnumDescConverter.GetEnumDescription(comboLeg.Action));
                                send(comboLeg.Exchange);
                            }
                        }
                    }

                    if (serverVersion >= 40)
                    {
                        if (contract.UnderlyingComponent != null)
                        {
                            var underComp = contract.UnderlyingComponent;
                            send(true);
                            send(underComp.ContractId);
                            send(underComp.Delta);
                            send(underComp.Price);
                        }
                        else
                        {
                            send(false);
                        }
                    }

                    if (serverVersion >= 31)
                    {
                        /*
                         * Even though SHORTABLE tick type supported only
                         * starting server version 33 it would be relatively
                         * expensive to expose this restriction here.
                         *
                         * Therefore we are relying on TWS doing validation.
                         */
                        var genList = new StringBuilder();
                        if (genericTickList != null)
                        {
                            for (var counter = 0; counter < genericTickList.Count; counter++)
                            {
                                genList.AppendFormat(
                                    "{0},", 
                                    ((int)genericTickList[counter]).ToString(CultureInfo.InvariantCulture));
                            }
                        }

                        if (marketDataOff)
                        {
                            genList.AppendFormat("mdoff");
                        }

                        send(genList.ToString().Trim(','));
                    }

                    // 35 is the minum version for SnapShot
                    if (serverVersion >= 35)
                    {
                        send(snapshot);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendRequestMarket, e);
                    close();
                }
            }
        }

        /// <summary>
        /// The API can receive frozen market data from Trader Workstation.
        ///     Frozen market data is the last data recorded in our system.
        ///     During normal trading hours, the API receives real-time market data.
        ///     If you use this function, you are telling TWS to automatically switch to frozen market data after the close.
        ///     Then, before the opening of the next trading day, market data will automatically switch back to real-time market
        ///     data.
        /// </summary>
        /// <param name="marketDataType">
        /// 1 for real-time streaming market data or 2 for frozen market data.
        /// </param>
        public virtual void RequestMarketDataType(int marketDataType)
        {
            // not connected?
            if (!connected)
            {
                error(ErrorMessage.NotConnected);
                return;
            }

            if (serverVersion < MinServerVersion.RequestMarketDataType)
            {
                error(ErrorMessage.UpdateTws, "It does not support marketDataType requests.");
                return;
            }

            const int version = 1;

            // send the reqMarketDataType message
            try
            {
                send((int)OutgoingMessage.RequestMarketDataType);
                send(version);
                send(marketDataType);
            }
            catch (Exception e)
            {
                error(ErrorMessage.FailSendRequestMarketDataType, e);
                close();
            }
        }

        /// <summary>
        /// Call this method to request market depth for a specific contract. The market depth will be returned by the
        ///     updateMktDepth() and updateMktDepthL2() methods.
        /// </summary>
        /// <param name="tickerId">
        /// the ticker Id. Must be a unique value. When the market depth data returns, it will be identified by this tag. This
        ///     is also used when canceling the market depth.
        /// </param>
        /// <param name="contract">
        /// this structure contains a description of the contract for which market depth data is being requested.
        /// </param>
        /// <param name="numberOfRows">
        /// specifies the number of market depth rows to return.
        /// </param>
        public void RequestMarketDepth(int tickerId, Contract contract, int numberOfRows)
        {
            if (contract == null)
            {
                throw new ArgumentNullException("contract");
            }

            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                // This feature is only available for versions of TWS >=6
                if (serverVersion < 6)
                {
                    error(ErrorMessage.UpdateTws, "It does not support market depth.");
                    return;
                }

                var version = 3;

                try
                {
                    // send req mkt data msg
                    send((int)OutgoingMessage.RequestMarketDepth);
                    send(version);
                    send(tickerId);

                    // Request Contract Fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(
                        (contract.Right == RightType.Undefined)
                            ? string.Empty
                            : EnumDescConverter.GetEnumDescription(contract.Right));
                    if (serverVersion >= 15)
                    {
                        send(contract.Multiplier);
                    }

                    send(contract.Exchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    if (serverVersion >= 19)
                    {
                        send(numberOfRows);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendRequestMarketDepth, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call this method to start receiving news bulletins. Each bulletin will be returned by the updateNewsBulletin()
        ///     method.
        /// </summary>
        /// <param name="allMessages">
        /// if set to TRUE, returns all the existing bulletins for the current day and any new ones. IF set to FALSE, will only
        ///     return new bulletins.
        /// </param>
        public void RequestNewsBulletins(bool allMessages)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                var version = 1;

                try
                {
                    send((int)OutgoingMessage.RequestNewsBulletins);
                    send(version);
                    send(allMessages);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendCancelOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        ///     Call this method to request the open orders that were placed from this client. Each open order will be fed back
        ///     through the openOrder() and orderStatus() functions on the EWrapper.
        ///     The client with a clientId of "0" will also receive the TWS-owned open orders. These orders will be associated with
        ///     the client and a new orderId will be generated. This association will persist over multiple API and TWS sessions.
        /// </summary>
        public void RequestOpenOrders()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                var version = 1;

                // send cancel order msg
                try
                {
                    send((int)OutgoingMessage.RequestOpenOrders);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendOpenOrder, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the reqRealTimeBars() method to start receiving real time bar results through the realtimeBar() EWrapper
        ///     method.
        /// </summary>
        /// <param name="tickerId">
        /// The Id for the request. Must be a unique value. When the data is received, it will be identified
        ///     by this Id. This is also used when canceling the historical data request.
        /// </param>
        /// <param name="contract">
        /// This structure contains a description of the contract for which historical data is being requested.
        /// </param>
        /// <param name="barSize">
        /// Currently only 5 second bars are supported, if any other value is used, an exception will be thrown.
        /// </param>
        /// <param name="whatToShow">
        /// Determines the nature of the data extracted. Valid values include:
        ///     TRADES
        ///     BID
        ///     ASK
        ///     MIDPOINT
        /// </param>
        /// <param name="useRth">
        /// useRth – Regular Trading Hours only. Valid values include:
        ///     0 = all data available during the time span requested is returned, including time intervals when the market in
        ///     question was outside of regular trading hours.
        ///     1 = only data within the regular trading hours for the product requested is returned, even if the time time span
        ///     falls partially or completely outside.
        /// </param>
        public void RequestRealTimeBars(
            int tickerId, 
            Contract contract, 
            int barSize, 
            RealTimeBarType whatToShow, 
            bool useRth)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(tickerId, ErrorMessage.NotConnected);
                    return;
                }

                // 34 is the minimum version for real time bars
                if (serverVersion < MinServerVersion.RealTimeBars)
                {
                    error(ErrorMessage.UpdateTws, "It does not support real time bars.");
                    return;
                }

                var version = 1;

                try
                {
                    // send req mkt data msg
                    send((int)OutgoingMessage.RequestRealTimeBars);
                    send(version);
                    send(tickerId);

                    // Send Contract Fields
                    send(contract.Symbol);
                    send(EnumDescConverter.GetEnumDescription(contract.SecurityType));
                    send(contract.Expiry);
                    send(contract.Strike);
                    send(EnumDescConverter.GetEnumDescription(contract.Right));
                    send(contract.Multiplier);
                    send(contract.Exchange);
                    send(contract.PrimaryExchange);
                    send(contract.Currency);
                    send(contract.LocalSymbol);
                    send(barSize);
                    send(EnumDescConverter.GetEnumDescription(whatToShow));
                    send(useRth);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendRequestRealTimeBars, e);
                    close();
                }
            }
        }

        /// <summary>
        ///     Call the reqScannerParameters() method to receive an XML document that describes the valid parameters that a
        ///     scanner subscription can have.
        /// </summary>
        public void RequestScannerParameters()
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < 24)
                {
                    error(ErrorMessage.UpdateTws, "It does not support API scanner subscription.");
                    return;
                }

                var version = 1;

                try
                {
                    send((int)OutgoingMessage.RequestScannerParameters);
                    send(version);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) | throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendRequestScannerParameters, e);
                    close();
                }
            }
        }

        /// <summary>
        /// Call the reqScannerSubscription() method to start receiving market scanner results through the scannerData()
        ///     EWrapper method.
        /// </summary>
        /// <param name="tickerId">
        /// the Id for the subscription. Must be a unique value. When the subscription  data is received, it will be identified
        ///     by this Id. This is also used when canceling the scanner.
        /// </param>
        /// <param name="subscription">
        /// summary of the scanner subscription parameters including filters.
        /// </param>
        public void RequestScannerSubscription(int tickerId, ScannerSubscription subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException("subscription");
            }

            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected);
                    return;
                }

                if (serverVersion < 24)
                {
                    error(ErrorMessage.UpdateTws, "It does not support API scanner subscription.");
                    return;
                }

                var version = 3;

                try
                {
                    send((int)OutgoingMessage.RequestScannerSubscription);
                    send(version);
                    send(tickerId);
                    sendMax(subscription.NumberOfRows);
                    send(subscription.Instrument);
                    send(subscription.LocationCode);
                    send(subscription.ScanCode);
                    sendMax(subscription.AbovePrice);
                    sendMax(subscription.BelowPrice);
                    sendMax(subscription.AboveVolume);
                    sendMax(subscription.MarketCapAbove);
                    sendMax(subscription.MarketCapBelow);
                    send(subscription.MoodyRatingAbove);
                    send(subscription.MoodyRatingBelow);
                    send(subscription.SPRatingAbove);
                    send(subscription.SPRatingBelow);
                    send(subscription.MaturityDateAbove);
                    send(subscription.MaturityDateBelow);
                    sendMax(subscription.CouponRateAbove);
                    sendMax(subscription.CouponRateBelow);
                    send(subscription.ExcludeConvertible);
                    if (serverVersion >= 25)
                    {
                        send(subscription.AverageOptionVolumeAbove);
                        send(subscription.ScannerSettingPairs);
                    }

                    if (serverVersion >= 27)
                    {
                        send(subscription.StockTypeFilter);
                    }
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(tickerId, ErrorMessage.FailSendRequestScanner, e);
                    close();
                }
            }
        }

        /// <summary>
        /// The default level is ERROR. Refer to the API logging page for more details.
        /// </summary>
        /// <param name="serverLogLevel">
        /// logLevel - specifies the level of log entry detail used by the server (TWS) when processing API requests. Valid
        ///     values include:
        ///     1 = SYSTEM
        ///     2 = ERROR
        ///     3 = WARNING
        ///     4 = INFORMATION
        ///     5 = DETAIL
        /// </param>
        public void SetServerLogLevel(LogLevel serverLogLevel)
        {
            lock (this)
            {
                // not connected?
                if (!connected)
                {
                    error(ErrorMessage.NotConnected, string.Empty);
                    return;
                }

                var version = 1;

                // send the set server logging level message
                try
                {
                    send((int)OutgoingMessage.SetServerLogLevel);
                    send(version);
                    send((int)serverLogLevel);
                }
                catch (Exception e)
                {
                    if (!(e is ObjectDisposedException || e is IOException) || throwExceptions)
                    {
                        throw;
                    }

                    error(ErrorMessage.FailSendServerLogLevel, e.ToString());
                    close();
                }
            }
        }

        /// <summary>
        ///     Tells the worker thread to stop, typically after completing its
        ///     current work item. (The thread is *not* guaranteed to have stopped
        ///     by the time this method returns.)
        /// </summary>
        public void Stop()
        {
            lock (stopLock)
            {
                stopping = true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Contains the reader thread.
        /// </summary>
        internal void Run()
        {
            try
            {
                // loop until thread is terminated
                while (!Stopping && ProcessMsg((IncomingMessage)ReadInt()))
                {
                    ;
                }
            }
            catch (IOException)
            {
            }
            catch (Exception ex)
            {
                if (throwExceptions)
                {
                    throw;
                }

                exception(ex);
            }
            finally
            {
                SetStopped();
                connectionClosed();
                close();
            }
        }

        /// <summary>
        ///     Forks the reading thread
        /// </summary>
        internal void Start()
        {
            if (!Stopping)
            {
                readThread.Start();
            }
        }

        /// <summary>
        /// used for reqHistoricalData
        /// </summary>
        /// <param name="StartTime">
        /// The Start Time.
        /// </param>
        /// <param name="EndTime">
        /// The End Time.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected static string ConvertPeriodtoIb(DateTime StartTime, DateTime EndTime)
        {
            var period = EndTime.Subtract(StartTime);
            var secs = period.TotalSeconds;
            long unit;

            if (secs < 1)
            {
                throw new ArgumentOutOfRangeException("Period cannot be less than 1 second.");
            }

            if (secs < 86400)
            {
                unit = (long)Math.Ceiling(secs);
                return string.Concat(unit, " S");
            }

            var days = secs / 86400;

            unit = (long)Math.Ceiling(days);
            if (unit <= 34)
            {
                return string.Concat(unit, " D");
            }

            var weeks = days / 7;
            unit = (long)Math.Ceiling(weeks);
            if (unit > 52)
            {
                throw new ArgumentOutOfRangeException("Period cannot be bigger than 52 weeks.");
            }

            return string.Concat(unit, " W");
        }

        /// <summary>
        /// The bulk of the clean-up code is implemented in Dispose(bool)
        /// </summary>
        /// <param name="disposing">
        /// Allows the ondispose method to override the dispose action.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                close();
                if (dos != null)
                {
                    dos.Close();
                    dos = null;
                }

                if (dis != null)
                {
                    dis.Close();
                    dis = null;
                }
            }
        }

        /// <summary>
        /// Called internally when the receive thread receives a Account Download End Event.
        /// </summary>
        /// <param name="e">
        /// Contract Details End Event Arguments
        /// </param>
        protected virtual void OnAccountDownloadEnd(AccountDownloadEndEventArgs e)
        {
            RaiseEvent(AccountDownloadEnd, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a Bond Contract Details Event.
        /// </summary>
        /// <param name="e">
        /// Bond Contract Details Event Arguments
        /// </param>
        protected virtual void OnBondContractDetails(BondContractDetailsEventArgs e)
        {
            RaiseEvent(BondContractDetails, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a Market Data Type Event.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void OnCommissionReport(CommissionReportEventArgs e)
        {
            RaiseEvent(CommissionReport, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a connection closed event.
        /// </summary>
        /// <param name="e">
        /// Connection Closed Event Arguments
        /// </param>
        protected virtual void OnConnectionClosed(ConnectionClosedEventArgs e)
        {
            RaiseEvent(ConnectionClosed, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a contract details event.
        /// </summary>
        /// <param name="e">
        /// Contract Details Event Arguments
        /// </param>
        protected virtual void OnContractDetails(ContractDetailsEventArgs e)
        {
            RaiseEvent(ContractDetails, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a Contract Details End Event.
        /// </summary>
        /// <param name="e">
        /// Contract Details End Event Arguments
        /// </param>
        protected virtual void OnContractDetailsEnd(ContractDetailsEndEventArgs e)
        {
            RaiseEvent(ContractDetailsEnd, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a current time event.
        /// </summary>
        /// <param name="e">
        /// Current Time Event Arguments
        /// </param>
        protected virtual void OnCurrentTime(CurrentTimeEventArgs e)
        {
            RaiseEvent(CurrentTime, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a Contract Details End Event.
        /// </summary>
        /// <param name="e">
        /// Contract Details End Event Arguments
        /// </param>
        protected virtual void OnDeltaNuetralValidation(DeltaNuetralValidationEventArgs e)
        {
            RaiseEvent(DeltaNuetralValidation, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives an error event.
        /// </summary>
        /// <param name="e">
        /// Error Event Arguments
        /// </param>
        protected virtual void OnError(ErrorEventArgs e)
        {
            RaiseEvent(Error, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives an execution details event.
        /// </summary>
        /// <param name="e">
        /// Execution Details Event Arguments
        /// </param>
        protected virtual void OnExecDetails(ExecDetailsEventArgs e)
        {
            if (ExecDetails != null)
            {
                ExecDetails(this, e);
            }
        }

        /// <summary>
        /// Called internally when the receive thread receives a Contract Details End Event.
        /// </summary>
        /// <param name="e">
        /// Contract Details End Event Arguments
        /// </param>
        protected virtual void OnExecutionDataEnd(ExecutionDataEndEventArgs e)
        {
            RaiseEvent(ExecutionDataEnd, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a fundamental data event.
        /// </summary>
        /// <param name="e">
        /// Fundamental Data Event Arguments
        /// </param>
        protected virtual void OnFundamentalData(FundamentalDetailsEventArgs e)
        {
            RaiseEvent(FundamentalData, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a tick price event.
        /// </summary>
        /// <param name="e">
        /// Historical Data Event Arguments
        /// </param>
        protected virtual void OnHistoricalData(HistoricalDataEventArgs e)
        {
            RaiseEvent(HistoricalData, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a managed accounts event.
        /// </summary>
        /// <param name="e">
        /// Managed Accounts Event Arguments
        /// </param>
        protected virtual void OnManagedAccounts(ManagedAccountsEventArgs e)
        {
            RaiseEvent(ManagedAccounts, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a Market Data Type Event.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void OnMarketDataType(MarketDataTypeEventArgs e)
        {
            RaiseEvent(MarketDataType, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a Next Valid Id event.
        /// </summary>
        /// <param name="e">
        /// Next Valid Id Event Arguments
        /// </param>
        protected virtual void OnNextValidId(NextValidIdEventArgs e)
        {
            RaiseEvent(NextValidId, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives an open order event.
        /// </summary>
        /// <param name="e">
        /// Open Order Event Arguments
        /// </param>
        protected virtual void OnOpenOrder(OpenOrderEventArgs e)
        {
            RaiseEvent(OpenOrder, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a Open Orders End Event.
        /// </summary>
        /// <param name="e">
        /// Empty Event Arguments
        /// </param>
        protected virtual void OnOpenOrderEnd(EventArgs e)
        {
            RaiseEvent(OpenOrderEnd, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives an order status event.
        /// </summary>
        /// <param name="e">
        /// Order Status Event Arguments
        /// </param>
        protected virtual void OnOrderStatus(OrderStatusEventArgs e)
        {
            RaiseEvent(OrderStatus, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a real time bar event.
        /// </summary>
        /// <param name="e">
        /// Real Time Bar Event Arguments
        /// </param>
        protected virtual void OnRealTimeBar(RealTimeBarEventArgs e)
        {
            RaiseEvent(RealTimeBar, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a Receive Finanvial Advisor event.
        /// </summary>
        /// <param name="e">
        /// Receive FA Event Arguments
        /// </param>
        protected virtual void OnReceiveFA(ReceiveFAEventArgs e)
        {
            RaiseEvent(ReceiveFA, this, e);
        }

        /// <summary>
        /// Called internally when a exception is being thrown
        /// </summary>
        /// <param name="e">
        /// </param>
        protected virtual void OnReportException(ReportExceptionEventArgs e)
        {
            RaiseEvent(ReportException, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a tick price event.
        /// </summary>
        /// <param name="e">
        /// Scanner Data Event Arguments
        /// </param>
        protected virtual void OnScannerData(ScannerDataEventArgs e)
        {
            RaiseEvent(ScannerData, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a tick price event.
        /// </summary>
        /// <param name="e">
        /// Scanner Data Event Arguments
        /// </param>
        protected virtual void OnScannerDataEnd(ScannerDataEndEventArgs e)
        {
            RaiseEvent(ScannerDataEnd, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a scanner parameters event.
        /// </summary>
        /// <param name="e">
        /// Scanner Parameters Event Arguments
        /// </param>
        protected virtual void OnScannerParameters(ScannerParametersEventArgs e)
        {
            RaiseEvent(ScannerParameters, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a tick EFP event.
        /// </summary>
        /// <param name="e">
        /// Tick Efp Arguments
        /// </param>
        protected virtual void OnTickEfp(TickEfpEventArgs e)
        {
            RaiseEvent(TickEfp, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a generic tick event.
        /// </summary>
        /// <param name="e">
        /// Tick Generic Event Arguments
        /// </param>
        protected virtual void OnTickGeneric(TickGenericEventArgs e)
        {
            RaiseEvent(TickGeneric, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a tick option computation event.
        /// </summary>
        /// <param name="e">
        /// Tick Option Computation Arguments
        /// </param>
        protected virtual void OnTickOptionComputation(TickOptionComputationEventArgs e)
        {
            RaiseEvent(TickOptionComputation, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a tick price event.
        /// </summary>
        /// <param name="e">
        /// Tick Price event arguments
        /// </param>
        protected virtual void OnTickPrice(TickPriceEventArgs e)
        {
            RaiseEvent(TickPrice, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a tick size event.
        /// </summary>
        /// <param name="e">
        /// Tick Size Event Arguments
        /// </param>
        protected virtual void OnTickSize(TickSizeEventArgs e)
        {
            RaiseEvent(TickSize, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a Tick Snapshot End Event.
        /// </summary>
        /// <param name="e">
        /// Contract Details End Event Arguments
        /// </param>
        protected virtual void OnTickSnapshotEnd(TickSnapshotEndEventArgs e)
        {
            RaiseEvent(TickSnapshotEnd, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives a Tick String  event.
        /// </summary>
        /// <param name="e">
        /// Tick String Event Arguments
        /// </param>
        protected virtual void OnTickString(TickStringEventArgs e)
        {
            RaiseEvent(TickString, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives an Update Account Time event.
        /// </summary>
        /// <param name="e">
        /// Update Account Time Event Arguments
        /// </param>
        protected virtual void OnUpdateAccountTime(UpdateAccountTimeEventArgs e)
        {
            RaiseEvent(UpdateAccountTime, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives an Update Account Value event.
        /// </summary>
        /// <param name="e">
        /// Update Account Value Event Arguments
        /// </param>
        protected virtual void OnUpdateAccountValue(UpdateAccountValueEventArgs e)
        {
            RaiseEvent(UpdateAccountValue, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives an update market depth event.
        /// </summary>
        /// <param name="e">
        /// </param>
        protected virtual void OnUpdateMarketDepth(UpdateMarketDepthEventArgs e)
        {
            RaiseEvent(UpdateMarketDepth, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives an update market depth level 2 event.
        /// </summary>
        /// <param name="e">
        /// Update Market Depth L2 Event Arguments
        /// </param>
        protected virtual void OnUpdateMarketDepthL2(UpdateMarketDepthL2EventArgs e)
        {
            RaiseEvent(UpdateMarketDepthL2, this, e);
        }

        /// <summary>
        /// Called internally when the receive thread receives an update news bulletin event.
        /// </summary>
        /// <param name="e">
        /// Update News Bulletin Event Arguments
        /// </param>
        protected virtual void OnUpdateNewsBulletin(UpdateNewsBulletinEventArgs e)
        {
            RaiseEvent(UpdateNewsBulletin, this, e);
        }

        /// <summary>
        /// Called Internally when the receive thread receives an Update Portfolio event.
        /// </summary>
        /// <param name="e">
        /// Update Portfolio Event Arguments
        /// </param>
        protected virtual void OnUpdatePortfolio(UpdatePortfolioEventArgs e)
        {
            RaiseEvent(UpdatePortfolio, this, e);
        }

        /// <summary>
        /// Raise the event in a threadsafe manner
        /// </summary>
        /// <param name="event">
        /// </param>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        private static void RaiseEvent<T>(EventHandler<T> @event, object sender, T e) where T : EventArgs
        {
            var handler = @event;
            if (handler == null)
            {
                return;
            }

            handler(sender, e);
        }

        /// <summary>
        /// Converts a string to an array of bytes
        /// </summary>
        /// <param name="source">
        /// The string to be converted
        /// </param>
        /// <returns>
        /// The new array of bytes
        /// </returns>
        private static byte[] ToByteArray(string source)
        {
            return Encoding.UTF8.GetBytes(source);
        }

        /// <summary>
        /// Overridden in subclass.
        /// </summary>
        /// <param name="msgId">
        /// The msg Id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ProcessMsg(IncomingMessage msgId)
        {
            if (msgId == IncomingMessage.Error)
            {
                return false;
            }

            switch (msgId)
            {
                case IncomingMessage.TickPrice:
                    {
                        var version = ReadInt();
                        var tickerId = ReadInt();
                        var tickType = ReadInt();
                        var price = ReadDecimal();
                        var size = 0;
                        if (version >= 2)
                        {
                            size = ReadInt();
                        }

                        var canAutoExecute = 0;
                        if (version >= 3)
                        {
                            canAutoExecute = ReadInt();
                        }

                        tickPrice(tickerId, (TickType)tickType, price, canAutoExecute != 0);

                        if (version >= 2)
                        {
                            var sizeTickType = -1; // not a tick
                            switch (tickType)
                            {
                                case 1: // BID
                                    sizeTickType = 0; // BID_SIZE
                                    break;

                                case 2: // ASK
                                    sizeTickType = 3; // ASK_SIZE
                                    break;

                                case 4: // LAST
                                    sizeTickType = 5; // LAST_SIZE
                                    break;
                            }

                            if (sizeTickType != -1)
                            {
                                tickSize(tickerId, (TickType)sizeTickType, size);
                            }
                        }

                        break;
                    }

                case IncomingMessage.TickSize:
                    {
                        var version = ReadInt();
                        var tickerId = ReadInt();
                        var tickType = ReadInt();
                        var size = ReadInt();

                        tickSize(tickerId, (TickType)tickType, size);
                        break;
                    }

                case IncomingMessage.TickOptionComputation:
                    {
                        var version = ReadInt();
                        var tickerId = ReadInt();
                        var tickType = (TickType)ReadInt();
                        var impliedVol = ReadDouble();
                        if (impliedVol < 0)
                        {
                            // -1 is the "not yet computed" indicator
                            impliedVol = double.MaxValue;
                        }

                        var delta = ReadDouble();
                        if (Math.Abs(delta) > 1)
                        {
                            // -2 is the "not yet computed" indicator
                            delta = double.MaxValue;
                        }

                        var optPrice = double.MaxValue;
                        var pvDividend = double.MaxValue;
                        var gamma = double.MaxValue;
                        var vega = double.MaxValue;
                        var theta = double.MaxValue;
                        var undPrice = double.MaxValue;
                        if (version >= 6 || tickType == TickType.ModelOption)
                        {
                            // introduced in version == 5
                            optPrice = ReadDouble();
                            if (optPrice < 0)
                            {
                                // -1 is the "not yet computed" indicator
                                optPrice = double.MaxValue;
                            }

                            pvDividend = ReadDouble();
                            if (pvDividend < 0)
                            {
                                // -1 is the "not yet computed" indicator
                                pvDividend = double.MaxValue;
                            }
                        }

                        if (version >= 6)
                        {
                            gamma = ReadDouble();
                            if (Math.Abs(gamma) > 1)
                            {
                                // -2 is the "not yet computed" indicator
                                gamma = double.MaxValue;
                            }

                            vega = ReadDouble();
                            if (Math.Abs(vega) > 1)
                            {
                                // -2 is the "not yet computed" indicator
                                vega = double.MaxValue;
                            }

                            theta = ReadDouble();
                            if (Math.Abs(theta) > 1)
                            {
                                // -2 is the "not yet computed" indicator
                                theta = double.MaxValue;
                            }

                            undPrice = ReadDouble();
                            if (undPrice < 0)
                            {
                                // -1 is the "not yet computed" indicator
                                undPrice = double.MaxValue;
                            }
                        }

                        tickOptionComputation(
                            tickerId, 
                            tickType, 
                            impliedVol, 
                            delta, 
                            optPrice, 
                            pvDividend, 
                            gamma, 
                            vega, 
                            theta, 
                            undPrice);
                        break;
                    }

                case IncomingMessage.TickGeneric:
                    {
                        var version = ReadInt();
                        var tickerId = ReadInt();
                        var tickType = ReadInt();
                        var value_Renamed = ReadDouble();

                        tickGeneric(tickerId, (TickType)tickType, value_Renamed);
                        break;
                    }

                case IncomingMessage.TickString:
                    {
                        var version = ReadInt();
                        var tickerId = ReadInt();
                        var tickType = ReadInt();
                        var value_Renamed = ReadStr();

                        tickString(tickerId, (TickType)tickType, value_Renamed);
                        break;
                    }

                case IncomingMessage.TickEfp:
                    {
                        var version = ReadInt();
                        var tickerId = ReadInt();
                        var tickType = ReadInt();
                        var basisPoints = ReadDouble();
                        var formattedBasisPoints = ReadStr();
                        var impliedFuturesPrice = ReadDouble();
                        var holdDays = ReadInt();
                        var futureExpiry = ReadStr();
                        var dividendImpact = ReadDouble();
                        var dividendsToExpiry = ReadDouble();
                        tickEfp(
                            tickerId, 
                            (TickType)tickType, 
                            basisPoints, 
                            formattedBasisPoints, 
                            impliedFuturesPrice, 
                            holdDays, 
                            futureExpiry, 
                            dividendImpact, 
                            dividendsToExpiry);
                        break;
                    }

                case IncomingMessage.OrderStatus:
                    {
                        var version = ReadInt();
                        var id = ReadInt();
                        var orderstat = ReadStr();
                        var status = string.IsNullOrEmpty(orderstat)
                                         ? IBNet.OrderStatus.None
                                         : (OrderStatus)EnumDescConverter.GetEnumValue(typeof(OrderStatus), orderstat);
                        var filled = ReadInt();
                        var remaining = ReadInt();
                        var avgFillPrice = ReadDecimal();

                        var permId = 0;
                        if (version >= 2)
                        {
                            permId = ReadInt();
                        }

                        var parentId = 0;
                        if (version >= 3)
                        {
                            parentId = ReadInt();
                        }

                        decimal lastFillPrice = 0;
                        if (version >= 4)
                        {
                            lastFillPrice = ReadDecimal();
                        }

                        var clientId = 0;
                        if (version >= 5)
                        {
                            clientId = ReadInt();
                        }

                        string whyHeld = null;
                        if (version >= 6)
                        {
                            whyHeld = ReadStr();
                        }

                        orderStatus(
                            id, 
                            status, 
                            filled, 
                            remaining, 
                            avgFillPrice, 
                            permId, 
                            parentId, 
                            lastFillPrice, 
                            clientId, 
                            whyHeld);
                        break;
                    }

                case IncomingMessage.AccountValue:
                    {
                        var version = ReadInt();
                        var key = ReadStr();
                        var val = ReadStr();
                        var cur = ReadStr();
                        string accountName = null;
                        if (version >= 2)
                        {
                            accountName = ReadStr();
                        }

                        updateAccountValue(key, val, cur, accountName);
                        break;
                    }

                case IncomingMessage.PortfolioValue:
                    {
                        var version = ReadInt();
                        var contract = new Contract();
                        if (version >= 6)
                        {
                            contract.ContractId = ReadInt();
                        }

                        contract.Symbol = ReadStr();
                        contract.SecurityType =
                            (SecurityType)EnumDescConverter.GetEnumValue(typeof(SecurityType), ReadStr());
                        contract.Expiry = ReadStr();
                        contract.Strike = ReadDouble();
                        var rstr = ReadStr();
                        contract.Right = rstr == null || rstr.Length <= 0 || rstr.Equals("?") || rstr.Equals("0")
                                             ? RightType.Undefined
                                             : (RightType)EnumDescConverter.GetEnumValue(typeof(RightType), rstr);

                        if (version >= 7)
                        {
                            contract.Multiplier = ReadStr();
                            contract.PrimaryExchange = ReadStr();
                        }

                        contract.Currency = ReadStr();
                        if (version >= 2)
                        {
                            contract.LocalSymbol = ReadStr();
                        }

                        var position = ReadInt();
                        var marketPrice = ReadDecimal();
                        var marketValue = ReadDecimal();
                        var averageCost = 0.0m;
                        var unrealizedPNL = 0.0m;
                        var realizedPNL = 0.0m;
                        if (version >= 3)
                        {
                            averageCost = ReadDecimal();
                            unrealizedPNL = ReadDecimal();
                            realizedPNL = ReadDecimal();
                        }

                        string accountName = null;
                        if (version >= 4)
                        {
                            accountName = ReadStr();
                        }

                        if (version == 6 && serverVersion == 39)
                        {
                            contract.PrimaryExchange = ReadStr();
                        }

                        updatePortfolio(
                            contract, 
                            position, 
                            marketPrice, 
                            marketValue, 
                            averageCost, 
                            unrealizedPNL, 
                            realizedPNL, 
                            accountName);

                        break;
                    }

                case IncomingMessage.AccountUpdateTime:
                    {
                        var version = ReadInt();
                        var timeStamp = ReadStr();
                        updateAccountTime(timeStamp);
                        break;
                    }

                case IncomingMessage.ErrorMessage:
                    {
                        var version = ReadInt();
                        if (version < 2)
                        {
                            var msg = ReadStr();
                            error(msg);
                        }
                        else
                        {
                            var id = ReadInt();
                            var errorCode = ReadInt();
                            var errorMsg = ReadStr();
                            error(id, (ErrorMessage)errorCode, errorMsg);
                        }

                        break;
                    }

                case IncomingMessage.OpenOrder:
                    {
                        // read version
                        var version = ReadInt();

                        // read order id
                        var order = new Order();
                        order.OrderId = ReadInt();

                        // read contract fields
                        var contract = new Contract();
                        if (version >= 17)
                        {
                            contract.ContractId = ReadInt();
                        }

                        contract.Symbol = ReadStr();
                        contract.SecurityType =
                            (SecurityType)EnumDescConverter.GetEnumValue(typeof(SecurityType), ReadStr());
                        contract.Expiry = ReadStr();
                        contract.Strike = ReadDouble();
                        var rstr = ReadStr();
                        contract.Right = string.IsNullOrEmpty(rstr) || rstr.Equals("?")
                                             ? RightType.Undefined
                                             : (RightType)EnumDescConverter.GetEnumValue(typeof(RightType), rstr);
                        contract.Exchange = ReadStr();
                        contract.Currency = ReadStr();
                        if (version >= 2)
                        {
                            contract.LocalSymbol = ReadStr();
                        }

                        // read order fields
                        order.Action = (ActionSide)EnumDescConverter.GetEnumValue(typeof(ActionSide), ReadStr());
                        order.TotalQuantity = ReadInt();
                        order.OrderType = (OrderType)EnumDescConverter.GetEnumValue(typeof(OrderType), ReadStr());
                        order.LimitPrice = ReadDecimal();
                        order.AuxPrice = ReadDecimal();
                        order.Tif = (TimeInForce)EnumDescConverter.GetEnumValue(typeof(TimeInForce), ReadStr());
                        order.OcaGroup = ReadStr();
                        order.Account = ReadStr();
                        order.OpenClose = ReadStr();
                        order.Origin = (OrderOrigin)ReadInt();
                        order.OrderRef = ReadStr();

                        if (version >= 3)
                        {
                            order.ClientId = ReadInt();
                        }

                        if (version >= 4)
                        {
                            order.PermId = ReadInt();
                            if (version < 18)
                            {
                                // will never happen
                                /* order.m_ignoreRth = */
                                ReadBoolFromInt();
                            }
                            else
                            {
                                order.OutsideRth = ReadBoolFromInt();
                            }

                            order.Hidden = ReadInt() == 1;
                            order.DiscretionaryAmt = ReadDecimal();
                        }

                        if (version >= 5)
                        {
                            order.GoodAfterTime = ReadStr();
                        }

                        if (version >= 6)
                        {
                            // skip deprecated sharesAllocation field
                            ReadStr();
                        }

                        if (version >= 7)
                        {
                            order.FAGroup = ReadStr();
                            var fam = ReadStr();
                            order.FAMethod = string.IsNullOrEmpty(fam)
                                                 ? FinancialAdvisorAllocationMethod.None
                                                 : (FinancialAdvisorAllocationMethod)
                                                   EnumDescConverter.GetEnumValue(
                                                       typeof(FinancialAdvisorAllocationMethod), 
                                                       fam);
                            order.FAPercentage = ReadStr();
                            order.FAProfile = ReadStr();
                        }

                        if (version >= 8)
                        {
                            order.GoodTillDate = ReadStr();
                        }

                        if (version >= 9)
                        {
                            rstr = ReadStr();
                            order.Rule80A = string.IsNullOrEmpty(rstr)
                                                ? AgentDescription.None
                                                : (AgentDescription)
                                                  EnumDescConverter.GetEnumValue(typeof(AgentDescription), rstr);
                            order.PercentOffset = ReadDouble();
                            order.SettlingFirm = ReadStr();
                            order.ShortSaleSlot = (ShortSaleSlot)ReadInt();
                            order.DesignatedLocation = ReadStr();
                            if (serverVersion == 51)
                            {
                                ReadInt(); // exempt code
                            }
                            else if (version >= 23)
                            {
                                order.ExemptCode = ReadInt();
                            }

                            order.AuctionStrategy = (AuctionStrategy)ReadInt();
                            order.StartingPrice = ReadDecimal();
                            order.StockRefPrice = ReadDouble();
                            order.Delta = ReadDouble();
                            order.StockRangeLower = ReadDouble();
                            order.StockRangeUpper = ReadDouble();
                            order.DisplaySize = ReadInt();
                            if (version < 18)
                            {
                                // will never happen
                                /* order.m_rthOnly = */
                                ReadBoolFromInt();
                            }

                            order.BlockOrder = ReadBoolFromInt();
                            order.SweepToFill = ReadBoolFromInt();
                            order.AllOrNone = ReadBoolFromInt();
                            order.MinQty = ReadInt();
                            order.OcaType = (OcaType)ReadInt();
                            order.ETradeOnly = ReadBoolFromInt();
                            order.FirmQuoteOnly = ReadBoolFromInt();
                            order.NbboPriceCap = ReadDecimal();
                        }

                        if (version >= 10)
                        {
                            order.ParentId = ReadInt();
                            order.TriggerMethod = (TriggerMethod)ReadInt();
                        }

                        if (version >= 11)
                        {
                            order.Volatility = ReadDouble();
                            rstr = ReadStr();
                            int i;
                            order.VolatilityType = int.TryParse(rstr, out i)
                                                       ? (VolatilityType)i
                                                       : VolatilityType.Undefined;
                            if (version == 11)
                            {
                                var receivedInt = ReadInt();
                                order.DeltaNeutralOrderType = (receivedInt == 0) ? OrderType.Empty : OrderType.Market;
                            }
                            else
                            {
                                // version 12 and up
                                var dnoa = ReadStr();
                                order.DeltaNeutralOrderType = string.IsNullOrEmpty(dnoa)
                                                                  ? OrderType.Empty
                                                                  : (OrderType)
                                                                    EnumDescConverter.GetEnumValue(
                                                                        typeof(OrderType), 
                                                                        dnoa);
                                order.DeltaNeutralAuxPrice = ReadDouble();

                                if (version >= 27 && order.DeltaNeutralOrderType != OrderType.Empty)
                                {
                                    order.DeltaNeutralConId = ReadInt();
                                    order.DeltaNeutralSettlingFirm = ReadStr();
                                    order.DeltaNeutralClearingAccount = ReadStr();
                                    order.DeltaNeutralClearingIntent = ReadStr();
                                }

                                if (version >= 31 && !string.IsNullOrEmpty(dnoa))
                                {
                                    order.DeltaNeutralOpenClose = ReadStr();
                                    order.DeltaNeutralShortSale = ReadBoolFromInt();
                                    order.DeltaNeutralShortSaleSlot = ReadInt();
                                    order.DeltaNeutralDesignatedLocation = ReadStr();
                                }
                            }

                            order.ContinuousUpdate = ReadInt();
                            if (serverVersion == 26)
                            {
                                order.StockRangeLower = ReadDouble();
                                order.StockRangeUpper = ReadDouble();
                            }

                            order.ReferencePriceType = ReadInt();
                        }

                        if (version >= 13)
                        {
                            order.TrailStopPrice = ReadDecimal();
                        }

                        if (version >= 30)
                        {
                            order.TrailingPercent = ReadDoubleMax();
                        }

                        if (version >= 14)
                        {
                            order.BasisPoints = ReadDecimal();
                            order.BasisPointsType = ReadInt();
                            contract.ComboLegsDescription = ReadStr();
                        }

                        if (version >= 29)
                        {
                            var comboLegsCount = ReadInt();
                            if (comboLegsCount > 0)
                            {
                                contract.ComboLegs = new Collection<ComboLeg>();
                                for (var i = 0; i < comboLegsCount; ++i)
                                {
                                    var conId = ReadInt();
                                    var ratio = ReadInt();
                                    var action =
                                        (ActionSide)EnumDescConverter.GetEnumValue(typeof(ActionSide), ReadStr());
                                    var exchange = ReadStr();
                                    var openClose = (ComboOpenClose)ReadInt();
                                    var shortSaleSlot = (ShortSaleSlot)ReadInt();
                                    var designatedLocation = ReadStr();
                                    var exemptCode = ReadInt();

                                    var comboLeg = new ComboLeg(
                                        conId, 
                                        ratio, 
                                        action, 
                                        exchange, 
                                        openClose, 
                                        shortSaleSlot, 
                                        designatedLocation, 
                                        exemptCode);
                                    contract.ComboLegs.Add(comboLeg);
                                }
                            }

                            var orderComboLegsCount = ReadInt();
                            if (orderComboLegsCount > 0)
                            {
                                order.OrderComboLegs = new Collection<OrderComboLeg>();
                                for (var i = 0; i < orderComboLegsCount; ++i)
                                {
                                    var price = ReadDoubleMax();

                                    var orderComboLeg = new OrderComboLeg(price);
                                    order.OrderComboLegs.Add(orderComboLeg);
                                }
                            }
                        }

                        if (version >= 26)
                        {
                            var smartComboRoutingParamsCount = ReadInt();
                            if (smartComboRoutingParamsCount > 0)
                            {
                                order.SmartComboRoutingParams = new Collection<TagValue>();
                                for (var i = 0; i < smartComboRoutingParamsCount; ++i)
                                {
                                    var tagValue = new TagValue();
                                    tagValue.Tag = ReadStr();
                                    tagValue.Value = ReadStr();
                                    order.SmartComboRoutingParams.Add(tagValue);
                                }
                            }
                        }

                        if (version >= 15)
                        {
                            if (version >= 20)
                            {
                                order.ScaleInitLevelSize = ReadIntMax();
                                order.ScaleSubsLevelSize = ReadIntMax();
                            }
                            else
                            {
                                /* int notSuppScaleNumComponents = */
                                ReadIntMax();
                                order.ScaleInitLevelSize = ReadIntMax();
                            }

                            order.ScalePriceIncrement = ReadDecimalMax();
                        }

                        if (version >= 28 && order.ScalePriceIncrement > 0.0m
                            && order.ScalePriceIncrement != decimal.MaxValue)
                        {
                            order.ScalePriceAdjustValue = ReadDoubleMax();
                            order.ScalePriceAdjustInterval = ReadIntMax();
                            order.ScaleProfitOffset = ReadDoubleMax();
                            order.ScaleAutoReset = ReadBoolFromInt();
                            order.ScaleInitPosition = ReadIntMax();
                            order.ScaleInitFillQty = ReadIntMax();
                            order.ScaleRandomPercent = ReadBoolFromInt();
                        }

                        if (version >= 24)
                        {
                            order.HedgeType = ReadStr();
                            if (!string.IsNullOrEmpty(order.HedgeType))
                            {
                                order.HedgeParam = ReadStr();
                            }
                        }

                        if (version >= 25)
                        {
                            order.OptOutSmartRouting = ReadBoolFromInt();
                        }

                        if (version >= 19)
                        {
                            order.ClearingAccount = ReadStr();
                            order.ClearingIntent = ReadStr();
                        }

                        if (version >= 22)
                        {
                            order.NotHeld = ReadBoolFromInt();
                        }

                        if (version >= 20)
                        {
                            if (ReadBoolFromInt())
                            {
                                var underComp = new UnderlyingComponent();
                                underComp.ContractId = ReadInt();
                                underComp.Delta = ReadDouble();
                                underComp.Price = ReadDecimal();
                                contract.UnderlyingComponent = underComp;
                            }
                        }

                        if (version >= 21)
                        {
                            order.AlgoStrategy = ReadStr();
                            if (!string.IsNullOrEmpty(order.AlgoStrategy))
                            {
                                var algoParamsCount = ReadInt();
                                if (algoParamsCount > 0)
                                {
                                    order.AlgoParams = new Collection<TagValue>();
                                    for (var i = 0; i < algoParamsCount; i++)
                                    {
                                        var tagValue = new TagValue();
                                        tagValue.Tag = ReadStr();
                                        tagValue.Value = ReadStr();
                                        order.AlgoParams.Add(tagValue);
                                    }
                                }
                            }
                        }

                        var orderState = new OrderState();

                        if (version >= 16)
                        {
                            rstr = ReadStr();
                            order.WhatIf = !(string.IsNullOrEmpty(rstr) || rstr == "0");

                            var ost = ReadStr();
                            orderState.Status = string.IsNullOrEmpty(ost)
                                                    ? IBNet.OrderStatus.None
                                                    : (OrderStatus)
                                                      EnumDescConverter.GetEnumValue(typeof(OrderStatus), ost);
                            orderState.InitMargin = ReadStr();
                            orderState.MaintMargin = ReadStr();
                            orderState.EquityWithLoan = ReadStr();
                            orderState.Commission = ReadDoubleMax();
                            orderState.MinCommission = ReadDoubleMax();
                            orderState.MaxCommission = ReadDoubleMax();
                            orderState.CommissionCurrency = ReadStr();
                            orderState.WarningText = ReadStr();
                        }

                        openOrder(order.OrderId, contract, order, orderState);
                        break;
                    }

                case IncomingMessage.NextValidId:
                    {
                        var version = ReadInt();
                        var orderId = ReadInt();
                        nextValidId(orderId);
                        break;
                    }

                case IncomingMessage.ScannerData:
                    {
                        var contract = new ContractDetails();
                        var version = ReadInt();
                        var tickerId = ReadInt();
                        var numberOfElements = ReadInt();
                        for (var ctr = 0; ctr < numberOfElements; ctr++)
                        {
                            var rank = ReadInt();
                            if (version >= 3)
                            {
                                contract.Summary.ContractId = ReadInt();
                            }

                            contract.Summary.Symbol = ReadStr();
                            contract.Summary.SecurityType =
                                (SecurityType)EnumDescConverter.GetEnumValue(typeof(SecurityType), ReadStr());
                            contract.Summary.Expiry = ReadStr();
                            contract.Summary.Strike = ReadDouble();
                            var rstr = ReadStr();
                            contract.Summary.Right = rstr == null || rstr.Length <= 0 || rstr.Equals("?")
                                                         ? RightType.Undefined
                                                         : (RightType)
                                                           EnumDescConverter.GetEnumValue(typeof(RightType), rstr);
                            contract.Summary.Exchange = ReadStr();
                            contract.Summary.Currency = ReadStr();
                            contract.Summary.LocalSymbol = ReadStr();
                            contract.MarketName = ReadStr();
                            contract.TradingClass = ReadStr();
                            var distance = ReadStr();
                            var benchmark = ReadStr();
                            var projection = ReadStr();
                            string legsStr = null;
                            if (version >= 2)
                            {
                                legsStr = ReadStr();
                            }

                            scannerData(tickerId, rank, contract, distance, benchmark, projection, legsStr);
                        }

                        scannerDataEnd(tickerId);
                        break;
                    }

                case IncomingMessage.ContractData:
                    {
                        var version = ReadInt();

                        var reqId = -1;
                        if (version >= 3)
                        {
                            reqId = ReadInt();
                        }

                        var contract = new ContractDetails();
                        contract.Summary.Symbol = ReadStr();
                        contract.Summary.SecurityType =
                            (SecurityType)EnumDescConverter.GetEnumValue(typeof(SecurityType), ReadStr());
                        contract.Summary.Expiry = ReadStr();
                        contract.Summary.Strike = ReadDouble();
                        var rstr = ReadStr();
                        contract.Summary.Right = rstr == null || rstr.Length <= 0 || rstr.Equals("?")
                                                     ? RightType.Undefined
                                                     : (RightType)
                                                       EnumDescConverter.GetEnumValue(typeof(RightType), rstr);
                        contract.Summary.Exchange = ReadStr();
                        contract.Summary.Currency = ReadStr();
                        contract.Summary.LocalSymbol = ReadStr();
                        contract.MarketName = ReadStr();
                        contract.TradingClass = ReadStr();
                        contract.Summary.ContractId = ReadInt();
                        contract.MinTick = ReadDouble();
                        contract.Summary.Multiplier = ReadStr();
                        contract.OrderTypes = ReadStr();
                        contract.ValidExchanges = ReadStr();
                        if (version >= 2)
                        {
                            contract.PriceMagnifier = ReadInt();
                        }

                        if (version >= 4)
                        {
                            contract.UnderConId = ReadInt();
                        }

                        if (version >= 5)
                        {
                            contract.LongName = ReadStr();
                            contract.Summary.PrimaryExchange = ReadStr();
                        }

                        if (version >= 6)
                        {
                            contract.ContractMonth = ReadStr();
                            contract.Industry = ReadStr();
                            contract.Category = ReadStr();
                            contract.Subcategory = ReadStr();
                            contract.TimeZoneId = ReadStr();
                            contract.TradingHours = ReadStr();
                            contract.LiquidHours = ReadStr();
                        }

                        contractDetails(reqId, contract);
                        break;
                    }

                case IncomingMessage.BondContractData:
                    {
                        var version = ReadInt();

                        var reqId = -1;
                        if (version >= 3)
                        {
                            reqId = ReadInt();
                        }

                        var contract = new ContractDetails();

                        contract.Summary.Symbol = ReadStr();
                        contract.Summary.SecurityType =
                            (SecurityType)EnumDescConverter.GetEnumValue(typeof(SecurityType), ReadStr());
                        contract.Cusip = ReadStr();
                        contract.Coupon = ReadDouble();
                        contract.Maturity = ReadStr();
                        contract.IssueDate = ReadStr();
                        contract.Ratings = ReadStr();
                        contract.BondType = ReadStr();
                        contract.CouponType = ReadStr();
                        contract.Convertible = ReadBoolFromInt();
                        contract.Callable = ReadBoolFromInt();
                        contract.Putable = ReadBoolFromInt();
                        contract.DescriptionAppend = ReadStr();
                        contract.Summary.Exchange = ReadStr();
                        contract.Summary.Currency = ReadStr();
                        contract.MarketName = ReadStr();
                        contract.TradingClass = ReadStr();
                        contract.Summary.ContractId = ReadInt();
                        contract.MinTick = ReadDouble();
                        contract.OrderTypes = ReadStr();
                        contract.ValidExchanges = ReadStr();
                        if (version >= 2)
                        {
                            contract.NextOptionDate = ReadStr();
                            contract.NextOptionType = ReadStr();
                            contract.NextOptionPartial = ReadBoolFromInt();
                            contract.Notes = ReadStr();
                        }

                        if (version >= 4)
                        {
                            contract.LongName = ReadStr();
                        }

                        bondContractDetails(reqId, contract);
                        break;
                    }

                case IncomingMessage.ExecutionData:
                    {
                        var version = ReadInt();

                        var reqId = -1;
                        if (version >= 7)
                        {
                            reqId = ReadInt();
                        }

                        var orderId = ReadInt();

                        // Handle the 2^31-1 == 0 bug
                        if (orderId == 2147483647)
                        {
                            orderId = 0;
                        }

                        // Read Contract Fields
                        var contract = new Contract();
                        if (version >= 5)
                        {
                            contract.ContractId = ReadInt();
                        }

                        contract.Symbol = ReadStr();
                        contract.SecurityType =
                            (SecurityType)EnumDescConverter.GetEnumValue(typeof(SecurityType), ReadStr());
                        contract.Expiry = ReadStr();
                        contract.Strike = ReadDouble();
                        var rstr = ReadStr();
                        contract.Right = string.IsNullOrEmpty(rstr) || rstr.Equals("?")
                                             ? RightType.Undefined
                                             : (RightType)EnumDescConverter.GetEnumValue(typeof(RightType), rstr);
                        if (version >= 9)
                        {
                            contract.Multiplier = ReadStr();
                        }

                        contract.Exchange = ReadStr();
                        contract.Currency = ReadStr();
                        contract.LocalSymbol = ReadStr();

                        var exec = new Execution();
                        exec.OrderId = orderId;
                        exec.ExecutionId = ReadStr();
                        exec.Time = ReadStr();
                        exec.AccountNumber = ReadStr();
                        exec.Exchange = ReadStr();
                        exec.Side = (ExecutionSide)EnumDescConverter.GetEnumValue(typeof(ExecutionSide), ReadStr());
                        exec.Shares = ReadInt();
                        exec.Price = ReadDouble();
                        if (version >= 2)
                        {
                            exec.PermId = ReadInt();
                        }

                        if (version >= 3)
                        {
                            exec.ClientId = ReadInt();
                        }

                        if (version >= 4)
                        {
                            exec.Liquidation = ReadInt();
                        }

                        if (version >= 6)
                        {
                            exec.CumQuantity = ReadInt();
                            exec.AvgPrice = ReadDecimal();
                        }

                        if (version >= 8)
                        {
                            exec.OrderRef = ReadStr();
                        }

                        if (version >= 9)
                        {
                            exec.EvRule = ReadStr();
                            exec.EvMultipler = ReadDouble();
                        }

                        execDetails(reqId, orderId, contract, exec);
                        break;
                    }

                case IncomingMessage.MarketDepth:
                    {
                        var version = ReadInt();
                        var id = ReadInt();

                        var position = ReadInt();
                        var operation = (MarketDepthOperation)ReadInt();
                        var side = (MarketDepthSide)ReadInt();
                        var price = ReadDecimal();
                        var size = ReadInt();

                        updateMktDepth(id, position, operation, side, price, size);
                        break;
                    }

                case IncomingMessage.MarketDepthL2:
                    {
                        var version = ReadInt();
                        var id = ReadInt();

                        var position = ReadInt();
                        var marketMaker = ReadStr();
                        var operation = (MarketDepthOperation)ReadInt();
                        var side = (MarketDepthSide)ReadInt();
                        var price = ReadDecimal();
                        var size = ReadInt();

                        updateMktDepthL2(id, position, marketMaker, operation, side, price, size);
                        break;
                    }

                case IncomingMessage.NewsBulletins:
                    {
                        var version = ReadInt();
                        var newsMsgId = ReadInt();
                        var newsMsgType = (NewsType)ReadInt();
                        var newsMessage = ReadStr();
                        var originatingExch = ReadStr();

                        updateNewsBulletin(newsMsgId, newsMsgType, newsMessage, originatingExch);
                        break;
                    }

                case IncomingMessage.ManagedAccounts:
                    {
                        var version = ReadInt();
                        var accountsList = ReadStr();

                        managedAccounts(accountsList);
                        break;
                    }

                case IncomingMessage.ReceiveFA:
                    {
                        var version = ReadInt();
                        var faDataType = (FADataType)ReadInt();
                        var xml = ReadStr();

                        receiveFA(faDataType, xml);
                        break;
                    }

                case IncomingMessage.HistoricalData:
                    {
                        var version = ReadInt();
                        var reqId = ReadInt();
                        if (version >= 2)
                        {
                            // Read Start Date String
                            /*String startDateStr = */
                            ReadStr();

                            /*String endDateStr   = */
                            ReadStr();

                            // completedIndicator += ("-" + startDateStr + "-" + endDateStr);
                        }

                        var itemCount = ReadInt();
                        for (var ctr = 0; ctr < itemCount; ctr++)
                        {
                            // Comes in as seconds
                            // 2 - dates are returned as a long integer specifying the number of seconds since 1/1/1970 GMT.
                            var date = ReadStr();
                            var longDate = long.Parse(date, CultureInfo.InvariantCulture);

                            // Check if date time string or seconds
                            DateTime timeStamp;
                            if (longDate < 30000000)
                            {
                                timeStamp =
                                    new DateTime(
                                        int.Parse(date.Substring(0, 4)), 
                                        int.Parse(date.Substring(4, 2)), 
                                        int.Parse(date.Substring(6, 2)), 
                                        0, 
                                        0, 
                                        0, 
                                        DateTimeKind.Utc).ToLocalTime();
                            }
                            else
                            {
                                timeStamp =
                                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(longDate)
                                        .ToLocalTime();
                            }

                            var open = ReadDecimal();
                            var high = ReadDecimal();
                            var low = ReadDecimal();
                            var close = ReadDecimal();
                            var volume = ReadInt();
                            var WAP = ReadDouble();
                            var hasGaps = ReadStr();
                            var barCount = -1;
                            if (version >= 3)
                            {
                                barCount = ReadInt();
                            }

                            historicalData(
                                reqId, 
                                timeStamp, 
                                open, 
                                high, 
                                low, 
                                close, 
                                volume, 
                                barCount, 
                                WAP, 
                                bool.Parse(hasGaps), 
                                ctr, 
                                itemCount);
                        }

                        break;
                    }

                case IncomingMessage.ScannerParameters:
                    {
                        var version = ReadInt();
                        var xml = ReadStr();
                        scannerParameters(xml);
                        break;
                    }

                case IncomingMessage.CurrentTime:
                    {
                        /*int version =*/
                        ReadInt();
                        var time = ReadLong();
                        var cTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(time);
                        currentTime(cTime);
                        break;
                    }

                case IncomingMessage.RealTimeBars:
                    {
                        /*int version =*/
                        ReadInt();
                        var reqId = ReadInt();
                        var time = ReadLong();
                        var open = ReadDecimal();
                        var high = ReadDecimal();
                        var low = ReadDecimal();
                        var close = ReadDecimal();
                        var volume = ReadLong();
                        var wap = ReadDouble();
                        var count = ReadInt();
                        realTimeBar(reqId, time, open, high, low, close, volume, wap, count);
                        break;
                    }

                case IncomingMessage.FundamentalData:
                    {
                        /*int version =*/
                        ReadInt();
                        var reqId = ReadInt();
                        var data = ReadStr();
                        fundamentalData(reqId, data);
                        break;
                    }

                case IncomingMessage.ContractDataEnd:
                    {
                        /*int version =*/
                        ReadInt();
                        var reqId = ReadInt();
                        contractDetailsEnd(reqId);
                        break;
                    }

                case IncomingMessage.OpenOrderEnd:
                    {
                        /*int version =*/
                        ReadInt();
                        openOrderEnd();
                        break;
                    }

                case IncomingMessage.AccountDownloadEnd:
                    {
                        /*int version =*/
                        ReadInt();
                        var accountName = ReadStr();
                        accountDownloadEnd(accountName);
                        break;
                    }

                case IncomingMessage.ExecutionDataEnd:
                    {
                        /*int version =*/
                        ReadInt();
                        var reqId = ReadInt();
                        executionDataEnd(reqId);
                        break;
                    }

                case IncomingMessage.DeltaNuetralValidation:
                    {
                        /*int version =*/
                        ReadInt();
                        var reqId = ReadInt();

                        var underComp = new UnderComp();
                        underComp.ConId = ReadInt();
                        underComp.Delta = ReadDouble();
                        underComp.Price = ReadDouble();

                        deltaNuetralValidation(reqId, underComp);
                        break;
                    }

                case IncomingMessage.TickSnapshotEnd:
                    {
                        /*int version =*/
                        ReadInt();
                        var reqId = ReadInt();

                        tickSnapshotEnd(reqId);
                        break;
                    }

                case IncomingMessage.MarketDataType:
                    {
                        /*int version =*/
                        ReadInt();
                        var reqId = ReadInt();
                        var mdt = (MarketDataType)ReadInt();

                        marketDataType(reqId, mdt);
                        break;
                    }

                case IncomingMessage.CommissionReport:
                    {
                        /*int version =*/
                        ReadInt();

                        var report = new CommissionReport();
                        report.ExecId = ReadStr();
                        report.Commission = ReadDouble();
                        report.Currency = ReadStr();
                        report.RealizedPnL = ReadNullableDouble();
                        report.Yield = ReadNullableDouble();
                        report.YieldRedemptionDate = ReadNullableDateInt();

                        commissionReport(report);
                        break;
                    }

                default:
                    {
                        error(ErrorMessage.NoValidId);
                        return false;
                    }
            }

            return true;
        }

        /// <summary>
        ///     The read bool from int.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        private bool ReadBoolFromInt()
        {
            var str = ReadStr();
            return str != null && (int.Parse(str, CultureInfo.InvariantCulture) != 0);
        }

        /// <summary>
        ///     The read decimal.
        /// </summary>
        /// <returns>
        ///     The <see cref="decimal" />.
        /// </returns>
        private decimal ReadDecimal()
        {
            var str = ReadStr();
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            decimal retVal;
            return decimal.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out retVal)
                       ? retVal
                       : decimal.MaxValue;
        }

        /// <summary>
        ///     The read decimal max.
        /// </summary>
        /// <returns>
        ///     The <see cref="decimal" />.
        /// </returns>
        private decimal ReadDecimalMax()
        {
            var str = ReadStr();
            decimal retVal;
            return (!string.IsNullOrEmpty(str)
                    && decimal.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out retVal))
                       ? retVal
                       : decimal.MaxValue;
        }

        /// <summary>
        ///     The read double.
        /// </summary>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        private double ReadDouble()
        {
            var str = ReadStr();
            return str == null ? 0 : double.Parse(str, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     The read double max.
        /// </summary>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        private double ReadDoubleMax()
        {
            var str = ReadStr();
            return string.IsNullOrEmpty(str) ? double.MaxValue : double.Parse(str, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     The read int.
        /// </summary>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        private int ReadInt()
        {
            var str = ReadStr();

            return str == null ? 0 : int.Parse(str, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     The read int max.
        /// </summary>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        private int ReadIntMax()
        {
            var str = ReadStr();
            return string.IsNullOrEmpty(str) ? int.MaxValue : int.Parse(str, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     The read long.
        /// </summary>
        /// <returns>
        ///     The <see cref="long" />.
        /// </returns>
        private long ReadLong()
        {
            var str = ReadStr();
            return str == null ? 0L : long.Parse(str, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     The read nullable date int.
        /// </summary>
        /// <returns>
        ///     The <see cref="DateTime?" />.
        /// </returns>
        private DateTime? ReadNullableDateInt()
        {
            var dateInt = ReadInt();
            DateTime? date = null;
            if (dateInt != 0)
            {
                date = DateTime.ParseExact(dateInt.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
            }

            return date;
        }

        /// <summary>
        ///     The read nullable double.
        /// </summary>
        /// <returns>
        ///     The <see cref="double?" />.
        /// </returns>
        private double? ReadNullableDouble()
        {
            var str = ReadStr();
            if (str == null || str == "1.7976931348623157E308")
            {
                return null;
            }

            return double.Parse(str, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     The read str.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        private string ReadStr()
        {
            var buf = new StringBuilder();
            while (true)
            {
                var c = (sbyte)dis.ReadByte();
                if (c == 0)
                {
                    break;
                }

                buf.Append((char)c);
            }

            var str = buf.ToString();
            return str.Length == 0 ? null : str;
        }

        /// <summary>
        ///     Called by the worker thread to indicate when it has stopped.
        /// </summary>
        private void SetStopped()
        {
            lock (stopLock)
            {
                stopped = true;
            }
        }

        /// <summary>
        /// The account download end.
        /// </summary>
        /// <param name="accountName">
        /// The account name.
        /// </param>
        private void accountDownloadEnd(string accountName)
        {
            var e = new AccountDownloadEndEventArgs(accountName);
            OnAccountDownloadEnd(e);
        }

        /// <summary>
        /// The bond contract details.
        /// </summary>
        /// <param name="requestId">
        /// The request id.
        /// </param>
        /// <param name="contractDetails">
        /// The contract details.
        /// </param>
        private void bondContractDetails(int requestId, ContractDetails contractDetails)
        {
            var e = new BondContractDetailsEventArgs(requestId, contractDetails);
            OnBondContractDetails(e);
        }

        /// <summary>
        /// The check connected.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string checkConnected(string host)
        {
            if (connected)
            {
                error(ErrorMessage.ConnectFail, ErrorMessage.AlreadyConnected);
                return null;
            }

            if (host == null || host.Length < 1)
            {
                host = "127.0.0.1";
            }

            return host;
        }

        /// <summary>
        ///     The close.
        /// </summary>
        private void close()
        {
            Disconnect();
            connectionClosed();
        }

        /// <summary>
        /// The commission report.
        /// </summary>
        /// <param name="report">
        /// The report.
        /// </param>
        private void commissionReport(CommissionReport report)
        {
            var e = new CommissionReportEventArgs(report);
            OnCommissionReport(e);
        }

        /// <summary>
        /// The connect.
        /// </summary>
        /// <param name="socket">
        /// The socket.
        /// </param>
        /// <param name="clientId">
        /// The client id.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        private void connect(TcpClient socket, int clientId)
        {
            if (socket == null)
            {
                throw new ArgumentNullException("socket");
            }

            lock (this)
            {
                ibSocket = socket;

                // create io streams
                dis = new BinaryReader(ibSocket.GetStream());
                dos = new BinaryWriter(ibSocket.GetStream());

                // set client version
                send(clientVersion);

                // start Reader thread

                // check server version
                serverVersion = ReadInt();
                GeneralTracer.WriteLineIf(ibTrace.TraceInfo, "IBMethod: Connect: Server Version: {0}", serverVersion);
                if (serverVersion >= 20)
                {
                    twsTime = ReadStr();
                    GeneralTracer.WriteLineIf(
                        ibTrace.TraceInfo, 
                        "IBMethod: Connect: TWS Time at connection: {0}", 
                        twsTime);

                    // Let's fire the servertime event
                }

                if (serverVersion < minimumServerVersion)
                {
                    error(
                        ErrorMessage.UpdateTws, 
                        "Server version " + serverVersion + " is lower than required version " + minimumServerVersion
                        + ".");
                    return;
                }

                // Send the client id
                if (serverVersion >= 3)
                {
                    send(clientId);
                }

                Start();

                // set connected flag
                connected = true;
            }
        }

        /// <summary>
        ///     The connection closed.
        /// </summary>
        private void connectionClosed()
        {
            var e = new ConnectionClosedEventArgs();
            OnConnectionClosed(e);
        }

        /// <summary>
        /// The contract details.
        /// </summary>
        /// <param name="requestId">
        /// The request id.
        /// </param>
        /// <param name="contractDetails">
        /// The contract details.
        /// </param>
        private void contractDetails(int requestId, ContractDetails contractDetails)
        {
            var e = new ContractDetailsEventArgs(requestId, contractDetails);
            OnContractDetails(e);
        }

        /// <summary>
        /// The contract details end.
        /// </summary>
        /// <param name="requestId">
        /// The request id.
        /// </param>
        private void contractDetailsEnd(int requestId)
        {
            var e = new ContractDetailsEndEventArgs(requestId);
            OnContractDetailsEnd(e);
        }

        /// <summary>
        /// The current time.
        /// </summary>
        /// <param name="time">
        /// The time.
        /// </param>
        private void currentTime(DateTime time)
        {
            var e = new CurrentTimeEventArgs(time);
            OnCurrentTime(e);
        }

        /// <summary>
        /// The delta nuetral validation.
        /// </summary>
        /// <param name="requestId">
        /// The request id.
        /// </param>
        /// <param name="underComp">
        /// The under comp.
        /// </param>
        private void deltaNuetralValidation(int requestId, UnderComp underComp)
        {
            var e = new DeltaNuetralValidationEventArgs(requestId, underComp);
            OnDeltaNuetralValidation(e);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="errorMsg">
        /// The error msg.
        /// </param>
        private void error(int tickerId, ErrorMessage errorCode, string errorMsg)
        {
            lock (this)
            {
                GeneralTracer.WriteLineIf(
                    ibTrace.TraceError, 
                    "IBEvent: Error: tickerId: {0}, errorCode: {1}, errorMsg: {2}", 
                    tickerId, 
                    errorCode, 
                    errorMsg);
                var e = new ErrorEventArgs(tickerId, errorCode, errorMsg);
                OnError(e);
            }
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="errorString">
        /// The error string.
        /// </param>
        private void error(ErrorMessage errorCode, ErrorMessage errorString)
        {
            error(errorCode, errorString.ToString());
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void error(ErrorMessage errorCode, Exception e)
        {
            error(errorCode, e.ToString());
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void error(int tickerId, ErrorMessage errorCode, Exception e)
        {
            error(tickerId, errorCode, e.ToString());
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        private void error(ErrorMessage errorCode)
        {
            error(errorCode, string.Empty);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="tail">
        /// The tail.
        /// </param>
        private void error(string tail)
        {
            error(ErrorMessage.NoValidId, tail);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        private void error(int tickerId, ErrorMessage errorCode)
        {
            error(tickerId, errorCode, string.Empty);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="tail">
        /// The tail.
        /// </param>
        private void error(ErrorMessage errorCode, string tail)
        {
            error((int)ErrorMessage.NoValidId, errorCode, tail);
        }

        /// <summary>
        /// The exception.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        private void exception(Exception ex)
        {
            var e = new ReportExceptionEventArgs(ex);
            OnReportException(e);
        }

        /// <summary>
        /// The exec details.
        /// </summary>
        /// <param name="reqId">
        /// The req id.
        /// </param>
        /// <param name="orderId">
        /// The order id.
        /// </param>
        /// <param name="contract">
        /// The contract.
        /// </param>
        /// <param name="execution">
        /// The execution.
        /// </param>
        private void execDetails(int reqId, int orderId, Contract contract, Execution execution)
        {
            var e = new ExecDetailsEventArgs(reqId, orderId, contract, execution);
            OnExecDetails(e);
        }

        /// <summary>
        /// The execution data end.
        /// </summary>
        /// <param name="requestId">
        /// The request id.
        /// </param>
        private void executionDataEnd(int requestId)
        {
            var e = new ExecutionDataEndEventArgs(requestId);
            OnExecutionDataEnd(e);
        }

        /// <summary>
        /// The fundamental data.
        /// </summary>
        /// <param name="requestId">
        /// The request id.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        private void fundamentalData(int requestId, string data)
        {
            var e = new FundamentalDetailsEventArgs(requestId, data);
            OnFundamentalData(e);
        }

        /// <summary>
        /// The historical data.
        /// </summary>
        /// <param name="reqId">
        /// The req id.
        /// </param>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <param name="open">
        /// The open.
        /// </param>
        /// <param name="high">
        /// The high.
        /// </param>
        /// <param name="low">
        /// The low.
        /// </param>
        /// <param name="close">
        /// The close.
        /// </param>
        /// <param name="volume">
        /// The volume.
        /// </param>
        /// <param name="trades">
        /// The trades.
        /// </param>
        /// <param name="WAP">
        /// The wap.
        /// </param>
        /// <param name="hasGaps">
        /// The has gaps.
        /// </param>
        /// <param name="recordNumber">
        /// The record number.
        /// </param>
        /// <param name="recordTotal">
        /// The record total.
        /// </param>
        private void historicalData(
            int reqId, 
            DateTime date, 
            decimal open, 
            decimal high, 
            decimal low, 
            decimal close, 
            int volume, 
            int trades, 
            double WAP, 
            bool hasGaps, 
            int recordNumber, 
            int recordTotal)
        {
            var e = new HistoricalDataEventArgs(
                reqId, 
                date, 
                open, 
                high, 
                low, 
                close, 
                volume, 
                trades, 
                WAP, 
                hasGaps, 
                recordNumber, 
                recordTotal);
            OnHistoricalData(e);
        }

        /// <summary>
        /// The managed accounts.
        /// </summary>
        /// <param name="accountsList">
        /// The accounts list.
        /// </param>
        private void managedAccounts(string accountsList)
        {
            var e = new ManagedAccountsEventArgs(accountsList);
            OnManagedAccounts(e);
        }

        /// <summary>
        /// The market data type.
        /// </summary>
        /// <param name="requestId">
        /// The request id.
        /// </param>
        /// <param name="dataType">
        /// The data type.
        /// </param>
        private void marketDataType(int requestId, MarketDataType dataType)
        {
            var e = new MarketDataTypeEventArgs(requestId, dataType);
            OnMarketDataType(e);
        }

        /// <summary>
        /// The next valid id.
        /// </summary>
        /// <param name="orderId">
        /// The order id.
        /// </param>
        private void nextValidId(int orderId)
        {
            // GeneralTracer.WriteLineIf(ibTickTrace.TraceInfo, "IBEvent: NextValidId: orderId: {0}", orderId);
            var e = new NextValidIdEventArgs(orderId);
            OnNextValidId(e);
        }

        /// <summary>
        /// The open order.
        /// </summary>
        /// <param name="orderId">
        /// The order id.
        /// </param>
        /// <param name="contract">
        /// The contract.
        /// </param>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <param name="orderState">
        /// The order state.
        /// </param>
        private void openOrder(int orderId, Contract contract, Order order, OrderState orderState)
        {
            var e = new OpenOrderEventArgs(orderId, contract, order, orderState);
            OnOpenOrder(e);
        }

        /// <summary>
        ///     The open order end.
        /// </summary>
        private void openOrderEnd()
        {
            var e = new EventArgs();
            OnOpenOrderEnd(e);
        }

        /// <summary>
        /// The order status.
        /// </summary>
        /// <param name="orderId">
        /// The order id.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="filled">
        /// The filled.
        /// </param>
        /// <param name="remaining">
        /// The remaining.
        /// </param>
        /// <param name="avgFillPrice">
        /// The avg fill price.
        /// </param>
        /// <param name="permId">
        /// The perm id.
        /// </param>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <param name="lastFillPrice">
        /// The last fill price.
        /// </param>
        /// <param name="clientId">
        /// The client id.
        /// </param>
        /// <param name="whyHeld">
        /// The why held.
        /// </param>
        private void orderStatus(
            int orderId, 
            OrderStatus status, 
            int filled, 
            int remaining, 
            decimal avgFillPrice, 
            int permId, 
            int parentId, 
            decimal lastFillPrice, 
            int clientId, 
            string whyHeld)
        {
            var e = new OrderStatusEventArgs(
                orderId, 
                status, 
                filled, 
                remaining, 
                avgFillPrice, 
                permId, 
                parentId, 
                lastFillPrice, 
                clientId, 
                whyHeld);
            OnOrderStatus(e);
        }

        /// <summary>
        /// The real time bar.
        /// </summary>
        /// <param name="reqId">
        /// The req id.
        /// </param>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <param name="open">
        /// The open.
        /// </param>
        /// <param name="high">
        /// The high.
        /// </param>
        /// <param name="low">
        /// The low.
        /// </param>
        /// <param name="close">
        /// The close.
        /// </param>
        /// <param name="volume">
        /// The volume.
        /// </param>
        /// <param name="wap">
        /// The wap.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        private void realTimeBar(
            int reqId, 
            long time, 
            decimal open, 
            decimal high, 
            decimal low, 
            decimal close, 
            long volume, 
            double wap, 
            int count)
        {
            var e = new RealTimeBarEventArgs(reqId, time, open, high, low, close, volume, wap, count);
            OnRealTimeBar(e);
        }

        /// <summary>
        /// The receive fa.
        /// </summary>
        /// <param name="faDataType">
        /// The fa data type.
        /// </param>
        /// <param name="xml">
        /// The xml.
        /// </param>
        private void receiveFA(FADataType faDataType, string xml)
        {
            var e = new ReceiveFAEventArgs(faDataType, xml);
            OnReceiveFA(e);
        }

        /// <summary>
        /// The scanner data.
        /// </summary>
        /// <param name="reqId">
        /// The req id.
        /// </param>
        /// <param name="rank">
        /// The rank.
        /// </param>
        /// <param name="contractDetails">
        /// The contract details.
        /// </param>
        /// <param name="distance">
        /// The distance.
        /// </param>
        /// <param name="benchmark">
        /// The benchmark.
        /// </param>
        /// <param name="projection">
        /// The projection.
        /// </param>
        /// <param name="legsStr">
        /// The legs str.
        /// </param>
        private void scannerData(
            int reqId, 
            int rank, 
            ContractDetails contractDetails, 
            string distance, 
            string benchmark, 
            string projection, 
            string legsStr)
        {
            var e = new ScannerDataEventArgs(reqId, rank, contractDetails, distance, benchmark, projection, legsStr);
            OnScannerData(e);
        }

        /// <summary>
        /// The scanner data end.
        /// </summary>
        /// <param name="reqId">
        /// The req id.
        /// </param>
        private void scannerDataEnd(int reqId)
        {
            var e = new ScannerDataEndEventArgs(reqId);
            OnScannerDataEnd(e);
        }

        /// <summary>
        /// The scanner parameters.
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        private void scannerParameters(string xml)
        {
            var e = new ScannerParametersEventArgs(xml);
            OnScannerParameters(e);
        }

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        private void send(string str)
        {
            // write string to data buffer; writer thread will
            // write it to ibSocket
            if (!string.IsNullOrEmpty(str))
            {
                dos.Write(ToByteArray(str));
            }

            sendEOL();
        }

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        private void send(int val)
        {
            send(Convert.ToString(val, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        private void send(double val)
        {
            send(Convert.ToString(val, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        private void send(decimal val)
        {
            send(Convert.ToString(val, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        private void send(bool val)
        {
            send(val ? 1 : 0);
        }

        /// <summary>
        /// The send.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        private void send(bool? val)
        {
            if (val != null)
            {
                send(val.Value);
            }
            else
            {
                send(string.Empty);
            }
        }

        /// <summary>
        ///     The send eol.
        /// </summary>
        private void sendEOL()
        {
            dos.Write(EOL);
        }

        /// <summary>
        /// The send max.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        private void sendMax(double val)
        {
            if (val == double.MaxValue)
            {
                sendEOL();
            }
            else
            {
                send(Convert.ToString(val, CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// The send max.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        private void sendMax(int val)
        {
            if (val == int.MaxValue)
            {
                sendEOL();
            }
            else
            {
                send(Convert.ToString(val, CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// The send max.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        private void sendMax(decimal val)
        {
            if (val == decimal.MaxValue)
            {
                sendEOL();
            }
            else
            {
                send(Convert.ToString(val, CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// The tick efp.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="tickType">
        /// The tick type.
        /// </param>
        /// <param name="basisPoints">
        /// The basis points.
        /// </param>
        /// <param name="formattedBasisPoints">
        /// The formatted basis points.
        /// </param>
        /// <param name="impliedFuture">
        /// The implied future.
        /// </param>
        /// <param name="holdDays">
        /// The hold days.
        /// </param>
        /// <param name="futureExpiry">
        /// The future expiry.
        /// </param>
        /// <param name="dividendImpact">
        /// The dividend impact.
        /// </param>
        /// <param name="dividendsToExpiry">
        /// The dividends to expiry.
        /// </param>
        private void tickEfp(
            int tickerId, 
            TickType tickType, 
            double basisPoints, 
            string formattedBasisPoints, 
            double impliedFuture, 
            int holdDays, 
            string futureExpiry, 
            double dividendImpact, 
            double dividendsToExpiry)
        {
            var e = new TickEfpEventArgs(
                tickerId, 
                tickType, 
                basisPoints, 
                formattedBasisPoints, 
                impliedFuture, 
                holdDays, 
                futureExpiry, 
                dividendImpact, 
                dividendsToExpiry);
            OnTickEfp(e);
        }

        /// <summary>
        /// The tick generic.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="tickType">
        /// The tick type.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        private void tickGeneric(int tickerId, TickType tickType, double value)
        {
            var e = new TickGenericEventArgs(tickerId, tickType, value);
            OnTickGeneric(e);
        }

        /// <summary>
        /// The tick option computation.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="tickType">
        /// The tick type.
        /// </param>
        /// <param name="impliedVol">
        /// The implied vol.
        /// </param>
        /// <param name="delta">
        /// The delta.
        /// </param>
        /// <param name="optPrice">
        /// The opt price.
        /// </param>
        /// <param name="pvDividend">
        /// The pv dividend.
        /// </param>
        /// <param name="gamma">
        /// The gamma.
        /// </param>
        /// <param name="vega">
        /// The vega.
        /// </param>
        /// <param name="theta">
        /// The theta.
        /// </param>
        /// <param name="undPrice">
        /// The und price.
        /// </param>
        private void tickOptionComputation(
            int tickerId, 
            TickType tickType, 
            double impliedVol, 
            double delta, 
            double optPrice, 
            double pvDividend, 
            double gamma, 
            double vega, 
            double theta, 
            double undPrice)
        {
            var e = new TickOptionComputationEventArgs(
                tickerId, 
                tickType, 
                impliedVol, 
                delta, 
                optPrice, 
                pvDividend, 
                gamma, 
                vega, 
                theta, 
                undPrice);
            OnTickOptionComputation(e);
        }

        /// <summary>
        /// The tick price.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="tickType">
        /// The tick type.
        /// </param>
        /// <param name="price">
        /// The price.
        /// </param>
        /// <param name="canAutoExecute">
        /// The can auto execute.
        /// </param>
        private void tickPrice(int tickerId, TickType tickType, decimal price, bool canAutoExecute)
        {
            // GeneralTracer.WriteLineIf(ibTickTrace.TraceInfo, "IBEvent: TickPrice: tickerId: {0}, tickType: {1}, price: {2}, canAutoExecute: {3}", tickerId, tickType, price, canAutoExecute);
            var e = new TickPriceEventArgs(tickerId, tickType, price, canAutoExecute);
            OnTickPrice(e);
        }

        /// <summary>
        /// The tick size.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="tickType">
        /// The tick type.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        private void tickSize(int tickerId, TickType tickType, int size)
        {
            // GeneralTracer.WriteLineIf(ibTickTrace.TraceInfo, "IBEvent: TickSize: tickerId: {0}, tickType: {1}, size: {2}", tickerId, tickType, size);
            var e = new TickSizeEventArgs(tickerId, tickType, size);
            OnTickSize(e);
        }

        /// <summary>
        /// The tick snapshot end.
        /// </summary>
        /// <param name="requestId">
        /// The request id.
        /// </param>
        private void tickSnapshotEnd(int requestId)
        {
            var e = new TickSnapshotEndEventArgs(requestId);
            OnTickSnapshotEnd(e);
        }

        /// <summary>
        /// The tick string.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="tickType">
        /// The tick type.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        private void tickString(int tickerId, TickType tickType, string value)
        {
            var e = new TickStringEventArgs(tickerId, tickType, value);
            OnTickString(e);
        }

        /// <summary>
        /// The update account time.
        /// </summary>
        /// <param name="timeStamp">
        /// The time stamp.
        /// </param>
        private void updateAccountTime(string timeStamp)
        {
            var e = new UpdateAccountTimeEventArgs(timeStamp);
            OnUpdateAccountTime(e);
        }

        /// <summary>
        /// The update account value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <param name="accountName">
        /// The account name.
        /// </param>
        private void updateAccountValue(string key, string value, string currency, string accountName)
        {
            var e = new UpdateAccountValueEventArgs(key, value, currency, accountName);
            OnUpdateAccountValue(e);
        }

        /// <summary>
        /// The update mkt depth.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <param name="side">
        /// The side.
        /// </param>
        /// <param name="price">
        /// The price.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        private void updateMktDepth(
            int tickerId, 
            int position, 
            MarketDepthOperation operation, 
            MarketDepthSide side, 
            decimal price, 
            int size)
        {
            var e = new UpdateMarketDepthEventArgs(tickerId, position, operation, side, price, size);
            OnUpdateMarketDepth(e);
        }

        /// <summary>
        /// The update mkt depth l 2.
        /// </summary>
        /// <param name="tickerId">
        /// The ticker id.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <param name="marketMaker">
        /// The market maker.
        /// </param>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <param name="side">
        /// The side.
        /// </param>
        /// <param name="price">
        /// The price.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        private void updateMktDepthL2(
            int tickerId, 
            int position, 
            string marketMaker, 
            MarketDepthOperation operation, 
            MarketDepthSide side, 
            decimal price, 
            int size)
        {
            var e = new UpdateMarketDepthL2EventArgs(tickerId, position, marketMaker, operation, side, price, size);
            OnUpdateMarketDepthL2(e);
        }

        /// <summary>
        /// The update news bulletin.
        /// </summary>
        /// <param name="msgId">
        /// The msg id.
        /// </param>
        /// <param name="msgType">
        /// The msg type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="origExchange">
        /// The orig exchange.
        /// </param>
        private void updateNewsBulletin(int msgId, NewsType msgType, string message, string origExchange)
        {
            var e = new UpdateNewsBulletinEventArgs(msgId, msgType, message, origExchange);
            OnUpdateNewsBulletin(e);
        }

        /// <summary>
        /// The update portfolio.
        /// </summary>
        /// <param name="contract">
        /// The contract.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <param name="marketPrice">
        /// The market price.
        /// </param>
        /// <param name="marketValue">
        /// The market value.
        /// </param>
        /// <param name="averageCost">
        /// The average cost.
        /// </param>
        /// <param name="unrealizedPNL">
        /// The unrealized pnl.
        /// </param>
        /// <param name="realizedPNL">
        /// The realized pnl.
        /// </param>
        /// <param name="accountName">
        /// The account name.
        /// </param>
        private void updatePortfolio(
            Contract contract, 
            int position, 
            decimal marketPrice, 
            decimal marketValue, 
            decimal averageCost, 
            decimal unrealizedPNL, 
            decimal realizedPNL, 
            string accountName)
        {
            var e = new UpdatePortfolioEventArgs(
                contract, 
                position, 
                marketPrice, 
                marketValue, 
                averageCost, 
                unrealizedPNL, 
                realizedPNL, 
                accountName);
            OnUpdatePortfolio(e);
        }

        #endregion
    }
}