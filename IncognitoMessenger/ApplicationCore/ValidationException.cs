using FluentValidation.Results;

namespace IncognitoMessenger.ApplicationCore
{
    public class ValidationException : Exception
    {
        public ValidationException() { }
        public ValidationException(string result): base(result) { }
        public ValidationException(ValidationResult result): base(result.ToString()) { }
    } 
}
