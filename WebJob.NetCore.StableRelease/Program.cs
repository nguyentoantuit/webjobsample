using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebJob.NetCore.StableRelease
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = new JobHostConfiguration();

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            IConfigurationRoot configurationRoot = BuildConfiguration();
            config.DashboardConnectionString = configurationRoot.GetSection("ConnectionStrings")["AzureWebJobsDashboard"];
            config.StorageConnectionString = configurationRoot.GetSection("ConnectionStrings")["AzureWebJobsStorage"];

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configurationRoot = builder.Build();
            return configurationRoot;
        }
    }
}
