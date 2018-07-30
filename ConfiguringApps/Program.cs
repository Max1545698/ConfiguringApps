using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConfiguringApps
{

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return new WebHostBuilder()
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  config.AddJsonFile("appsettings.json",
                      optional: true, // exception will not be thrown if the file doesn't exist 
                      reloadOnChange: true); // config data refreshed automatically if there is some changes 
                  config.AddEnvironmentVariables();
                  if (args != null)
                  {
                      config.AddCommandLine(args);
                  }
              })
              .ConfigureLogging((hostingContext, logging) =>
              {
                  logging.AddConfiguration(
                      hostingContext.Configuration.GetSection("Logging"));
                  logging.AddConsole();
                  logging.AddDebug();
              })
              .UseIISIntegration()
              .UseStartup<Startup>()
              .Build();
        }
    }
}
