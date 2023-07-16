using APIDemo.Common.CommonConstants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text.Json;

namespace APIDemo.Common.Api
{
    public abstract class BaseController : ControllerBase
    {
        private IHttpContextAccessor _contextAccessor;

        public int UserId { get; set; }

        public string CurrentUserName { get; set; }

        public string ClientIp { get; set; }

        public bool IsTestUserRequest { get; private set; }

        /// <summary>
        /// Constructor of base controller
        /// </summary>
        /// <param name="auditLogger"></param>
        public BaseController(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
            GetDataFromHeader(_contextAccessor.HttpContext);
        }

        /// <summary>
        /// Get Error Message
        /// </summary>
        /// <returns></returns>
        protected (string errorMessage, string errorCode) GetErrorMessage()
        {
            var message = string.Empty;
            var errorCode = string.Empty;
            foreach (var modelStateKey in this.ModelState.Keys)
            {
                var modelStateVal = this.ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    var exmessage = string.Empty;
                    var excode = string.Empty;
                    if (error.Exception == null)
                    {
                        exmessage = error.ErrorMessage;
                        if (exmessage.Split('~').Length > 1)
                        {
                            excode = exmessage.Split('~')[0];
                            exmessage = exmessage.Split('~')[1];
                        }
                        else
                        {
                            ////errorCode = 400;
                        }

                        errorCode += excode + ", ";
                        message += exmessage + "\r\n ";
                    }
                    else
                    {
                        ////message = error.ErrorMessage == string.Empty ? error.Exception.Message : error.ErrorMessage;
                        message = "Validation Error";
                        errorCode = "1001";
                    }
                    message = message.Trim().EndsWith("\r\n") ? message.Trim().Remove(message.Length - 4) : message;
                    errorCode = errorCode.Trim().EndsWith(",") ? errorCode.Trim().Remove(errorCode.Length - 2) : errorCode;
                    return (message.Trim(), errorCode);
                }
            }

            message = message.Trim().EndsWith("\r\n") ? message.Trim().Remove(message.Length - 4) : message;
            errorCode = errorCode.Trim().EndsWith(",") ? errorCode.Trim().Remove(errorCode.Length - 2) : errorCode;
            return (message.Trim(), errorCode);
        }

        private void GetDataFromHeader(HttpContext context)
        {
            if (context != null)
            {
                bool isTestMode;
                if (context.Request.Headers.TryGetValue("UserId", out var userIdValues) && userIdValues.Count > 0)
                {
                    UserId = Convert.ToInt32(userIdValues[0]);
                }

                if (context.Request.Headers.TryGetValue("UserName", out var userNameValues) && userNameValues.Count > 0)
                {
                    CurrentUserName = userNameValues[0];
                }

                if (context.Request.Headers.TryGetValue("ClientIp", out var clientIpValues) && clientIpValues.Count > 0)
                {
                    ClientIp = clientIpValues[0];
                }

                if (context.Request.Headers.TryGetValue("Is-Test-Mode", out var isTestModeValues) && isTestModeValues.Count > 0)
                {
                    Boolean.TryParse(isTestModeValues[0], out isTestMode);
                    IsTestUserRequest = isTestMode;
                }
            }
        }

        protected IActionResult BadRequestResponse(string methodname)
        {
            var errorResponse = new Common.BO_APIErrorResponse((int)HttpStatusCode.BadRequest, methodname);
            return CreateResponse(HttpStatusCode.BadRequest, errorResponse);
        }

        protected IActionResult DefaultBadRequestResponse(string methodname, int code = 401, string message = "Bad request!")
        {
            var errorResponse = new BO_APIErrorResponse(code, methodname, message);
            return CreateResponse(HttpStatusCode.BadRequest, errorResponse);
        }

        protected IActionResult NotFoundResponse(string methodname)
        {
            var errorResponse = new BO_APIErrorResponse((int)HttpStatusCode.NotFound, methodname);
            return CreateResponse(HttpStatusCode.NotFound, errorResponse);
        }

        protected IActionResult NoContentResponse<T>(string methodname, T response)
        {
            var successResponse = new BO_APIResponse<T>((int)HttpStatusCode.NoContent, methodname, CustomerValidationMessage.StatusCodeMessage[(int)HttpStatusCode.NoContent], response);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };

            var json = System.Text.Json.JsonSerializer.Serialize(successResponse, options);

            return Content(json, Constant.ApplicationJsonType);
        }

        protected IActionResult DefaultNoContentResponse()
        {
            return NoContent();
        }
        protected IActionResult OkResponse<T>(string methodname, List<T> results)
        {
            var successResponse = new BO_APIResponse<List<T>>((int)HttpStatusCode.OK, methodname, results);
            return CreateResponse(HttpStatusCode.OK, successResponse);
        }

        protected IActionResult CreateResponse<T>(HttpStatusCode code, T response)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
            };
            var camelCaseLoad = JsonConvert.SerializeObject(response, settings);

            return new ContentResult
            {
                Content = camelCaseLoad,
                ContentType = Constant.ApplicationJsonType,
                StatusCode = (int)code
            };
        }
    }
}
