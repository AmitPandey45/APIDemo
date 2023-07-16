using APIDemo.Common.CommonConstants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace APIDemo.Common
{
    public static class CustomLog
    {
        private static ILogger logger;

        public static void SetNLogObject(ILogger loggerObj)
        {
            logger = loggerObj;
        }

        public static string GetCustomLog(HttpContext context, string logInfo, string logLevel, string message, int? httpStatusCode = null)
        {
            string userId = null;
            string finalJson = null;

            if (context != null)
            {
                var request = context.Request;
                var headers = request.Headers;

                headers.TryGetValue("UserId", out var headerValue);

                userId = headerValue.FirstOrDefault();

                var hostName = Dns.GetHostName();

                finalJson = GetCustomLog(logInfo, logLevel, message, userId, request.Path, request.Method, httpStatusCode);
            }
            else
            {
                finalJson = GetCustomLog(logInfo, logLevel, message);
            }

            return finalJson;
        }

        public static string GetCustomLog(string logInfo, string logLevel, string message, string userId = null, string apiPath = SystemAPIConstant.SingleSpace, string httpMethod = SystemAPIConstant.SingleSpace, int? httpStatusCode = null)
        {
            string finalJson = null;
            var hostName = Dns.GetHostName();
            var log = new NLogDetails
            {
                TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                LogInfo = logInfo,
                LogLevel = logLevel,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                TenantId = "ASTM Tenant",
                AccountId = "ASTM Account",
                UserId = userId,
                Api = apiPath,
                Message = message,
                HttpMethod = httpMethod,
                HostName = hostName,
                HttpStatusCode = httpStatusCode,
            };

            finalJson = JsonConvert.SerializeObject(log);
            return finalJson;
        }

        public static void LogTrace(HttpContext context, string methodName, string message, int? statusCode = null)
        {
            string finalJson = GetCustomLog(context, methodName, SystemAPIConstant.TraceLogLevel.ToUpper(), message, httpStatusCode: statusCode);
            logger.LogInformation(finalJson);
        }

        public static void LogTrace(string methodName, string message, int? statusCode = null)
        {
            string finalJson = GetCustomLog(methodName, SystemAPIConstant.TraceLogLevel.ToUpper(), message, httpStatusCode: statusCode);
            logger.LogInformation(finalJson);
        }

        public static void LogDebug(HttpContext context, string methodName, string message, int? statusCode = null)
        {
            string finalJson = GetCustomLog(context, methodName, SystemAPIConstant.DebugLogLevel.ToUpper(), message, httpStatusCode: statusCode);
            logger.LogInformation(finalJson);
        }

        public static void LogDebug(string methodName, string message, int? statusCode = null)
        {
            string finalJson = GetCustomLog(methodName, SystemAPIConstant.DebugLogLevel.ToUpper(), message, httpStatusCode: statusCode);
            logger.LogInformation(finalJson);
        }

        public static void LogInfo(HttpContext context, string methodName, string message, int? statusCode = null)
        {
            string finalJson = GetCustomLog(context, methodName, SystemAPIConstant.InfoLogLevel.ToUpper(), message, httpStatusCode: statusCode);
            logger.LogInformation(finalJson);
        }

        public static void LogInfo(string methodName, string message, int? statusCode = null)
        {
            string finalJson = GetCustomLog(methodName, SystemAPIConstant.InfoLogLevel.ToUpper(), message, httpStatusCode: statusCode);
            logger.LogInformation(finalJson);
        }

        public static void LogWarn(HttpContext context, string methodName, string message, int? statusCode = null)
        {
            statusCode = statusCode == null ? 200 : statusCode;
            string finalJson = GetCustomLog(context, methodName, SystemAPIConstant.WarnLogLevel.ToUpper(), message, httpStatusCode: statusCode);
            logger.LogWarning(finalJson);
        }

        public static void LogWarn(string methodName, string message, int? statusCode = null)
        {
            statusCode = statusCode == null ? 200 : statusCode;
            string finalJson = GetCustomLog(methodName, SystemAPIConstant.WarnLogLevel.ToUpper(), message, httpStatusCode: statusCode);
            logger.LogWarning(finalJson);
        }

        public static void LogError(HttpContext context, string methodName, Exception ex, int? statusCode = null)
        {
            statusCode = statusCode == null ? 500 : statusCode;
            string errorDetails = GetExceptionDetails(ex);
            string finalJson = GetCustomLog(context, methodName, SystemAPIConstant.ErrorLogLevel.ToUpper(), errorDetails, httpStatusCode: statusCode);
            logger.LogError(finalJson);
        }

        public static void LogError(HttpContext context, string methodName, string errorDetails, int? statusCode = null)
        {
            statusCode = statusCode == null ? 500 : statusCode;
            string finalJson = GetCustomLog(context, methodName, SystemAPIConstant.ErrorLogLevel.ToUpper(), errorDetails, httpStatusCode: statusCode);
            logger.LogError(finalJson);
        }

        public static void LogException(HttpContext context, string methodName, Exception ex, int? statusCode = null)
        {
            statusCode = statusCode == null ? 500 : statusCode;
            string errorMessage = ex.Message;
            string finalJson = GetCustomLog(context, methodName, SystemAPIConstant.ErrorLogLevel.ToUpper(), errorMessage, httpStatusCode: statusCode);
            logger.LogError(new EventId(), finalJson, ex);
        }

        public static void LogError(string methodName, Exception ex, int? statusCode = null)
        {
            statusCode = statusCode == null ? 500 : statusCode;
            string errorDetails = GetExceptionDetails(ex);
            string finalJson = GetCustomLog(methodName, SystemAPIConstant.ErrorLogLevel.ToUpper(), errorDetails, httpStatusCode: statusCode);
            logger.LogError(finalJson);
        }

        public static void LogError(string methodName, string errorDetails, int? statusCode = null)
        {
            statusCode = statusCode == null ? 500 : statusCode;
            string finalJson = GetCustomLog(methodName, SystemAPIConstant.ErrorLogLevel.ToUpper(), errorDetails, httpStatusCode: statusCode);
            logger.LogError(finalJson);
        }

        public static void LogException(string methodName, Exception ex, int? statusCode = null)
        {
            statusCode = statusCode == null ? 500 : statusCode;
            string errorMessage = ex.Message;
            string finalJson = GetCustomLog(methodName, SystemAPIConstant.ErrorLogLevel.ToUpper(), errorMessage, httpStatusCode: statusCode);
            logger.LogError(new EventId(), finalJson, ex);
        }

        public static void LogFatal(HttpContext context, string methodName, Exception ex, int? statusCode = null)
        {
            statusCode = statusCode == null ? 503 : statusCode;
            string errorDetails = GetExceptionDetails(ex);
            string finalJson = GetCustomLog(context, methodName, SystemAPIConstant.FatalLogLevel.ToUpper(), errorDetails, httpStatusCode: statusCode);
            logger.LogCritical(finalJson);
        }

        public static void LogFatal(HttpContext context, string methodName, string errorDetails, int? statusCode = null)
        {
            statusCode = statusCode == null ? 503 : statusCode;
            string finalJson = GetCustomLog(context, methodName, SystemAPIConstant.FatalLogLevel.ToUpper(), errorDetails, httpStatusCode: statusCode);
            logger.LogCritical(finalJson);
        }

        public static void LogFatalException(HttpContext context, string methodName, Exception ex, int? statusCode = null)
        {
            statusCode = statusCode == null ? 503 : statusCode;
            string errorMessage = ex.Message;
            string finalJson = GetCustomLog(context, methodName, SystemAPIConstant.FatalLogLevel.ToUpper(), errorMessage, httpStatusCode: statusCode);
            logger.LogError(new EventId(), finalJson, ex);
        }

        public static void LogFatal(string methodName, Exception ex, int? statusCode = null)
        {
            statusCode = statusCode == null ? 503 : statusCode;
            string errorDetails = GetExceptionDetails(ex);
            string finalJson = GetCustomLog(methodName, SystemAPIConstant.FatalLogLevel.ToUpper(), errorDetails, httpStatusCode: statusCode);
            logger.LogCritical(finalJson);
        }

        public static void LogFatal(string methodName, string errorDetails, int? statusCode = null)
        {
            statusCode = statusCode == null ? 503 : statusCode;
            string finalJson = GetCustomLog(methodName, SystemAPIConstant.FatalLogLevel.ToUpper(), errorDetails, httpStatusCode: statusCode);
            logger.LogCritical(finalJson);
        }

        public static void LogFatalException(string methodName, Exception ex, int? statusCode = null)
        {
            statusCode = statusCode == null ? 503 : statusCode;
            string errorMessage = ex.Message;
            string finalJson = GetCustomLog(methodName, SystemAPIConstant.FatalLogLevel.ToUpper(), errorMessage, httpStatusCode: statusCode);
            logger.LogError(new EventId(), finalJson, ex);
        }

        public static string GetExceptionDetails(Exception ex)
        {
            string errorMessage = $"Message => {ex.Message}, Stack Trace => {ex.ToString()}";
            return errorMessage;
        }
    }
}
