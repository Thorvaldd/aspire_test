using System.Net;

namespace Emp.GravatarAPI.Exceptions;

public class GravatarInternalException : GravatarException
{
    public GravatarInternalException(string response,
        HttpStatusCode httpStatusCode)
    :base(response, httpStatusCode)
    {
        
    }
}