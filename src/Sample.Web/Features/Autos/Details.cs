using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.Data;
using Sample.Data.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Web.Features.Autos
{
    public class Details
    {
        public class Query : IRequest<AutoViewModel>
        {
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, AutoViewModel>
        {
            private readonly SampleContext _db;

            public Handler(SampleContext db)
            {
                _db = db;
            }

            public async Task<AutoViewModel> Handle(Query query, CancellationToken cancellationToken)
            {
                return await _db.Vehicles
                    .WhereActive(x => x.Id == query.Id)
                    .Select(e => new AutoViewModel
                    {
                        Id = e.Id,
                        OwnerId = e.OwnerId,
                        Title = e.Title,
                        Description = e.Description,
                        Make = e.Automobile.Make,
                        Model = e.Automobile.Model,
                        Year = e.Automobile.Year,
                        Transmission = e.Automobile.Transmission,
                        FuelType = e.Automobile.FuelType,
                        BodyStyle = e.Automobile.BodyStyle,
                        DriveTrain = e.Automobile.DriveTrain,
                        Vin = e.Automobile.Vin,
                        Availablity = e.Automobile.Availablity,
                        Mileage = e.Mileage.Value,
                        Url = e.Url,
                        ImageUrl = e.ImageUrl,
                        Condition = e.Condition,
                        Price = e.Price,
                        ExteriorColor = e.ExteriorColor,
                        SalePrice = e.SalePrice,
                        StateOfVehicle = e.StateOfVehicle
                    })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}
