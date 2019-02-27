using Microsoft.Azure.WebJobs;
using System;
using WebJob.NetCore.BetaRelease.Models;

namespace WebJob.NetCore.BetaRelease
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called webjobsdkbeta.
        public static void ProcessQueueMessage([QueueTrigger("webjobsdkbeta")] string message)
        {
            Console.WriteLine(message);
        }

        public static void ProcessQueueSimpleMessage([QueueTrigger("simplemessage")] SimpleMessage message)
        {
            Console.WriteLine($"Message Name: {message.Name}, message value: {message.Value}");
        }
    }
}
