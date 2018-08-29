using MediatR;
using Sample.Data;
using Sample.Data.Extensions;
using Sample.Web.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Web.Features.Autos
{
    public class Edit
    {
        public class Command : AutoViewModel, IRequest
        {
        }

        public class Validator : BaseAbstractValidator<Command>
        {
            public Validator()
            {
                Include(new AutoValidator());
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
                var entity = await _db.Vehicles
                    .FirstActiveAsync(x => x.Id == command.Id)
                    .ConfigureAwait(false);
                if (entity == null)
                    return;

                //update entity
                entity.Update(command.Title);

                //Filter executes SaveChangesAsync
            }
        }
    }
}
