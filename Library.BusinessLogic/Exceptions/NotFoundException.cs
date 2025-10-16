using Library.BusinessLogic.Resources;
using System.Net;

namespace Library.BusinessLogic.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message): base("NotFound", message,HttpStatusCode.NotFound) { }

        public static NotFoundException BookNotFound(int id) => new (string.Format(BusinessMessages.BookNotFound,id));
        public static NotFoundException AuthorNotFound(int id) => new (string.Format(BusinessMessages.AuthorNotFound, id));
    }
}
