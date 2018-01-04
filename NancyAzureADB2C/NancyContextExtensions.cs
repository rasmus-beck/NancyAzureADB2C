using Microsoft.AspNetCore.Http;
using Nancy;
using Nancy.Owin;

namespace NancyAzureADB2C
{
    public static class NancyContextExtensions
    {
        public static HttpContext GetHttpContext(this NancyContext context)
        {
            var key = typeof(HttpContext).FullName;

            var environment = context.GetOwinEnvironment();

            if (environment.TryGetValue(key, out var httpContext))
            {
                return httpContext as HttpContext;
            }

            return null;
        }
    }
}
