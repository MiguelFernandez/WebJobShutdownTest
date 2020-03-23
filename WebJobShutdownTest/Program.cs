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
       
        static void Main()
        {
            var config = new JobHostConfiguration();
            
            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
                Console.WriteLine("Using Dev Settings");
            }
            var host = new JobHost(config);
            var shutdownWatcher = new WebJobsShutdownWatcher();
            shutdownWatcher.Token.Register(() => ShutdownRequested());
            host.CallAsync(typeof(Functions).GetMethod("ProcessAQueueAsync"), new { message = "hello" } );         
            host.RunAndBlock();           
        }
        private static void ShutdownRequested()
        {
            Console.WriteLine("Shutdown requested at " + DateTime.Now.ToLongTimeString());           
        }
    }


}
