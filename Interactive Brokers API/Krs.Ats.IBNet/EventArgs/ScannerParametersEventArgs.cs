// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScannerParametersEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Scanner Parameters Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Scanner Parameters Event Arguments
	/// </summary>
	[Serializable()]
	public class ScannerParametersEventArgs : EventArgs
	{
	    /// <summary>
	    /// The xml.
	    /// </summary>
	    private string xml;

		/// <summary>
		/// Initializes a new instance of the <see cref="ScannerParametersEventArgs"/> class. 
		/// Full Constructor
		/// </summary>
		/// <param name="xml">
		/// Document describing available scanner subscription parameters.
		/// </param>
		public ScannerParametersEventArgs(string xml)
		{
			this.xml = xml;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ScannerParametersEventArgs"/> class. 
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public ScannerParametersEventArgs()
		{
			
		}

		/// <summary>
		/// Document describing available scanner subscription parameters.
		/// </summary>
		public string Xml
		{
			get { return xml; }
			set { xml = value; }
		}
	}
}