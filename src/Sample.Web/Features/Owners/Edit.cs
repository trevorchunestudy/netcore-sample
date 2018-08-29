using MediatR;
using Sample.Data;
using Sample.Data.Extensions;
using Sample.Web.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Web.Features.Owners
{
    public class Edit
    {
        public class Command : OwnerViewModel, IRequest
        {
        }

        public class Validator : BaseAbstractValidator<Command>
        {
            public Validator()
            {
                Include(new OwnerValidator());
                //add specific edit rules here
            }
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
                if (entity == null)
                    return;

                //update entity
                entity.Update(command.Name);

                //Filter executes SaveChangesAsync
            }
        }
    }
}
