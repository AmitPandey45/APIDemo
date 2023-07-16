namespace APIDemo.Common
{
    public class BO_ExternalResponse<T>
    {
        /// <summary>
        /// API Name
        /// </summary>
        /// <summary>
        /// BO_Response
        /// </summary>
        /// <param name="code">ResponseCode</param>
        /// <param name="method">MethodName</param>        
        /// <param name="message">ResponseMessage</param>
        /// <param name="data">ResponseData</param>
        public BO_ExternalResponse(int code, string method, string message, T data)
        {
            this.ResponseCode = code;
            this.MethodName = method;
            this.ResponseMessage = message;
            this.ResponseData = data;
        }

        public string MethodName { get; set; }

        /// <summary>
        /// ResponseCode
        /// </summary>
        public int ResponseCode { get; set; }

        /// <summary>
        /// ResponseMessage
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// ResponseData
        /// </summary>
        public T ResponseData { get; set; }

        /// <summary>
        /// BO_ErrorResponse
        /// </summary>
        /// <param name="e">exception</param>
        public static explicit operator BO_ExternalResponse<T>(BO_ExternalErrorResponse e)
        {
            T outobj = default(T);
            return new BO_ExternalResponse<T>(e.ResponseCode, e.MethodName, e.ResponseMessage, outobj);
        }
    }
}
