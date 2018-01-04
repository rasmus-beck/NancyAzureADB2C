using Nancy;
using Nancy.Bootstrapper;
using Nancy.Owin;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;
using System;
using System.Linq;
using System.Security.Claims;

namespace NancyAzureADB2C
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(
                (c) =>
                {
                    c.ResponseProcessors.Clear();
                    c.ResponseProcessors.Add(typeof(JsonProcessor));
                });
            }
        }
    }
}
