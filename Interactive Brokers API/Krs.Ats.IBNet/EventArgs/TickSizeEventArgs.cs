// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TickSizeEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Tick Size Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Tick Size Event Arguments
	/// </summary>
	[Serializable()]
	public class TickSizeEventArgs : EventArgs
	{
	    /// <summary>
	    /// The size.
	    /// </summary>
	    private int size;

	    /// <summary>
	    /// The ticker id.
	    /// </summary>
	    private int tickerId;

	    /// <summary>
	    /// The tick type.
	    /// </summary>
	    private TickType tickType;

		/// <summary>
		/// Initializes a new instance of the <see cref="TickSizeEventArgs"/> class. 
		/// Full Constructor
		/// </summary>
		/// <param name="tickerId">
		/// The ticker Id that was specified previously in the call to reqMktData().
		/// </param>
		/// <param name="tickType">
		/// Specifies the type of price.
		/// </param>
		/// <param name="size">
		/// Specifies the size for the specified field.
		/// </param>
		public TickSizeEventArgs(int tickerId, TickType tickType, int size)
		{
			this.tickerId = tickerId;
			this.size = size;
			this.tickType = tickType;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TickSizeEventArgs"/> class. 
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public TickSizeEventArgs()
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
		/// Specifies the size for the specified field.
		/// </summary>
		public int Size
		{
			get { return size; }
			set { size = value; }
		}
	}
}