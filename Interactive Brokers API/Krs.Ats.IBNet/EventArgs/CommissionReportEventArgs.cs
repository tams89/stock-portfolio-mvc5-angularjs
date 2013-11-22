// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommissionReportEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Commission Report Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

/* file created: June, 2013 - Shane Castle - shane.castle@vaultic.com */
namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Commission Report Event Arguments
    /// </summary>
    [Serializable()]
    public class CommissionReportEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommissionReportEventArgs"/> class. 
        /// Retuned by the executions event, contains the commission report.
        /// </summary>
        /// <param name="report">
        /// The commission report.
        /// </param>
        public CommissionReportEventArgs(CommissionReport report)
        {
            CommissionReport = report;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommissionReportEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public CommissionReportEventArgs() {}

        /// <summary>
        /// Contains the commission report details.
        /// </summary>
        public CommissionReport CommissionReport { get; set; }
    }
}