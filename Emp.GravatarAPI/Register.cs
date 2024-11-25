using Emp.GravatarAPI.Implementations;
using Emp.GravatarAPI.Implementations.Clients;
using Emp.GravatarAPI.Implementations.HttpClientHandlers;
using Emp.GravatarAPI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Emp.GravatarAPI;

public static class Register
{
    public static void RegisterGravatarApi(this IServiceCollection services)
    {
        services.TryAddScoped<IModelBuilder, ModelBuilder>();

        #region Http delegating handlers
        services.TryAddTransient<AcceptHeaderHandler>();
        services.TryAddTransient<UserAgentHeaderHandler>();
        #endregion

        services.AddHttpClient<IGravatarHttpClient, GravatarClient>()
            .AddHttpClientHandlers()
            .AddStandardResilienceHandler();
    }
    
    
    #region Helper

    static IHttpClientBuilder AddHttpClientHandlers(this IHttpClientBuilder builder)
    {
        return builder
            .AddHttpMessageHandler<AcceptHeaderHandler>()
            .AddHttpMessageHandler<UserAgentHeaderHandler>();
    }
    #endregion
}