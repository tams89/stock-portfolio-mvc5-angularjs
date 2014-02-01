// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Krs.Ats.IBNet;
using Krs.Ats.IBNet.Contracts;
using System;
using System.Threading;

namespace Krs.Ats.TestApp
{
    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The next order id.
        /// </summary>
        private static int NextOrderId = 0;

        /// <summary>
        /// The tf.
        /// </summary>
        private static Contract TF;

        /// <summary>
        /// The tick nasdaq.
        /// </summary>
        private static Contract TickNasdaq;

        /// <summary>
        /// The vol nasdaq.
        /// </summary>
        private static Contract VolNasdaq;

        /// <summary>
        /// The ad nasdaq.
        /// </summary>
        private static Contract AdNasdaq;

        /// <summary>
        /// The tick nyse.
        /// </summary>
        private static Contract TickNyse;

        /// <summary>
        /// The vol nyse.
        /// </summary>
        private static Contract VolNyse;

        /// <summary>
        /// The ad nyse.
        /// </summary>
        private static Contract AdNyse;

        /// <summary>
        /// The ym ecbot.
        /// </summary>
        private static Contract YmEcbot;

        /// <summary>
        /// The es.
        /// </summary>
        private static Contract ES;

        /// <summary>
        /// The spy.
        /// </summary>
        private static Contract SPY;

        /// <summary>
        /// The zn.
        /// </summary>
        private static Contract ZN;

        /// <summary>
        /// The zb.
        /// </summary>
        private static Contract ZB;

        /// <summary>
        /// The zt.
        /// </summary>
        private static Contract ZT;

        /// <summary>
        /// The zf.
        /// </summary>
        private static Contract ZF;

        /// <summary>
        /// The client.
        /// </summary>
        private static IBClient client;

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            client = new IBClient();
            client.ThrowExceptions = true;

            client.TickPrice += client_TickPrice;
            client.TickSize += client_TickSize;
            client.Error += client_Error;
            client.NextValidId += client_NextValidId;
            client.UpdateMarketDepth += client_UpdateMktDepth;
            client.RealTimeBar += client_RealTimeBar;
            client.OrderStatus += client_OrderStatus;
            client.ExecDetails += client_ExecDetails;

            Console.WriteLine("Connecting to IB.");
            client.Connect("127.0.0.1", 7496, 0);
            TF = new Contract("TF", "NYBOT", SecurityType.Future, "USD", "200909");
            YmEcbot = new Contract("YM", "ECBOT", SecurityType.Future, "USD", "200909");
            ES = new Contract("ES", "GLOBEX", SecurityType.Future, "USD", "200909");
            SPY = new Contract("SPY", "GLOBEX", SecurityType.Future, "USD", "200909");
            ZN = new Contract("ZN", "ECBOT", SecurityType.Future, "USD", "200909");
            ZB = new Contract("ZB", "ECBOT", SecurityType.Future, "USD", "200909");
            ZT = new Contract("ZT", "ECBOT", SecurityType.Future, "USD", "200909");
            ZF = new Contract("ZF", "ECBOT", SecurityType.Future, "USD", "200909");

            TickNasdaq = new Contract("TICK-NASD", "NASDAQ", SecurityType.Index, "USD");
            VolNasdaq = new Contract("VOL-NASD", "NASDAQ", SecurityType.Index, "USD");
            AdNasdaq = new Contract("AD-NASD", "NASDAQ", SecurityType.Index, "USD");

            TickNyse = new Contract("TICK-NYSE", "NYSE", SecurityType.Index, "USD");
            VolNyse = new Contract("VOL-NYSE", "NYSE", SecurityType.Index, "USD");
            AdNyse = new Contract("AD-NYSE", "NYSE", SecurityType.Index, "USD");

            // New Contract Creation Features
            var Google = new Equity("GOOG");

            // Forex Test
            var EUR = new Forex("EUR", "USD");

            client.RequestMarketData(14, Google, null, false, false);
            client.RequestMarketDepth(15, Google, 5);
            client.RequestRealTimeBars(16, Google, 5, RealTimeBarType.Trades, false);
            client.RequestMarketData(17, EUR, null, false, false);

            var BuyContract = new Order();
            BuyContract.Action = ActionSide.Buy;
            BuyContract.OutsideRth = false;
            BuyContract.LimitPrice = 560;
            BuyContract.OrderType = OrderType.Limit;
            BuyContract.TotalQuantity = 1;


            // client.PlaceOrder(503, TF, BuyContract);
            client.RequestExecutions(34, new ExecutionFilter());

            client.RequestAllOpenOrders();

            while (true)
            {
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// The client_ exec details.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void client_ExecDetails(object sender, ExecDetailsEventArgs e)
        {
            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                e.Contract.Symbol, e.Execution.AccountNumber, e.Execution.ClientId, e.Execution.Exchange, e.Execution.ExecutionId,
                e.Execution.Liquidation, e.Execution.OrderId, e.Execution.PermId, e.Execution.Price, e.Execution.Shares, e.Execution.Side, e.Execution.Time);
        }

        /// <summary>
        /// The client_ real time bar.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void client_RealTimeBar(object sender, RealTimeBarEventArgs e)
        {
            Console.WriteLine("Received Real Time Bar: " + e.Close);
        }

        /// <summary>
        /// The client_ order status.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void client_OrderStatus(object sender, OrderStatusEventArgs e)
        {
            Console.WriteLine("Order Placed.");
        }

        /// <summary>
        /// The client_ update mkt depth.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void client_UpdateMktDepth(object sender, UpdateMarketDepthEventArgs e)
        {
            Console.WriteLine("Tick ID: " + e.TickerId + " Tick Side: " + EnumDescConverter.GetEnumDescription(e.Side) +
                              " Tick Size: " + e.Size + " Tick Price: " + e.Price + " Tick Position: " + e.Position +
                              " Operation: " + EnumDescConverter.GetEnumDescription(e.Operation));
        }

        /// <summary>
        /// The client_ next valid id.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void client_NextValidId(object sender, NextValidIdEventArgs e)
        {
            Console.WriteLine("Next Valid Id: " + e.OrderId);
            NextOrderId = e.OrderId;
        }

        /// <summary>
        /// The client_ tick size.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void client_TickSize(object sender, TickSizeEventArgs e)
        {
            Console.WriteLine("Tick Size: " + e.Size + " Tick Type: " + EnumDescConverter.GetEnumDescription(e.TickType));
        }

        /// <summary>
        /// The client_ error.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void client_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Error: " + e.ErrorMsg);
        }

        /// <summary>
        /// The client_ tick price.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void client_TickPrice(object sender, TickPriceEventArgs e)
        {
            Console.WriteLine("Price: " + e.Price + " Tick Type: " + EnumDescConverter.GetEnumDescription(e.TickType));
        }
    }
}