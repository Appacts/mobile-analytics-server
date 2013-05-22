using System;
using System.Threading;
using System.Web;

namespace Base
{
    public class CometAsyncResult : IAsyncResult
    {
        #region //Private Properties
        private bool isCompleted = false;
        private bool completedSynchronously = false;
        private readonly WaitHandle asyncWaitHandle = null;
        private readonly object asyncState = null; 
        #endregion

        #region //Public Properties
        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        /// <value>
        /// The account id.
        /// </value>
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the data id.
        /// </summary>
        /// <value>
        /// The data id.
        /// </value>
        public int DataId { get; set; }

        /// <summary> Callback that will be called when operation is completed
        /// (When we have notification about hanges, or timeout)</summary>
        public AsyncCallback Callback;

        public HttpContext Context;

        /// <summary> True when there is notification, False if timeout </summary>
        public bool Positive;

        /// <summary>
        /// Gets a value that indicates whether the asynchronous operation has completed.
        /// </summary>
        /// <returns>true if the operation is complete; otherwise, false.</returns>
        public bool IsCompleted
        {
            get { return isCompleted; }
            set { isCompleted = value; }
        }
        /// <summary>
        /// Gets a value that indicates whether the asynchronous operation completed synchronously.
        /// </summary>
        /// <returns>true if the asynchronous operation completed synchronously; otherwise, false.</returns>
        public bool CompletedSynchronously
        {
            get { return completedSynchronously; }
            set { completedSynchronously = value; }
        }

        /// <summary>
        /// Gets a <see cref="T:System.Threading.WaitHandle"/> that is used to wait for an asynchronous operation to complete.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.WaitHandle"/> that is used to wait for an asynchronous operation to complete.</returns>
        public WaitHandle AsyncWaitHandle { get { return asyncWaitHandle; } }

        /// <summary>
        /// Gets a user-defined object that qualifies or contains information about an asynchronous operation.
        /// </summary>
        /// <returns>A user-defined object that qualifies or contains information about an asynchronous operation.</returns>
        public object AsyncState { get { return asyncState; } } 
        #endregion


        #region //Constructor
        /// <summary>
        /// Default constructor. Can be used when request completed
        /// synchronously. In this case must set CompletedSynchronously 
        /// to true.
        /// </summary>
        public CometAsyncResult()
        {
            this.Positive = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CometAsyncResult"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="asyncCallback">The async callback.</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="dataId">The data id.</param>
        public CometAsyncResult(HttpContext context, AsyncCallback asyncCallback, int accountId, int dataId)
        {
            this.Context = context;
            this.Callback = asyncCallback;
            this.AccountId = accountId;
            this.DataId = dataId;
            this.Positive = false;
        } 
        #endregion
    }
}
