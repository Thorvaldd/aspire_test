using System.Net.Http.Headers;
using System.Reflection;

namespace Emp.GravatarAPI.Implementations.HttpClientHandlers;

public class UserAgentHeaderHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        ProductInfoHeaderValue headerInfo = new("EMP.PostOffice", version ?? "1.0.0");
        
        request.Headers.UserAgent.Add(headerInfo);
        
        return base.SendAsync(request, cancellationToken);
    }
}