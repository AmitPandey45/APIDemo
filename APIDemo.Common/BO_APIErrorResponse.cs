namespace APIDemo.Common
{
    public class BO_APIErrorResponse
    {
        /// <summary>
        /// The default constructor is only for the DeserializeObject and SerializeObject.
        /// To initize the class we need to use the paramerized contructor
        /// </summary>
        public BO_APIErrorResponse()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="method">MethodName</param>
        /// <param name="status">status</param>
        /// <param name="message">message</param>       
        public BO_APIErrorResponse(int code, string method, string message, bool status = false)
        {
            this.MethodName = method;
            this.Status = status;
            this.StatusCode = code;
            this.Message = message;
            this.Result = new List<string> { };
        }

        public BO_APIErrorResponse(int code, string method, bool status = false)
        {
            this.MethodName = method;
            this.Status = status;
            this.StatusCode = code;
            this.Message = CustomerValidationMessage.StatusCodeMessage[code];
            this.Result = new List<string> { };
        }

        /// <summary>
        /// API Name
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// ResponseStatus
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// ResponseCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// ResponseMessage
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ResponseData
        /// </summary>
        public List<string> Result { get; set; }
    }
}