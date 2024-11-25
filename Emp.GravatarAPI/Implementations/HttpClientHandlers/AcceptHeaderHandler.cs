using System.Net.Http.Headers;

namespace Emp.GravatarAPI.Implementations.HttpClientHandlers;

public class AcceptHeaderHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        MediaTypeWithQualityHeaderValue acceptHeader = new("application/json");
        
        request.Headers.Accept.Add(acceptHeader);
        
        return base.SendAsync(request, cancellationToken);
    }
}