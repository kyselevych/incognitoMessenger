namespace IncognitoMessenger.ApplicationCore
{
    public class ApiResponse
    {
        public object Data { get; set; } = new object();
        public ErrorResponse? Error { get; set; }

        public static ApiResponse Success(object data) => new ApiResponse() { Data = data };

        public static ApiResponse Failure(string message) => new ApiResponse() 
        {
            Error = new ErrorResponse() { Message = message }
        };
    }
}
