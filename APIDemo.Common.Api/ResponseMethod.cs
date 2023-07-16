using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIDemo.Common.Api
{
    public class ResponseMethod
    {
        public static T HttpResponseResult<T>(IActionResult actionResult)
        {
            // Assuming you have an IActionResult instance called 'actionResult'
            T response = default(T);

            if (actionResult is ContentResult contentResult)
            {
                string content = contentResult.Content;
                response = JsonConvert.DeserializeObject<T>(content);
            }

            return response;
        }
    }
}
