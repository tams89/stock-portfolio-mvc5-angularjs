// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateNewsBulletinEventArgs.cs" company="">
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
	public class UpdateNewsBulletinEventArgs : EventArgs
	{
	    /// <summary>
	    /// The message.
	    /// </summary>
	    private string message;

	    /// <summary>
	    /// The msg id.
	    /// </summary>
	    private int msgId;

	    /// <summary>
	    /// The msg type.
	    /// </summary>
	    private NewsType msgType;

	    /// <summary>
	    /// The origin exchange.
	    /// </summary>
	    private string originExchange;

		/// <summary>
		/// Initializes a new instance of the <see cref="UpdateNewsBulletinEventArgs"/> class. 
		/// Full Constructor
		/// </summary>
		/// <param name="msgId">
		/// The bulletin ID, incrementing for each new bulletin.
		/// </param>
		/// <param name="msgType">
		/// Specifies the type of bulletin.
		/// </param>
		/// <param name="message">
		/// The bulletin's message text.
		/// </param>
		/// <param name="originExchange">
		/// The exchange from which this message originated.
		/// </param>
		public UpdateNewsBulletinEventArgs(int msgId, NewsType msgType, string message, string originExchange)
		{
			this.msgId = msgId;
			this.originExchange = originExchange;
			this.message = message;
			this.msgType = msgType;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UpdateNewsBulletinEventArgs"/> class. 
		/// Uninitialized Constructor for Serialization
		/// </summary>
		public UpdateNewsBulletinEventArgs()
		{
			
		}

		/// <summary>
		/// The bulletin ID, incrementing for each new bulletin.
		/// </summary>
		public int MsgId
		{
			get { return msgId; }
			set { msgId = value; }
		}

		/// <summary>
		/// Specifies the type of bulletin.
		/// </summary>
		/// <seealso cref="NewsType"/>
		public NewsType MsgType
		{
			get { return msgType; }
			set { msgType = value; }
		}

		/// <summary>
		/// The bulletin's message text.
		/// </summary>
		public string Message
		{
			get { return message; }
			set { message = value; }
		}

		/// <summary>
		/// The exchange from which this message originated.
		/// </summary>
		public string OriginExchange
		{
			get { return originExchange; }
			set { originExchange = value; }
		}
	}
}