using Kernel.Contexts;
using Microsoft.AspNetCore.Http;

namespace Kernel.AspNet.Contexts;

public class HttpOperationContext : IOperationContext
{
    public HttpOperationContext(IHttpContextAccessor httpContextAccessor)
    {
        UserId = Guid.NewGuid();
    }
    
    public Guid UserId { get; }
}