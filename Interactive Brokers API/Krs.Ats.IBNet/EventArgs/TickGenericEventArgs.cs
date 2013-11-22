// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TickGenericEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Tick Generic Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Tick Generic Event Arguments
	/// </summary>
	[Serializable()]
	public class TickGenericEventArgs : EventArgs
	{
	    /// <summary>
	    /// The ticker id.
	    /// </summary>
	    private int tickerId;

	    /// <summary>
	    /// The tick type.
	    /// </summary>
	    private TickType tickType;

	    /// <summary>
	    /// The value.
	    /// </summary>
	    private double value;

		/// <summary>
		/// Initializes a new instance of the <see cref="TickGenericEventArgs"/> class. 
		/// Full Constructor
		/// </summary>
		/// <param name="tickerId">
		/// The ticker Id that was specified previously in the call to reqMktData().
		/// </param>
		/// <param name="tickType">
		/// Specifies the type of price.
		/// </param>
		/// <param name="value">
		/// The value of the specified field.
		/// </param>
		public TickGenericEventArgs(int tickerId, TickType tickType, double value)
		{
			this.tickerId = tickerId;
			this.value = value;
			this.tickType = tickType;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TickGenericEventArgs"/> class. 
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public TickGenericEventArgs()
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
		/// <seealso cref="TickType"/>
		public TickType TickType
		{
			get { return tickType; }
			set { tickType = value; }
		}

		/// <summary>
		/// The value of the specified field.
		/// </summary>
		public double Value
		{
			get { return value; }
			set { this.value = value; }
		}
	}
}