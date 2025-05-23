
namespace ECommerce.ErrorsHandling
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public ApiResponse(int statusCode,string message = null )
        {
            Message = message ?? GetMessageDependOnStatusCode(statusCode);
            StatusCode = statusCode;

        }

        private string GetMessageDependOnStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Somthing Went Wrong"
            };
        }
    }
}
