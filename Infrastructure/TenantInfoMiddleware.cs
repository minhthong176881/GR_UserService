using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Infrastructure
{
    public class TenantInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenantInfo = context.RequestServices.GetRequiredService<TenantInfo>();
            var tenantName = context.Request.Headers["Tenant"];

            tenantInfo.Name = tenantName;

            await _next(context);
        }
    }
}