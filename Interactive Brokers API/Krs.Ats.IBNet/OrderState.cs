// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderState.cs" company="">
//   
// </copyright>
// <summary>
//   The openOrder() callback with the new OrderState() object will now be invoked
//   each time TWS receives commission information for a trade.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

namespace Krs.Ats.IBNet
{
	/// <summary>
	/// The openOrder() callback with the new OrderState() object will now be invoked
	/// each time TWS receives commission information for a trade.
	/// </summary>
	[Serializable()]
	public class OrderState
	{
		#region Private Variables

	    /// <summary>
	    /// The status.
	    /// </summary>
	    private OrderStatus status;

	    /// <summary>
	    /// The init margin.
	    /// </summary>
	    private string initMargin;

	    /// <summary>
	    /// The maint margin.
	    /// </summary>
	    private string maintMargin;

	    /// <summary>
	    /// The equity with loan.
	    /// </summary>
	    private string equityWithLoan;

	    /// <summary>
	    /// The commission.
	    /// </summary>
	    private double commission;

	    /// <summary>
	    /// The min commission.
	    /// </summary>
	    private double minCommission;

	    /// <summary>
	    /// The max commission.
	    /// </summary>
	    private double maxCommission;

	    /// <summary>
	    /// The commission currency.
	    /// </summary>
	    private string commissionCurrency;

	    /// <summary>
	    /// The warning text.
	    /// </summary>
	    private string warningText;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderState"/> class. 
		/// Default Constructor
		/// </summary>
		public OrderState():this(OrderStatus.None, null, null, null, 0.0, 0.0, 0.0, null, null)
		{
		}
		

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderState"/> class. 
		/// Fully Specified Constructor
		/// </summary>
		/// <param name="status">
		/// Order Status
		/// </param>
		/// <param name="initMargin">
		/// Initial margin requirement for the order.
		/// </param>
		/// <param name="maintMargin">
		/// Maintenance margin requirement for the order.
		/// </param>
		/// <param name="equityWithLoan">
		/// </param>
		/// <param name="commission">
		/// </param>
		/// <param name="minCommission">
		/// </param>
		/// <param name="maxCommission">
		/// </param>
		/// <param name="commissionCurrency">
		/// </param>
		/// <param name="warningText">
		/// </param>
		public OrderState(OrderStatus status, string initMargin, string maintMargin, string equityWithLoan, double commission, double minCommission, double maxCommission, string commissionCurrency, string warningText)
		{
			this.status = status;
			this.initMargin = initMargin;
			this.maintMargin = maintMargin;
			this.equityWithLoan = equityWithLoan;
			this.commission = commission;
			this.minCommission = minCommission;
			this.maxCommission = maxCommission;
			this.commissionCurrency = commissionCurrency;
			this.warningText = warningText;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Order Status
		/// </summary>
		public OrderStatus Status
		{
			get { return status; }
			set { status = value; }
		}

		/// <summary>
		/// Initial margin requirement for the order.
		/// </summary>
		public string InitMargin
		{
			get { return initMargin; }
			set { initMargin = value; }
		}

		/// <summary>
		/// Maintenance margin requirement for the order.
		/// </summary>
		public string MaintMargin
		{
			get { return maintMargin; }
			set { maintMargin = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public double Commission
		{
			get { return commission; }
			set { commission = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public double MinCommission
		{
			get { return minCommission; }
			set { minCommission = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public double MaxCommission
		{
			get { return maxCommission; }
			set { maxCommission = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string CommissionCurrency
		{
			get { return commissionCurrency; }
			set { commissionCurrency = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string WarningText
		{
			get { return warningText; }
			set { warningText = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string EquityWithLoan
		{
			get { return equityWithLoan; }
			set { equityWithLoan = value; }
		}

		#endregion
	}
}