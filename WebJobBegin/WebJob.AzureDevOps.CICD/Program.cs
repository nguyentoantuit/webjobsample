using System;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace WebJob.AzureDevOps.CICD
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddAzureStorage();
                b.AddTimers();
            });

            builder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();
                b.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                NLog.LogManager.LoadConfiguration($"nlog.config");
                string instrumentationKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
                b.AddApplicationInsights(o => o.InstrumentationKey = instrumentationKey);
            });


            builder.ConfigureAppConfiguration(c =>
            {
                c.SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables().Build();
            });

            builder.ConfigureServices((context, s) =>
            {
                s.Configure<QueuesOptions>(opt =>
                {
                    // Pool items from the queue every 15s
                    opt.MaxPollingInterval = TimeSpan.FromSeconds(15);
                    opt.BatchSize = 1;
                });
            });
            var host = builder.Build();
            using (host)
            {
                host.Run();
            }
        }
    }
}
