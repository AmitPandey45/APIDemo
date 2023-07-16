namespace APIDemo.Common
{
    public class BO_Response<T>
    {
        public BO_Response()
        {
        }

        /// <summary>
        /// BO_Response
        /// </summary>
        /// <param name="code">ResponseCode</param>
        /// <param name="method">MethodName</param>
        /// <param name="status">ResponseStatus</param>
        /// <param name="message">ResponseMessage</param>
        /// <param name="data">ResponseData</param>
        public BO_Response(int code, string method, bool status, string message, T data, string uniqueRequestId = null)
        {
            this.ResponseCode = code;
            this.MethodName = method;
            this.ResponseStatus = status;
            this.ResponseMessage = message;
            this.ResponseData = data;
            this.UniqueRequestId = uniqueRequestId;
        }

        public string MethodName { get; set; }

        /// <summary>
        /// ResponseCode
        /// </summary>
        public int ResponseCode { get; set; }

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
        public T ResponseData { get; set; }

        /// <summary>
        /// UniqueRequestId
        /// </summary>
        public string UniqueRequestId { get; set; }

        /// <summary>
        /// BO_ErrorResponse
        /// </summary>
        /// <param name="e">exception</param>
        public static explicit operator BO_Response<T>(BO_ErrorResponse e)
        {
            T outobj = default(T);
            return new BO_Response<T>(e.ResponseCode, e.MethodName, e.ResponseStatus, e.ResponseMessage, outobj, e.UniqueRequestId);
        }
    }
}
