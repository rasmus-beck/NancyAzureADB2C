using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;
using System;

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
