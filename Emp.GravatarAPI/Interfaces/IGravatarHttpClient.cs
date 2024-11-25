namespace Emp.GravatarAPI.Interfaces;

public interface IGravatarHttpClient
{
    Task<Dictionary<string,string>> GetProfileAsync(string email,
        CancellationToken ct = default);
}