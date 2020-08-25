using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Application.Setup
{
    public class Application_Automation
    {
        static TestConfiguration _configuration = null;

        public static string SampleDirectory
        {
            get
            {
                return TestContext.CurrentContext.TestDirectory + @"\Samples\";
            }
        }

        public static TestConfiguration GetApplicationConfiguration()
        {
            if (_configuration == null)
            {
                TestContext context = TestContext.CurrentContext;
                _configuration = new TestConfiguration();

                var builder = new ConfigurationBuilder()
                    .SetBasePath(context.TestDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddUserSecrets<Application_Automation>()
                    .Build();

                builder
                    .GetSection("Automation")
                    .Bind(_configuration);
            }

            return _configuration;
        }
    }
}
