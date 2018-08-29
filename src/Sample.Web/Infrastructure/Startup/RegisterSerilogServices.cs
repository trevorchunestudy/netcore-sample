using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Sample.Web.Infrastructure.Startup
{
    public static class RegisterSerilogServices
    {
        public static IServiceCollection AddSerilogServices(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Reving up the flux capicitor...");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Not enough gigawatts. 1.21 gigawatts required.");
            }

            return services.AddLogging(lb => lb.AddSerilog(dispose: true));
        }
    }
}
