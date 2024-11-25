using System.Net;
using System.Text;
using System.Text.Json;
using Emp.GravatarAPI.Exceptions;
using Emp.GravatarAPI.Interfaces;

namespace Emp.GravatarAPI.Implementations;

public class ModelBuilder : IModelBuilder
{
    public async Task<TResponse> ProcessResponse<TResponse>(HttpResponseMessage responseMessage,
        CancellationToken ct) where TResponse : class
    {
        HttpStatusCode responseStatusCode = responseMessage.StatusCode;
        
        await using Stream responseStream = await responseMessage.Content.ReadAsStreamAsync(ct);
      

        switch (responseStatusCode)
        {
            // most common 400 errors:
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.NotFound:
            case HttpStatusCode.RequestEntityTooLarge:
            case HttpStatusCode.UnsupportedMediaType:
            case HttpStatusCode.RequestUriTooLong:
            {
                using StreamReader reader = new (responseStream, new UTF8Encoding(false, true));
                string responseBody = await reader.ReadToEndAsync(ct);
                throw new GravatarException(responseBody, responseStatusCode);
            }
            
            // 400 errors
            case HttpStatusCode.Unauthorized:
            case HttpStatusCode.Forbidden:
            case HttpStatusCode.RequestTimeout:
            case HttpStatusCode.TooManyRequests:
            // 500 codes
            case HttpStatusCode.InternalServerError:
            case HttpStatusCode.BadGateway:
            case HttpStatusCode.NotImplemented:
            {
                using StreamReader reader = new (responseStream, new UTF8Encoding(false, true));
                string responseBody = await reader.ReadToEndAsync(ct);
                throw new GravatarInternalException(responseBody, responseStatusCode);
            }
            
            // 200 codes
            case HttpStatusCode.OK:
            case HttpStatusCode.Created:
            default:
            {
                TResponse result = await JsonSerializer
                    .DeserializeAsync<TResponse>(responseStream);
                
                return result;
            }
        }
    }
}