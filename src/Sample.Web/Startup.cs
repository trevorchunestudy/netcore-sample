using System;
using FluentValidation.AspNetCore;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Sample.Data;
using Sample.Web.Infrastructure.Background;
using Sample.Web.Infrastructure.Filters;
using Sample.Web.Infrastructure.Startup;

namespace Sample.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            Console.Out.WriteLine($"Current environment is: {env.EnvironmentName ?? "Default"}");
            AppSettings.SetEnviornmentalSettings(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSerilogServices(Configuration);
            services.AddCors();
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(DbContextTransactionFilter));
                opt.Filters.Add(typeof(ValidatorActionFilter));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); })
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                });

            services.AddScoped<ApiExceptionFilter>();
            services.AddMediatR(typeof(Startup));
            services.AddDbContext<SampleContext>(opt =>
            {
                opt.UseSqlServer(
                    Configuration.GetConnectionString(AppSettings.ConnectionStringName),
                    x => x.MigrationsAssembly("Sample.Data"));
            });

            //add mediatr support to separate assemblies
            //services.AddMediatR(typeof().GetTypeInfo().Assembly);
            var sp = services.BuildServiceProvider();
            var mediator = sp.GetService<IMediator>();

            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(Configuration.GetConnectionString(AppSettings.ConnectionStringName));
                x.UseActivator(new HangfireMediatrActivator(mediator));
                x.UseMediatR(mediator);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            //make sure this is called first in the pipeline!!!
            //add client side url's here
            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200",
                                    "http://localhost:55556")
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();

            //TODO: Secure Hangfire Dashboard. Hangfire on Redis?
            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
