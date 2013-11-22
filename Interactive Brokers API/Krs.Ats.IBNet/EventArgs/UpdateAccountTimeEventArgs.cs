// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateAccountTimeEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Update Account Time Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Update Account Time Event Arguments
	/// </summary>
	[Serializable()]
	public class UpdateAccountTimeEventArgs : EventArgs
	{
	    /// <summary>
	    /// The timestamp.
	    /// </summary>
	    private string timestamp;

		/// <summary>
		/// Initializes a new instance of the <see cref="UpdateAccountTimeEventArgs"/> class. 
		/// Full Constructor
		/// </summary>
		/// <param name="timestamp">
		/// Current system time on the server side.
		/// </param>
		public UpdateAccountTimeEventArgs(string timestamp)
		{
			this.timestamp = timestamp;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UpdateAccountTimeEventArgs"/> class. 
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public UpdateAccountTimeEventArgs()
		{
			
		}

		/// <summary>
		/// Current system time on the server side.
		/// </summary>
		public string Timestamp
		{
			get { return timestamp; }
			set { timestamp = value; }
		}
	}
}