using Microsoft.AspNetCore.Authentication;
using Nancy;
using Nancy.Security;

namespace NancyAzureADB2C
{
    public static class NancyModuleExtensions
    {
        public static void RequiresAspNetCoreAuthentication(this INancyModule module)
        {
            module.Before.AddItemToStartOfPipeline(async (ctx, ct) =>
            {
                var httpContext = ctx.GetHttpContext();

                if (httpContext.User.IsAuthenticated())
                {
                    return null;
                }

                await httpContext.ChallengeAsync(new AuthenticationProperties { RedirectUri = "/" });

                return HttpStatusCode.Unauthorized;
            });
        }
    }
}
