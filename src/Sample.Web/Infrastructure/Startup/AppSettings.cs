using Microsoft.Extensions.Configuration;

namespace Sample.Web.Infrastructure.Startup
{
    public static class AppSettings
    {
        public static string ConnectionStringName { get; private set; }
        public static string AppRoot { get; private set; }
        public static string Auth0Domain { get; private set; }
        public static string Auth0ClientId { get; private set; }


        public static void SetEnviornmentalSettings(IConfiguration configuration)
        {
            ConnectionStringName = "Default";
            AppRoot = configuration["General:AppAllowOrigin"];
            Auth0Domain = configuration["Auth0:Domain"];
            Auth0ClientId = configuration["Auth0:ClientId"];
        }
    }
}
