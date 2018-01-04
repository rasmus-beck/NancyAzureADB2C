using Nancy;
using Nancy.ErrorHandling;
using System;

namespace NancyAzureADB2C
{
    public class StatusCodeHandler : IStatusCodeHandler
    {
        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            Console.WriteLine(statusCode);
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return true;
        }
    }
}
