// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnderComp.cs" company="">
//   
// </copyright>
// <summary>
//   Underlying Component Class
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.ObjectModel;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// Underlying Component Class
	/// </summary>
	[Serializable]
	public class UnderComp
	{
	    #region Private Variables

	    /// <summary>
	    /// The con id.
	    /// </summary>
	    private int conId;

	    /// <summary>
	    /// The delta.
	    /// </summary>
	    private double delta;

	    /// <summary>
	    /// The price.
	    /// </summary>
	    private double price;

	    #endregion

	    #region Constructor / Deconstructor

	    /// <summary>
	    /// Initializes a new instance of the <see cref="UnderComp"/> class. 
	    /// Instantiate an UnderComp class
	    /// </summary>
	    public UnderComp()
	    {
	        conId = 0;
	        delta = 0;
	        price = 0;
	    }

	    #endregion

	    #region Public Properties

        /// <summary>
        /// Contract Id
        /// </summary>
	    public int ConId
	    {
            get { return conId; }
            set { conId = value; }
	    }

        /// <summary>
        /// Delta Value
        /// </summary>
	    public double Delta
	    {
            get { return delta; }
            set { delta = value; }
	    }

        /// <summary>
        /// Price
        /// </summary>
	    public double Price
	    {
            get { return price; }
            set { price = value; }
	    }

	    #endregion
	}
}