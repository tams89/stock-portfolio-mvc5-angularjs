// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenOrderEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Open Order Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Open Order Event Arguments
    /// </summary>
    [Serializable()]
    public class OpenOrderEventArgs : EventArgs
    {
        /// <summary>
        /// The contract.
        /// </summary>
        private Contract contract;

        /// <summary>
        /// The order.
        /// </summary>
        private Order order;

        /// <summary>
        /// The order id.
        /// </summary>
        private int orderId;

        /// <summary>
        /// The order state.
        /// </summary>
        private OrderState orderState;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenOrderEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="orderId">
        /// The order Id assigned by TWS. Used to cancel or update the order.
        /// </param>
        /// <param name="contract">
        /// Describes the contract for the open order.
        /// </param>
        /// <param name="order">
        /// Gives the details of the open order.
        /// </param>
        /// <param name="orderState">
        /// The openOrder() callback with the new OrderState() object will now be invoked each time TWS receives commission information for a trade.
        /// </param>
        public OpenOrderEventArgs(int orderId, Contract contract, Order order, OrderState orderState)
        {
            this.orderId = orderId;
            this.order = order;
            this.contract = contract;
            this.orderState = orderState;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenOrderEventArgs"/> class. 
        /// Parameterless OpenOrderEventArgs Constructor
        /// </summary>
        public OpenOrderEventArgs()
        {
            orderId = -1;
            order = new Order();
            contract = new Contract();
            orderState = new OrderState();
        }


        /// <summary>
        /// The order Id assigned by TWS. Used to cancel or update the order.
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
			set { orderId = value; }
		}

        /// <summary>
        /// Describes the contract for the open order.
        /// </summary>
        public Contract Contract
        {
            get { return contract; }
			set { contract = value; }
		}

        /// <summary>
        /// Gives the details of the open order.
        /// </summary>
        public Order Order
        {
            get { return order; }
			set { order = value; }
		}

        /// <summary>
        /// The openOrder() callback with the new OrderState() object will
        /// now be invoked each time TWS receives commission information for a trade.
        /// </summary>
        public OrderState OrderState
        {
            get { return orderState; }
			set { orderState = value; }
		}
    }
}