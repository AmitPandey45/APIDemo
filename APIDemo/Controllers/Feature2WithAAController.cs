using APIDemo.Common;
using APIDemo.Common.Api;
using APIDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace APIDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Feature2WithAAController : BaseController
    {
        public Feature2WithAAController(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {

        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        //[HttpGet("GetDataWithRequiredRoute/{param1}/{param2}/{param3}", Name = "Feature2GetDataWithRequiredRoute")]
        [HttpGet]
        [Route("GetDataWithRequiredRoute/{param1}/{param2}/{param3}")]
        public IActionResult GetDataWithRequiredRoute(string param1, int param2, double param3)
        {
            string result = $"Param1 = {param1}, Param2 = {param2}, Param3 = {param3}";

            return this.CreateResponse(HttpStatusCode.OK, new BO_APIResponse<string>(200, MethodBase.GetCurrentMethod().Name, result));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        //[HttpGet("GetDataWithQueryString", Name = "Feature2GetDataWithQueryString")]
        [HttpGet]
        [Route("GetDataWithQueryString")]
        public IActionResult GetDataWithQueryString(string param1, int param2, double param3)
        {
            string result = $"Param1 = {param1}, Param2 = {param2}, Param3 = {param3}";

            return this.CreateResponse(HttpStatusCode.OK, new BO_APIResponse<string>(200, MethodBase.GetCurrentMethod().Name, result));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        //[HttpGet("GetDataWithRequiredAndOptionalRoute/{param1}/{param2?}/{param3?}/{param4?}", Name = "Feature2GetDataWithRequiredAndOptionalRoute")]
        [HttpGet]
        [Route("GetDataWithRequiredAndOptionalRoute/{param1}/{param2?}/{param3?}/{param4?}")]
        public IActionResult GetDataWithRequiredAndOptionalRoute(string param1, int? param2 = 0, double? param3 = 0, string param4 = null)
        {
            string result = $"Param1 = {param1}, Param2 = {param2}, Param3 = {param3}, Param4 = {param4}";

            return this.CreateResponse(HttpStatusCode.OK, new BO_APIResponse<string>(200, MethodBase.GetCurrentMethod().Name, result));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        //[HttpGet("GetDataWithOptionalRoute/{param1?}/{param2?}/{param3?}", Name = "Feature2GetDataWithOptionalRoute")]
        [HttpGet]
        [Route("GetDataWithOptionalRoute/{param1?}/{param2?}/{param3?}")]
        public IActionResult GetDataWithOptionalRoute(string param1, int? param2 = 0, double? param3 = 0)
        {
            string result = $"Param1 = {param1}, Param2 = {param2}, Param3 = {param3}";

            return this.CreateResponse(HttpStatusCode.OK, new BO_APIResponse<string>(200, MethodBase.GetCurrentMethod().Name, result));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        //[HttpGet("GetDataWithRequiredAndOptionalRouteAndQueryString/{param1}/{param2?}/{param3?}/{param4?}", Name = "Feature2GetDataWithRequiredAndOptionalRouteAndQueryString")]
        [HttpGet]
        [Route("GetDataWithRequiredAndOptionalRouteAndQueryString/{param1}/{param2?}/{param3?}/{param4?}")]
        public IActionResult GetDataWithRequiredAndOptionalRouteAndQueryString(string param1, string param5, int? param2 = 0, double? param3 = 0, string param4 = null)
        {
            string result = $"Param1 = {param1}, Param2 = {param2}, Param3 = {param3}, Param4 = {param4}, Param5 = {param5}";

            return this.CreateResponse(HttpStatusCode.OK, new BO_APIResponse<string>(200, MethodBase.GetCurrentMethod().Name, result));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [HttpPost]
        [Route("CreateData")]
        public IActionResult CreateData([FromBody] CreateDataModel request)
        {
            string result = JsonConvert.SerializeObject(request, Formatting.Indented);

            return this.CreateResponse(HttpStatusCode.OK, new BO_APIResponse<string>(200, MethodBase.GetCurrentMethod().Name, result));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [HttpPut]
        [Route("UpdateAllFieldsData/{id}")]
        public IActionResult UpdateAllFieldsData(int id, [FromBody] CreateDataModel request)
        {
            string result = JsonConvert.SerializeObject(request, Formatting.Indented);

            return this.CreateResponse(HttpStatusCode.OK, new BO_APIResponse<string>(200, MethodBase.GetCurrentMethod().Name, result));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [HttpPatch]
        [Route("UpdateFewFieldsData/{id}")]
        public IActionResult UpdateFewFieldsData(int id, [FromBody] CreateDataModel request)
        {
            string result = JsonConvert.SerializeObject(request, Formatting.Indented);

            return this.CreateResponse(HttpStatusCode.OK, new BO_APIResponse<string>(200, MethodBase.GetCurrentMethod().Name, result));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [HttpDelete]
        [Route("DeleteData/{id}")]
        public IActionResult DeleteData(int id, [FromBody] CreateDataModel request)
        {
            string result = JsonConvert.SerializeObject(request, Formatting.Indented);

            return this.CreateResponse(HttpStatusCode.OK, new BO_APIResponse<string>(200, MethodBase.GetCurrentMethod().Name, result));
        }
    }
}
