using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace WebJobShutdownTest
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {

            //var storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=mifernawebjobs;AccountKey=cRyeKJakEL+K83svRS7yv9T39osizgnCGJxg8NX/j/+zytz0GGkq7jash1Fu5clRokQT5Uu91pfTXIcC1UrRaQ==;EndpointSuffix=core.windows.net";
            //var config = new JobHostConfiguration(storageConnectionString);
            var config = new JobHostConfiguration();
            
            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
                Console.WriteLine("Using Dev Settings");
            }

            var host = new JobHost(config);
            var shutdownWatcher = new WebJobsShutdownWatcher();
            shutdownWatcher.Token.Register(() => ShutdownRequested());



            // The following code ensures that the WebJob will be running continuously
          //  host.Start();
            host.CallAsync(typeof(Functions).GetMethod("ProcessAQueueAsync"), new { message = "hello" } );
            // Console.WriteLine("starting to sleep...");
            //  System.Threading.Thread.Sleep(5000);
            host.RunAndBlock();
           
        }
        private static void ShutdownRequested()
        {
            Console.WriteLine("Shutdown requested at " + DateTime.Now.ToLongTimeString());
           
        }
    }


}
