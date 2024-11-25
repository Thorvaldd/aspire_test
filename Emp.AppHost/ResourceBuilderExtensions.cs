using Microsoft.Extensions.DependencyInjection;

namespace Emp.AppHost;

public static class ResourceBuilderExtensions
{
    public static IResourceBuilder<TResource> WithHttpsCommand<TResource>(this IResourceBuilder<TResource> resourceBuilder,
        string path,
        string displayName,
        HttpMethod? method = default,
        string? endpointName = default,
        string? iconName =default)
    where TResource: IResourceWithEndpoints
    => WithHttpCommandImpl(resourceBuilder, path, displayName, endpointName ?? "http", method, "https", iconName);
    
    public static IResourceBuilder<TResource> WithHttpCommand<TResource>(this IResourceBuilder<TResource> builder,
        string path,
        string displayName,
        HttpMethod? method = default,
        string? endpointName = default,
        string? iconName = default)
        where TResource : IResourceWithEndpoints
        => WithHttpCommandImpl(builder, path, displayName, endpointName ?? "http", method, "http", iconName);


    private static IResourceBuilder<TResource> WithHttpCommandImpl<TResource>(this IResourceBuilder<TResource> builder,
        string path,
        string displayName,
        string endpointName,
        HttpMethod? method,
        string expectedScheme,
        string? iconName = default)
        where TResource : IResourceWithEndpoints
    {
        method ??= HttpMethod.Post;

        var endpoints = builder.Resource.GetEndpoints();
        var endpoint = endpoints
            .FirstOrDefault(x => string.Equals(x.EndpointName, endpointName, StringComparison.OrdinalIgnoreCase))
            ?? throw new DistributedApplicationException($"Could not create HTTP command for resource '{builder.Resource.Name}' as no endpoint named '{endpointName}' was found.");

        var commandName = $"http-{method.ToString().ToLowerInvariant()}-request";
        
        builder.WithCommand(commandName, displayName, async context =>
        {
            if (!endpoint.IsAllocated)
            {
                return new ExecuteCommandResult{Success = false, ErrorMessage = "Endpoints are not allocated."};
            }

            if (!string.Equals(endpoint.Scheme, expectedScheme, StringComparison.OrdinalIgnoreCase))
            {
                return new ExecuteCommandResult { Success = false, ErrorMessage = $"The endpoint named '{endpointName}' on resource '{builder.Resource.Name}' does not have the expected scheme of '{expectedScheme}'." };
            }

            var uri = new UriBuilder(endpoint.Url) { Path = path }.Uri;
            var httpClient = context.ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();
            var request = new HttpRequestMessage(method, uri);
            try
            {
                var response = await httpClient.SendAsync(request, context.CancellationToken);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return new ExecuteCommandResult { Success = false, ErrorMessage = e.Message };
            }
            
            return new ExecuteCommandResult { Success = true };
        },
            iconName: iconName,
            iconVariant: IconVariant.Regular);

        return builder;
    }
}