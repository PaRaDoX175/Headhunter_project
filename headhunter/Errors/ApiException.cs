namespace headhunter.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unknown Error",
            };
        }
    }
}
