using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TransactionImporter.Rest
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host
            .CreateDefaultBuilder(args)
            .ConfigureLogging((hostContext, loggingBuilder) =>
            {
                loggingBuilder.AddConfiguration(hostContext.Configuration.GetSection("Logging"));

                if (hostContext.HostingEnvironment.IsDevelopment())
                    loggingBuilder.AddConsole(); // log to file functionality could be added here
            })
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
