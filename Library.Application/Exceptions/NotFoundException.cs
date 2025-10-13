using System.Net;

namespace Library.BusinessLogic.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message): base("NotFound", message,HttpStatusCode.NotFound) { }

        public static NotFoundException BookNotFound(int id) => new ($"Book with ID {id} is missing");
        public static NotFoundException AuthorNotFound(int id) => new ($"Author with ID {id} is missing");
    }
}
