using System.Net;

namespace Emp.GravatarAPI.Exceptions;

[Serializable]
public class GravatarException : Exception
{
    public string GravatarMessage { get; set; }
    public HttpStatusCode ResponseStatusCode { get; set; }
    public GravatarException(string response, HttpStatusCode httpStatusCode):base(response)
    {
        GravatarMessage = response;
        ResponseStatusCode = httpStatusCode;
    }

    public GravatarException(string message) : base(message)
    {
        
    }
}