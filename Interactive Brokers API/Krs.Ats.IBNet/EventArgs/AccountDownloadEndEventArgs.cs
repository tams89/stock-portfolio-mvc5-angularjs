// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountDownloadEndEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Contract Details Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Contract Details Event Arguments
    /// </summary>
    [Serializable()]
    public class AccountDownloadEndEventArgs : EventArgs
    {
        /// <summary>
        /// The account name.
        /// </summary>
        private string accountName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountDownloadEndEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="accountName">
        /// Account Name
        /// </param>
        public AccountDownloadEndEventArgs(string accountName)
        {
            this.accountName = accountName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountDownloadEndEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public AccountDownloadEndEventArgs()
        {
            
        }

        /// <summary>
        /// Request Id
        /// </summary>
        public string AccountName
        {
            get { return accountName; }
			set { accountName = value; }
		}
    }
}