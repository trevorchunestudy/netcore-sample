using FluentValidation;
using MediatR;
using Sample.Core.Domain;
using Sample.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Web.Features.Owners
{
    public class Create
    {
        public class Command : OwnerViewModel, IRequest<long>
        {
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                Include(new OwnerValidator());
            }
        }

        public class Handler : IRequestHandler<Command, long>
        {
            private readonly SampleContext _db;

            public Handler(SampleContext db)
            {
                _db = db;
            }

            public async Task<long> Handle(Command command, CancellationToken cancellationToken)
            {
                var owner = new Owner(command.Name);

                _db.Owners.Add(owner);
                _db.BeginTransaction();
                await _db.CommitTransactionAsync();

                return owner.Id;
            }
        }
    }
}
