using Microsoft.AspNetCore.Mvc.Filters;
using Sample.Data;
using System;
using System.Threading.Tasks;

namespace Sample.Web.Infrastructure.Filters
{
    public class DbContextTransactionFilter : IAsyncActionFilter
    {
        private readonly SampleContext _dbContext;

        public DbContextTransactionFilter(SampleContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                _dbContext.BeginTransaction();
                await next();
                await _dbContext.CommitTransactionAsync();
            }
            catch (Exception)
            {
                _dbContext.RollbackTransaction();
                throw;
            }
        }
    }
}
