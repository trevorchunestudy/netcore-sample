using FluentValidation;
using MediatR;
using Sample.Core.Domain.Automotive;
using Sample.Core.ValueObjects.Vehicles;
using Sample.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Web.Features.Autos
{
    public class Create
    {
        public class Command : AutoViewModel, IRequest<long>
        {
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                Include(new AutoValidator());
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
                var auto = Automobile.Create(command.VehicleId, command.Make, command.Model, command.Year, command.Transmission, command.FuelType,
                            command.BodyStyle, command.DriveTrain, command.Vin);

  
                var vehicle = Vehicle.Create(command.OwnerId, auto, command.Title, command.Description,
                    command.Mileage, "MI", command.Url, command.ImageUrl, "foo",
                    command.Condition, command.Price, command.Address, command.ExteriorColor, command.SalePrice,
                    command.StateOfVehicle, 0, 0);

                _db.Vehicles.Add(vehicle);
                _db.BeginTransaction();
                await _db.CommitTransactionAsync();

                return vehicle.Id;
            }
        }
    }
}
