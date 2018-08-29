using MediatR;
using Sample.Data;
using Sample.Data.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Web.Features.Owners
{
    public class Delete
    {
        public class Command : IRequest
        {
            public long Id { get; set; }
        }

        public class CommandHandler : AsyncRequestHandler<Command>
        {
            private readonly SampleContext _db;

            public CommandHandler(SampleContext db)
            {
                _db = db;
            }

            protected override async Task Handle(Command command, CancellationToken cancellationToken)
            {
                var entity = await _db.Owners
                    .FirstActiveAsync(x => x.Id == command.Id)
                    .ConfigureAwait(false);

                entity?.SoftDelete();
            }
        }
    }
}
