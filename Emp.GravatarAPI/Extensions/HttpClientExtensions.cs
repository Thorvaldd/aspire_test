using System.Diagnostics.CodeAnalysis;

namespace Emp.GravatarAPI.Extensions;

[ExcludeFromCodeCoverage(Justification = "Internal non testable code")]
internal static  class HttpClientExtensions
{
    public static HttpClient SetBaseAddress(this HttpClient client, string baseAddress)
    {
        client.BaseAddress = new Uri(baseAddress);
        return client;
    }
}