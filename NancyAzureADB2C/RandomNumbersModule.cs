using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace NancyAzureADB2C
{
    public class RandomNumbersModule : NancyModule
    {
        public RandomNumbersModule()
        {
            this.RequiresAspNetCoreAuthentication();
            Get("/numbers", _ => { return GetListOfRandomNumbers(); });
        }

        private IList<int> GetListOfRandomNumbers()
        {
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
