using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Sample.Core.Domain;
using Sample.Data;
using Sample.Web;
using Sample.Web.Infrastructure.Startup;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Sample.Tests.Integration
{
    public class SliceFixture
    {
        private static readonly Checkpoint _checkpoint;
        private static readonly IConfiguration _configuration;
        private static readonly IServiceScopeFactory _scopeFactory;

        static SliceFixture()
        {
            var host = A.Fake<IHostingEnvironment>();
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var dir = Path.GetDirectoryName(uri.Path);
            if (File.Exists(dir + "\\appsettings.Development.json"))
                host.EnvironmentName = "Development";

            A.CallTo(() => host.ContentRootPath).Returns(Directory.GetCurrentDirectory());

            var startup = new Startup(host);
            _configuration = startup.Configuration;
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            _scopeFactory = provider.GetService<IServiceScopeFactory>();
            _checkpoint = new Checkpoint { TablesToIgnore = new[] { "__EFMigrationsHistory" } };
        }

        public static Task ResetCheckpoint() => _checkpoint.Reset(_configuration.GetConnectionString(AppSettings.ConnectionStringName));

        public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<SampleContext>();

                try
                {
                    dbContext.BeginTransaction();

                    await action(scope.ServiceProvider);

                    await dbContext.CommitTransactionAsync();
                }
                catch (Exception)
                {
                    dbContext.RollbackTransaction();
                    throw;
                }
            }
        }

        public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<SampleContext>();

                try
                {
                    dbContext.BeginTransaction();

                    var result = await action(scope.ServiceProvider);

                    await dbContext.CommitTransactionAsync();

                    return result;
                }
                catch (Exception)
                {
                    dbContext.RollbackTransaction();
                    throw;
                }
            }
        }

        public static Task ExecuteDbContextAsync(Func<SampleContext, Task> action)
        {
            return ExecuteScopeAsync(sp => action(sp.GetService<SampleContext>()));
        }

        public static Task<T> ExecuteDbContextAsync<T>(Func<SampleContext, Task<T>> action)
        {
            return ExecuteScopeAsync(sp => action(sp.GetService<SampleContext>()));
        }

        public static Task InsertAsync<T>(params T[] entities) where T : Entity
        {
            return ExecuteDbContextAsync(async ctx =>
            {
                ctx.Set<T>().AddRange(entities);
                await ctx.SaveChangesAsync();
            });
        }

        public static Task UpdateAsync<T>(params T[] entities) where T : Entity
        {
            return ExecuteDbContextAsync(async ctx =>
            {
                foreach (var updated in entities)
                {
                    ctx.Set<T>().Attach(updated);
                    ctx.Entry(updated).State = EntityState.Modified;
                }
                await ctx.SaveChangesAsync();
            });
        }

        public static Task<T> FindAsync<T>(long id)
            where T : class, IEntity
        {
            return ExecuteDbContextAsync(db => db.Set<T>().FindAsync(id));
        }

        public static Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();

                return mediator.Send(request);
            });
        }

        public static Task SendAsync(IRequest request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();

                return mediator.Send(request);
            });
        }

        /// <summary>
        /// Build a  query to include any child collections
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="dbSet">IDbSet of type T</param>
        /// <param name="includeExpressions">Lambda expression for what child collections to include</param>
        /// <returns>IQueryable T</returns>
        /// <remarks>http://appetere.com/post/passing-include-statements-into-a-repository</remarks>
        private static IQueryable<T> IncludeAll<T>(IQueryable<T> dbSet, params Expression<Func<T, object>>[] includeExpressions)
            where T : Entity
        {
            foreach (var includeExpr in includeExpressions)
            {
                dbSet = dbSet.Include(includeExpr);
            }

            return dbSet;
        }
    }
}
