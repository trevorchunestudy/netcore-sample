using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.Data;
using Sample.Data.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Web.Features.Owners
{
    public class Details
    {
        public class Query : IRequest<OwnerViewModel>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, OwnerViewModel>
        {
            private readonly SampleContext _db;

            public Handler(SampleContext db)
            {
                _db = db;
            }

            public async Task<OwnerViewModel> Handle(Query query, CancellationToken cancellationToken)
            {
                return await _db.Owners
                    .WhereActive(x => x.Id == query.Id)
                    .Select(e => new OwnerViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}
