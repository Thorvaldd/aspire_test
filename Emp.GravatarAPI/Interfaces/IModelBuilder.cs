namespace Emp.GravatarAPI.Interfaces;

public interface IModelBuilder
{
    Task<TResponse> ProcessResponse<TResponse>(HttpResponseMessage responseMessage,
        CancellationToken ct) where TResponse : class;
}