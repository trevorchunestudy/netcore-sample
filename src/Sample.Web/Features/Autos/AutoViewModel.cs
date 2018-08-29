using Sample.Web.Infrastructure;

namespace Sample.Web.Features.Autos
{
    public class AutoViewModel : BaseViewModel
    { 
        public long OwnerId { get; set; }
        public string VehicleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public string BodyStyle { get; set; }
        public string DriveTrain { get; set; }
        public string Vin { get; set; }
        public string Availablity { get; set; }
        public int Mileage { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string ImageTag { get; set; }
        public string Condition { get; set; }
        public string Price { get; set; }
        public string Address { get; set; }
        public string ExteriorColor { get; set; }
        public string SalePrice { get; set; }
        public string StateOfVehicle { get; set; }
    }
}
