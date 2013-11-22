// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TickOptionComputationEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Tick Option Computation Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Tick Option Computation Event Arguments
	/// </summary>
	[Serializable()]
	public class TickOptionComputationEventArgs : EventArgs
	{
	    /// <summary>
	    /// The delta.
	    /// </summary>
	    private double delta;

	    /// <summary>
	    /// The implied vol.
	    /// </summary>
	    private double impliedVol;

	    /// <summary>
	    /// The option price.
	    /// </summary>
	    private double optionPrice;

	    /// <summary>
	    /// The pv dividend.
	    /// </summary>
	    private double pvDividend;

	    /// <summary>
	    /// The ticker id.
	    /// </summary>
	    private int tickerId;

	    /// <summary>
	    /// The gamma.
	    /// </summary>
	    private double gamma;

	    /// <summary>
	    /// The vega.
	    /// </summary>
	    private double vega;

	    /// <summary>
	    /// The theta.
	    /// </summary>
	    private double theta;

	    /// <summary>
	    /// The underlying price.
	    /// </summary>
	    private double underlyingPrice;

	    /// <summary>
	    /// The tick type.
	    /// </summary>
	    private TickType tickType;

		/// <summary>
		/// Initializes a new instance of the <see cref="TickOptionComputationEventArgs"/> class. 
		/// Full Constructor
		/// </summary>
		/// <param name="tickerId">
		/// The ticker Id that was specified previously in the call to reqMktData().
		/// </param>
		/// <param name="tickType">
		/// Specifies the type of option computation.
		/// </param>
		/// <param name="impliedVol">
		/// The implied volatility calculated by the TWS option modeler, using the specificed ticktype value.
		/// </param>
		/// <param name="delta">
		/// The option delta calculated by the TWS option modeler.
		/// </param>
		/// <param name="optionPrice">
		/// The model price.
		/// </param>
		/// <param name="pvDividend">
		/// Present value of dividends expected on the option’s underlier.
		/// </param>
		/// <param name="gamma">
		/// Gamma
		/// </param>
		/// <param name="vega">
		/// Vega
		/// </param>
		/// <param name="theta">
		/// Theta
		/// </param>
		/// <param name="undPrice">
		/// Underlying Price
		/// </param>
		public TickOptionComputationEventArgs(int tickerId, TickType tickType, double impliedVol, double delta, double optionPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
		{
			this.tickerId = tickerId;
			this.pvDividend = pvDividend;
			this.delta = delta;
			this.optionPrice = optionPrice;
			this.impliedVol = impliedVol;
			this.tickType = tickType;
			this.gamma = gamma;
			this.vega = vega;
			this.theta = theta;
			underlyingPrice = undPrice;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TickOptionComputationEventArgs"/> class. 
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public TickOptionComputationEventArgs()
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
		/// Specifies the type of option computation.
		/// </summary>
		/// <seealso cref="TickType"/>
		public TickType TickType
		{
			get { return tickType; }
			set { tickType = value; }
		}

		/// <summary>
		/// The implied volatility calculated by the TWS option modeler, using the specificed ticktype value.
		/// </summary>
		public double ImpliedVol
		{
			get { return impliedVol; }
			set { impliedVol = value; }
		}

		/// <summary>
		/// The option delta calculated by the TWS option modeler.
		/// </summary>
		public double Delta
		{
			get { return delta; }
			set { delta = value; }
		}

		/// <summary>
		/// The Option price.
		/// </summary>
		public double OptionPrice
		{
			get { return optionPrice; }
			set { optionPrice = value; }
		}

		/// <summary>
		/// Present value of dividends expected on the option’s underlier.
		/// </summary>
		public double PVDividend
		{
			get { return pvDividend; }
			set { pvDividend = value; }
		}

		/// <summary>
		/// Gamma
		/// </summary>
		public double Gamma
		{
			get { return gamma; }
			set { gamma = value; }
		}

		/// <summary>
		/// Vega
		/// </summary>
		public double Vega
		{
			get { return vega; }
			set { vega = value; }
		}

		/// <summary>
		/// Theta
		/// </summary>
		public double Theta
		{
			get { return theta; }
			set { theta = value; }
		}

		/// <summary>
		/// Underlying Price
		/// </summary>
		public double UnderlyingPrice
		{
			get { return underlyingPrice; }
			set { underlyingPrice = value; }
		}
	}
}