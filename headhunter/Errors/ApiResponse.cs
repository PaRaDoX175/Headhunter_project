namespace headhunter.Errors
{
    public class ApiResponse : ApiException
    {
        public ApiResponse(int statusCode, string details = null, string message = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}
