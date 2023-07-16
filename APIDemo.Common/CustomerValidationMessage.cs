using System.Net;

namespace APIDemo.Common
{
    public class CustomerValidationMessage
    {
        ////TODO:Please extend/refactor/map/arrange something like this
        private static Dictionary<int, string> statusCodeMessage = new Dictionary<int, string>
        {
            { (int)HttpStatusCode.OK, "success" },
            { (int)HttpStatusCode.BadRequest, "something went wrong" },
            { (int)HttpStatusCode.NotFound, "not found" },
            { 101, "validation falied" },
            { 102, "Incorrect Input!" },
            { 103, "CreatedBy/ModifiedBy is required!" },
            { (int)HttpStatusCode.NoContent, "No Content" },
            { (int)HttpStatusCode.InternalServerError, "can't process this request" }
        };

        public static Dictionary<int, string> StatusCodeMessage { get => statusCodeMessage; set => statusCodeMessage = value; }
    }
}
