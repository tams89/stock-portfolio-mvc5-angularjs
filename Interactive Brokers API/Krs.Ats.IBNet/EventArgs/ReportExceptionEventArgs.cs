// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportExceptionEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Update News Bulletin Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Update News Bulletin Event Arguments
	/// </summary>
	[Serializable()]
	public class ReportExceptionEventArgs : EventArgs
	{
	    /// <summary>
	    /// The error.
	    /// </summary>
	    private Exception error;
		

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportExceptionEventArgs"/> class. 
		/// Full constructor.
		/// </summary>
		/// <param name="error">
		/// The exception that was thrown.
		/// </param>
		public ReportExceptionEventArgs(Exception error)
		{
				this.error = error;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportExceptionEventArgs"/> class. 
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public ReportExceptionEventArgs()
		{
			
		}
	

		/// <summary>
		/// The exception that was thrown.
		/// </summary>
		public Exception Error {
			get { return error; }
			set { error = value; }
		}
	
	}
}
