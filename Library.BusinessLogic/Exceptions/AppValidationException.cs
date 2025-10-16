using System.Net;

namespace Library.BusinessLogic.Exceptions
{
    public class AppValidationException : AppException
    {
        public Dictionary<string, string[]> Errors { get; }

        public AppValidationException(Dictionary<string, string[]> errors)
            : base("Validation Error", "One or more validation errors occurred", HttpStatusCode.BadRequest)
        {
            Errors = errors;
        }
    }
}
