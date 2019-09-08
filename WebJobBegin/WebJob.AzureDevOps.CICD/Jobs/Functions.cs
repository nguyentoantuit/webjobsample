using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace WebJob.AzureDevOps.CICD.Jobs
{
    public static class Functions
    {
        public static void TriggerByQueueMessage([QueueTrigger("sample-test-queue")] string message, ILogger logger)
        {
            logger.LogInformation($"Version 1: sample-test-queue message: {message}");
        }
    }
}
