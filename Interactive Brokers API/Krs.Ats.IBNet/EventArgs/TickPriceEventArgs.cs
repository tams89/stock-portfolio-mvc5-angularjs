// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TickPriceEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Tick Price Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Tick Price Event Arguments
	/// </summary>
	[Serializable()]
	public class TickPriceEventArgs : EventArgs
	{
	    /// <summary>
	    /// The can auto execute.
	    /// </summary>
	    private bool canAutoExecute;

	    /// <summary>
	    /// The price.
	    /// </summary>
	    private decimal price;

	    /// <summary>
	    /// The ticker id.
	    /// </summary>
	    private int tickerId;

	    /// <summary>
	    /// The tick type.
	    /// </summary>
	    private TickType tickType;

		/// <summary>
		/// Initializes a new instance of the <see cref="TickPriceEventArgs"/> class. 
		/// Full Constructor
		/// </summary>
		/// <param name="tickerId">
		/// The ticker Id that was specified previously in the call to reqMktData().
		/// </param>
		/// <param name="tickType">
		/// Specifies the type of price.
		/// </param>
		/// <param name="price">
		/// Specifies the price for the specified field.
		/// </param>
		/// <param name="canAutoExecute">
		/// specifies whether the price tick is available for automatic execution.
		/// </param>
		public TickPriceEventArgs(int tickerId, TickType tickType, decimal price, bool canAutoExecute)
		{
			this.tickerId = tickerId;
			this.canAutoExecute = canAutoExecute;
			this.price = price;
			this.tickType = tickType;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TickPriceEventArgs"/> class. 
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public TickPriceEventArgs()
		{
			
		}

		/// <summary>
		/// The ticker Id that was specified previously in the call to reqMktData().
		/// </summary>
		public int TickerId
		{
			get { return tickerId; }
			set { tickerId = value; }
		}

		/// <summary>
		/// Specifies the type of price.
		/// </summary>
		public TickType TickType
		{
			get { return tickType; }
			set { tickType = value; }
		}

		/// <summary>
		/// Specifies the price for the specified field.
		/// </summary>
		public decimal Price
		{
			get { return price; }
			set { price = value; }
		}

		/// <summary>
		/// specifies whether the price tick is available for automatic execution.
		/// </summary>
		/// <remarks>Possible values are:
		/// 0 = not eligible for automatic execution
		/// 1 = eligible for automatic execution</remarks>
		public bool CanAutoExecute
		{
			get { return canAutoExecute; }
			set { canAutoExecute = value; }
		}
	}
}