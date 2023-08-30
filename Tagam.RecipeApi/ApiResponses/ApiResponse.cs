using System.Net;

namespace Tagam.RecipeApi.ApiResponses
{
    public class ApiResponse
    {
        const string SUCCESS= "Success";
        const string ERROR= "Error";
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public ApiResponse(HttpStatusCode code, object data = null, string message = SUCCESS)
        {
            Code = (int)code;
            Message = ((int)code >= 200 && (int)code <= 200) ? message : ERROR;
            Data = data;
        }
    }
}
