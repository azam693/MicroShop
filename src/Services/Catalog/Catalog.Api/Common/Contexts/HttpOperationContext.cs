using Kernel.Contexts;

namespace Catalog.Api.Common.Contexts;

public class HttpOperationContext : IOperationContext
{
    public HttpOperationContext(IHttpContextAccessor httpContextAccessor)
    {
        UserId = Guid.NewGuid();
    }
    
    public Guid UserId { get; }
}