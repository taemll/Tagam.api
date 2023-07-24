using System.Net;

namespace Tagam.RatingApi.ApiResponses
{
    public class ApiResponse
    {
        const string SUCCESS = "Successfull";
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public ApiResponse(HttpStatusCode code, object data = null, string message = SUCCESS)
        {
            Code = (int)code;
            Message = message;
            Data = data;
        }
    }
}
