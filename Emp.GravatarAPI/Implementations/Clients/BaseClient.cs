using Emp.GravatarAPI.Extensions;

namespace Emp.GravatarAPI.Implementations.Clients;

public abstract class BaseClient : IDisposable
{
    protected readonly HttpClient HttpClient;

    // private readonly IGravatarCredentialsConfigurationManager _credentialsManager;

    public BaseClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    protected void ConfigureHttpClient()
    {
        if (HttpClient.BaseAddress == null)
        {
            // Get credentials for example from AWS secret manager
            HttpClient.SetBaseAddress("https://en.gravatar.com/");
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        HttpClient?.Dispose();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}