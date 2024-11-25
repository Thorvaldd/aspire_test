using System.Net.Http.Json;
using Emp.GravatarAPI.Exceptions;
using Emp.GravatarAPI.Extensions;
using Emp.GravatarAPI.Interfaces;
using Emp.GravatarAPI.Models;
using Microsoft.Extensions.Logging;

namespace Emp.GravatarAPI.Implementations.Clients;

public class GravatarClient : BaseClient, IGravatarHttpClient
{
    #region Fields

    private readonly IModelBuilder _modelBuilder;
    private readonly ILogger<GravatarClient> _logger;
    
    public GravatarClient(HttpClient httpClient,
        IModelBuilder modelBuilder,
        ILogger<GravatarClient> logger)
        : base(httpClient)
    {
        _modelBuilder = modelBuilder;
        _logger = logger;
    }
    #endregion

    public async Task<Dictionary<string, string>> GetProfileAsync(string email,
        CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new GravatarException("Email is required");
        }
        ConfigureHttpClient();
        
        // Base address added / in the end
        HttpResponseMessage responseMessage = await HttpClient.GetAsync($"{email.ToMd5()}.json", ct);

        GravatarResponse response = await _modelBuilder.ProcessResponse<GravatarResponse>(responseMessage, ct);
        
        var entry = response.Entry.FirstOrDefault();
        if(entry == null) return new Dictionary<string, string>{{"", ""}};

        string name = entry.Name?.Formatted ?? (entry.DisplayName ?? "");
        string photo = entry.Photos != null && entry.Photos.Any() && !string.IsNullOrEmpty(entry.Photos[0].Value)
            ? entry.Photos[0].Value
            : "";
            return new Dictionary<string, string>() { {name, photo} };

    }
}