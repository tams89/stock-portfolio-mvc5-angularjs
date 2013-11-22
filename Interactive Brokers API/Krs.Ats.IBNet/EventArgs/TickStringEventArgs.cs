// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TickStringEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Tick String Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Tick String Event Arguments
	/// </summary>
	[Serializable()]
	public class TickStringEventArgs : EventArgs
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
	    private string value;

		/// <summary>
		/// Initializes a new instance of the <see cref="TickStringEventArgs"/> class. 
		/// Free Constructor
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
		public TickStringEventArgs(int tickerId, TickType tickType, string value)
		{
			this.tickerId = tickerId;
			this.value = value;
			this.tickType = tickType;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TickStringEventArgs"/> class. 
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public TickStringEventArgs()
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
		public string Value
		{
			get { return value; }
			set { this.value = value; }
		}
	}
}