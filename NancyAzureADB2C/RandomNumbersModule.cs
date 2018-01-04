using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Owin;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NancyAzureADB2C
{
    public class RandomNumbersModule : NancyModule
    {
        public RandomNumbersModule()
        {
            //this.RequiresAuthentication();
            Get("/numbers", async _ => { return await GetListOfRandomNumbers(); });
        }

        private async Task<IList<int>> GetListOfRandomNumbers()
        {
            var ctxKeyPair = Context.GetOwinEnvironment().FirstOrDefault(x => x.Key == typeof(HttpContext).FullName);
            var httpContext = ctxKeyPair.Value as HttpContext;
            await httpContext.Authentication.ChallengeAsync(new AuthenticationProperties { RedirectUri = "/" });
            
            var result = new HashSet<int>();
            var random = new Random();
            var seq = Enumerable.Range(1, 30).GetEnumerator();
            while (seq.MoveNext())
            {
                while (!result.Add(random.Next(1, 100000))) ;
            }

            return result.ToList();
        }
    }
}
