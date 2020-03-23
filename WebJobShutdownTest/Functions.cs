using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace WebJobShutdownTest
{
    public class Functions
    {
        

        [NoAutomaticTrigger]
        public static async Task ProcessAQueueAsync([QueueTrigger("message")] string message, TextWriter log)
        {
            ThreadTest tt = new ThreadTest();

            Thread InstanceCaller = new Thread(
            new ThreadStart(ThreadTest.Test));

            // Start the thread.
            InstanceCaller.Start();
        }
    }

    public class ThreadTest
    {
        public static void Test()
        {
            Console.WriteLine("Starting new thread");
            
            //sleep for 2 minutes
            System.Threading.Thread.Sleep(120000);
        }
    }
}
