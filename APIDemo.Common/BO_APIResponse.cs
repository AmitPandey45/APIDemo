using System.Text.Json.Serialization;

namespace APIDemo.Common
{
    public class BO_APIResponse<T>
    {
        /// <summary>
        /// BO_Response
        /// </summary>
        /// <param name="code">ResponseCode</param>
        /// <param name="method">MethodName</param>
        /// <param name="status">ResponseStatus</param>
        /// <param name="message">ResponseMessage</param>
        /// <param name="data">ResponseData</param>
        [JsonConstructor]
        public BO_APIResponse(int code, string method, string message, T data, bool status = true)
        {
            this.MethodName = method;
            this.Status = status;
            this.StatusCode = code;
            this.Message = message;
            this.Result = data;
        }

        public BO_APIResponse(int code, string method, T data, bool status = true)
        {
            this.MethodName = method;
            this.Status = status;
            this.StatusCode = code;
            this.Message = CustomerValidationMessage.StatusCodeMessage[code];
            this.Result = data;
        }

        /// <summary>
        /// MethodName
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// StatusCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// ResponseMessage
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ResponseData
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// BO_ErrorResponse
        /// </summary>
        /// <param name="e">exception</param>
        public static explicit operator BO_APIResponse<T>(BO_APIErrorResponse e)
        {
            T outobj = default(T);
            return new BO_APIResponse<T>(e.StatusCode, e.MethodName, e.Message, outobj, e.Status);
        }
    }
}
