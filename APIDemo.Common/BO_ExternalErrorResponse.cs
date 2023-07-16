namespace APIDemo.Common
{
    public class BO_ExternalErrorResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="method">MethodName</param>
        /// <param name="message">message</param>
        public BO_ExternalErrorResponse(int code, string method, string message)
        {
            this.MethodName = method;
            this.ResponseCode = code;
            this.ResponseMessage = message;
            this.ResponseData = null;
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
        /// ResponseMessage
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// ResponseData
        /// </summary>
        public List<string> ResponseData { get; set; }
    }
}
