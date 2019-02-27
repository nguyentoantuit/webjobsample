using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace WebJob.NetCore.BetaRelease
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

            config.LoggerFactory = new LoggerFactory().AddConsole();

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
    }
}
