using MediatR;
using Sample.Data;
using Sample.Data.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Web.Features.Autos
{
    public class Index
    {
        public class Query : PagingSort, IRequest<Result>
        {
        }

        public class Result
        {
            public Result(IPagedList<AutoViewModel> results)
            {
                Results = results;
            }

            public IPagedList<AutoViewModel> Results { get; private set; }
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
                var querySetup = _db.Vehicles
                    .VehicleContains(query)
                    .SortVehicleBy(query)
                    .WhereActive(x => true)
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
                    });

                var pagedList = new PagedList<AutoViewModel>(querySetup, query.Limit, query.Page);
                await pagedList.Initialization.ConfigureAwait(false);
                return new Result(pagedList);
            }
        }
    }
}
