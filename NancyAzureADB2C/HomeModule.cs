namespace NancyAzureADB2C
{
    public class HomeModule : Nancy.NancyModule
    {
        public HomeModule()
        {
            Get("/", _ => { return new { Greeting = "Hello and welcome to the Random Numbers API." }; });
        }
    }
}
