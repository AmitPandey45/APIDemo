namespace APIDemo.Common
{
    public class BO_ErrorResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="method">MethodName</param>
        /// <param name="status">status</param>
        /// <param name="message">message</param>
        /// <param name="uniqueRequestId">uniqueRequestId</param>
        public BO_ErrorResponse(int code, string method, bool status, string message, string uniqueRequestId)
        {
            this.ResponseCode = code;
            this.MethodName = method;
            this.ResponseStatus = status;
            this.ResponseMessage = message;
            this.ResponseData = null;
            this.UniqueRequestId = uniqueRequestId;
        }

        /// <summary>
        /// ResponseCode
        /// </summary>
        public int ResponseCode { get; set; }

        /// <summary>
        /// API Name
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// ResponseStatus
        /// </summary>
        public bool ResponseStatus { get; set; }

        /// <summary>
        /// ResponseMessage
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// ResponseData
        /// </summary>
        public List<string> ResponseData { get; set; }

        /// <summary>
        /// UniqueRequestId
        /// </summary>
        public string UniqueRequestId { get; set; }
    }
}
