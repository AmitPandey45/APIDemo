using APIDemo.Common.CommonConstants;
using Newtonsoft.Json;
using System.Net;

namespace APIDemo.Common.Api.Middlewares
{
    public class MessageHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MessageHandlerMiddlewareOptions _options;

        public MessageHandlerMiddleware(RequestDelegate next, MessageHandlerMiddlewareOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await ProcessRequestAsync(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task ProcessRequestAsync(HttpContext context)
        {
            var typedHeaders = context.Request.GetTypedHeaders();
            CustomLog.LogTrace(context, _options.SystemAPILogInfo, "Received REST request");

            var originalBodyStream = context.Response.Body;
            var responseBodyText = string.Empty;

            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;
                memoryStream.Seek(0, SeekOrigin.Begin);
                responseBodyText = await new StreamReader(memoryStream).ReadToEndAsync();
                memoryStream.Seek(0, SeekOrigin.Begin);
                context.Response.Body = originalBodyStream;
            }

            if (context.Response != null && context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                CustomLog.LogFatal(context, _options.SystemAPILogInfo, responseBodyText, (int)HttpStatusCode.NotFound);
            }
            else if (context.Response != null && context.Response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                CustomLog.LogFatal(context, _options.SystemAPILogInfo, responseBodyText, (int)HttpStatusCode.BadRequest);
            }
            else if (context.Response != null && context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                CustomLog.LogError(context, _options.SystemAPILogInfo, responseBodyText, (int)HttpStatusCode.InternalServerError);
            }

            CustomLog.LogTrace(context, _options.SystemAPILogInfo, "REST request exit", 200);

            if (typedHeaders.Referer != null)
            {
                CustomLog.LogTrace(_options.SystemAPILogInfo, $"Referer test: {typedHeaders.Referer.ToString()}");
            }
            else
            {
                CustomLog.LogTrace(_options.SystemAPILogInfo, "Referer test: NULL");
            }

            await _next(context);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string path = "InvalidURLEncodedSequences";
            var uniqueRequestId = context.Request.Headers.FirstOrDefault(x => x.Key == "UniqueRequestId").Value.FirstOrDefault();
            CustomLog.LogError(context, _options.SystemAPILogInfo, exception);
            try
            {
                path = Uri.UnescapeDataString(context.Request.Path);
            }
            catch (ArgumentException argumentEx)
            {
                CustomLog.LogError(context, _options.SystemAPILogInfo, argumentEx, (int)HttpStatusCode.InternalServerError);
            }

            // Construct your error response object
            string displayName = context.GetRouteData().Values["action"] as string;
            displayName = displayName ?? path;

            var errorResponse = new BO_Response<string>((int)HttpStatusCode.InternalServerError, displayName, false, exception.Message, null, uniqueRequestId);

            // Set the response status code and content type
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = Constant.ApplicationJsonType;

            // Serialize the error response object
            var jsonResponse = JsonConvert.SerializeObject(errorResponse);

            // Write the response
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
