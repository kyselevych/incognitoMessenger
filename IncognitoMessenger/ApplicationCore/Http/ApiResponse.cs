namespace IncognitoMessenger.ApplicationCore
{
    public class ApiResponse
    {
        public object Data { get; set; } = new object();
        public ErrorResponse Error { get; set; } = new ErrorResponse();

        public static ApiResponse Create(object data) => new ApiResponse() { Data = data };

        public static ApiResponse CreateError(string message) => new ApiResponse() 
        { 
            Error = new ErrorResponse() { Message = message }
        };
    }
}
