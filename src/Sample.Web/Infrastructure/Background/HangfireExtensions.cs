using Hangfire;
using Hangfire.Common;
using MediatR;
using Newtonsoft.Json;

namespace Sample.Web.Infrastructure.Background
{
    public static class HangfireExtensions
    {
        public static IGlobalConfiguration UseMediatR(this IGlobalConfiguration config, IMediator mediator)
        {
            config.UseActivator(new HangfireMediatrActivator(mediator));

            JobHelper.SetSerializerSettings(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return config;
        }
    }
}
