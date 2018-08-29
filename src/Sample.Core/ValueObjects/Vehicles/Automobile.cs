using Sample.Core.Enums;
using Sample.Core.Enums.Vehicles;
using Sample.Core.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Sample.Core.ValueObjects.Vehicles
{
    public class Automobile : ValueObject<Automobile>
    {
        private Automobile() { }

        private Automobile(string vehicleId, string make, string model, int year,
            string transmission, string fuelType, string bodyStyle, string driveTrain, string vin)
        {
            VehicleId = vehicleId.Truncate(100);
            Make = make.Truncate(25);
            Model = model.Truncate(25);
            Year = year;
            Transmission = transmission;
            FuelType = fuelType;
            BodyStyle = bodyStyle;
            DriveTrain = driveTrain;
            Vin = vin.Truncate(17);
            Availablity = "AVAILABLE";
        }

        [MaxLength(100)]
        public string VehicleId { get; private set; }

        [MaxLength(25)]
        public string Make { get; private set; }

        [MaxLength(25)]
        public string Model { get; private set; }
        public int Year { get; private set; }

        [MaxLength(10)]
        public string Transmission { get; private set; }

        [MaxLength(10)]
        public string FuelType { get; private set; }

        [MaxLength(12)]
        public string BodyStyle { get; private set; }

        [MaxLength(10)]
        public string DriveTrain { get; private set; }

        [MaxLength(17)]
        public string Vin { get; private set; }

        [MaxLength(12)]
        public string Availablity { get; private set; }

        public static Drivetrain SetDrivetrain(string driveTrain)
        {
            try
            {
                return Enumeration.FromDisplayName<Drivetrain>(driveTrain);
            }
            catch
            {
                return Drivetrain.Other;
            }
        }

        public static BodyStyle SetBodyStyle(string bodyStyle)
        {
            try
            {
                return Enumeration.FromDisplayName<BodyStyle>(bodyStyle);
            }
            catch
            {
                return EnumDisplayNameParser.BodyStyleFromName(bodyStyle);
            }
        }

        public static Transmission SetTransmission(string transmission)
        {
            try
            {
                return Enumeration.FromDisplayName<Transmission>(transmission);
            }
            catch
            {
                return EnumDisplayNameParser.TransmissionFromName(transmission);
            }
        }

        public static FuelType SetFuelType(string fuelType)
        {
            try
            {
                return Enumeration.FromDisplayName<FuelType>(fuelType);
            }
            catch
            {
                return EnumDisplayNameParser.FuelTypeFromName(fuelType);
            }
        }

        public static Automobile Create(string vehicleId, string make, string model, int year,
            string transmission, string fuelType, string bodyStyle, string driveTrain, string vin)
        {
            if (string.IsNullOrEmpty(vehicleId) ||
                string.IsNullOrEmpty(make) ||
                string.IsNullOrEmpty(model) ||
                year == 0 ||
               string.IsNullOrEmpty(transmission) ||
               string.IsNullOrEmpty(fuelType) ||
               string.IsNullOrEmpty(bodyStyle) ||
               string.IsNullOrEmpty(driveTrain) ||
                string.IsNullOrEmpty(vin))
                return null;

            var dt = SetDrivetrain(driveTrain);
            var bs = SetBodyStyle(bodyStyle);
            var trans = SetTransmission(transmission);
            var ft = SetFuelType(fuelType);

            if (dt == null || bs == null || trans == null || ft == null)
                return null;

            return new Automobile(vehicleId, make, model, year,
                   trans.DisplayName,
                   ft.DisplayName,
                   bs.DisplayName,
                   dt.DisplayName,
                   vin);
        }
    }
}
