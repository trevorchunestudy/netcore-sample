using MediatR;
using Sample.Data;
using Sample.Data.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Web.Features.Owners
{
    public class Index
    {
        public class Query : PagingSort, IRequest<Result>
        {
        }

        public class Result
        {
            public Result(IPagedList<OwnerViewModel> results)
            {
                Results = results;
            }

            public IPagedList<OwnerViewModel> Results { get; private set; }
        }


        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly SampleContext _db;

            public Handler(SampleContext db)
            {
                _db = db;
            }

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                var querySetup = _db.Owners
                    .OwnerContains(query)
                    .SortOwnerBy(query)
                    .WhereActive(x => true)
                    .Select(e => new OwnerViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    });

                var pagedList = new PagedList<OwnerViewModel>(querySetup, query.Limit, query.Page);
                await pagedList.Initialization.ConfigureAwait(false);
                return new Result(pagedList);
            }
        }
    }
}
