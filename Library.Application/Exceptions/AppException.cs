using System.Net;

namespace Library.BusinessLogic.Exceptions
{
    public abstract class AppException: Exception
    {
        public string Title { get; }
        public HttpStatusCode StatusCode { get; }

        protected AppException(string title, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            Title = title;
            StatusCode = statusCode;
        }
    }
}
