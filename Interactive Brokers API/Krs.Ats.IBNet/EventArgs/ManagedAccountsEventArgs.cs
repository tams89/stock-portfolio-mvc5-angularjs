// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagedAccountsEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Managed Accounts Event Arguments
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Managed Accounts Event Arguments
    /// </summary>
    [Serializable()]
    public class ManagedAccountsEventArgs : EventArgs
    {
        /// <summary>
        /// The accounts list.
        /// </summary>
        private string accountsList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedAccountsEventArgs"/> class. 
        /// Full Constructor
        /// </summary>
        /// <param name="accountsList">
        /// The comma delimited list of FA managed accounts.
        /// </param>
        public ManagedAccountsEventArgs(string accountsList)
        {
            this.accountsList = accountsList;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedAccountsEventArgs"/> class. 
        /// Uninitialized Constructor for Serialization
        /// </summary>
        public ManagedAccountsEventArgs()
        {
            
        }

        /// <summary>
        /// The comma delimited list of FA managed accounts.
        /// </summary>
        public string AccountsList
        {
            get { return accountsList; }
			set { accountsList = value; }
		}
    }
}