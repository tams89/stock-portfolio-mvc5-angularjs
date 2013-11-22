// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReceiveFAEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Receive Financial Advisor Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Receive Financial Advisor Event Arguments
    /// </summary>
    [Serializable()]
    public class ReceiveFAEventArgs : EventArgs
    {
        /// <summary>
        /// The fa data type.
        /// </summary>
        private FADataType faDataType;

        /// <summary>
        /// The xml.
        /// </summary>
        private string xml;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveFAEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="faDataType">
        /// Specifies the type of Financial Advisor configuration data being received from TWS.
        /// </param>
        /// <param name="xml">
        /// The XML string containing the previously requested FA configuration information.
        /// </param>
        public ReceiveFAEventArgs(FADataType faDataType, string xml)
        {
            this.faDataType = faDataType;
            this.xml = xml;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiveFAEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ReceiveFAEventArgs()
        {
            
        }

        /// <summary>
        /// Specifies the type of Financial Advisor configuration data being received from TWS.
        /// </summary>
        /// <remarks>Valid values include:
        /// 1 = GROUPS
        /// 2 = PROFILE
        /// 3 = ACCOUNT ALIASES
        /// </remarks>
        public FADataType FADataType
        {
            get { return faDataType; }
			set { faDataType = value; }
		}

        /// <summary>
        /// The XML string containing the previously requested FA configuration information.
        /// </summary>
        public string Xml
        {
            get { return xml; }
			set { xml = value; }
		}
    }
}