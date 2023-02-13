using System.Net;

namespace Domain.Common;
public class WebShopException : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;
    public WebShopException() : base() { }

    public WebShopException(string message) : base(message) { }

    public WebShopException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
