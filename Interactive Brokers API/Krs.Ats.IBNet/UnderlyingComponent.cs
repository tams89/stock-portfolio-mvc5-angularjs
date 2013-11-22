// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnderlyingComponent.cs" company="">
//   
// </copyright>
// <summary>
//   Underlying Component
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Text;

namespace Krs.Ats.IBNet
{
    /// <summary>
    /// Underlying Component
    /// </summary>
    [Serializable]
    public class UnderlyingComponent
    {
        #region Private Properties

        /// <summary>
        /// The contract id.
        /// </summary>
        private int contractId;

        /// <summary>
        /// The delta.
        /// </summary>
        private double delta;

        /// <summary>
        /// The price.
        /// </summary>
        private decimal price;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UnderlyingComponent"/> class. 
        /// Underlying Component
        /// </summary>
        public UnderlyingComponent()
        {
            contractId = 0;
            delta = 0;
            price = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Contract Id
        /// </summary>
        public int ContractId
        {
            get
            {
                return contractId;
            }

            set
            {
                contractId = value;
            }
        }

        /// <summary>
        /// Delta
        /// </summary>
        public double Delta
        {
            get
            {
                return delta;
            }

            set
            {
                delta = value;
            }
        }

        /// <summary>
        /// Price of underlying
        /// </summary>
        public decimal Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }
        #endregion
    }
}
