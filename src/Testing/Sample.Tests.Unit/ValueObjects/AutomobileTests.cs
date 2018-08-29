using Sample.Core.Enums.Vehicles;
using Sample.Core.Extensions;
using Sample.Core.ValueObjects.Vehicles;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Sample.Tests.Unit.ValueObjects
{
    public class AutomobileTests
    {
        private const string VEHICLE_ID = "abc123";
        private const string MAKE = "Ford";
        private const string MODEL = "F150";
        private const int YEAR = 2004;
        private const string TRANSMISSION = "Automatic";
        private const string FUEL_TYPE = "GASOLINE";
        private const string BODY_STYLE = "TRUCK";
        private const string DRIVETRAIN = "4X2";
        private const string VIN = "abc123";

        [Fact]
        public void Should_return_null_when_vehicleId_is_null()
        {
            var auto = Automobile.Create(null, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_vehicleId_is_empty()
        {
            var auto = Automobile.Create("", MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_make_is_null()
        {
            var auto = Automobile.Create(VEHICLE_ID, null, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_make_is_empty()
        {
            var auto = Automobile.Create(VEHICLE_ID, "", MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_model_is_null()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, null, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_model_is_empty()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, "", YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_year_is_zero()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, 0, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_transmission_is_null()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, null, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_transmission_is_empty()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, "", FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_fuelType_is_null()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, null, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_fuelType_is_empty()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, "", BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_bodyStyle_is_null()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, null, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_bodyStyle_is_empty()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, "", DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_driveTrain_is_null()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, null, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_driveTrain_is_empty()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, "", VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_vin_is_null()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, null);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_vin_is_empty()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, "");
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_transmission_does_not_match()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, "foobar", FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_fuelType_does_not_match()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, "foobar", BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_when_bodyStyle_does_not_match()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, "foobar", DRIVETRAIN, VIN);
            auto.ShouldBeNull();
        }

        [Fact]
        public void Should_return_Default_Other_when_driveTrain_does_not_match()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, "foobar", VIN);
            auto.ShouldNotBeNull();
            auto.DriveTrain.ShouldBe(Drivetrain.Other.DisplayName);
        }

        [Fact]
        public void Should_return_Automobile_when_in_correct_format()
        {
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldNotBeNull();
            auto.VehicleId.ShouldBe(VEHICLE_ID);
            auto.Make.ShouldBe(MAKE);
            auto.Model.ShouldBe(MODEL);
            auto.Year.ShouldBe(YEAR);
            auto.Transmission.ShouldBe(TRANSMISSION);
            auto.FuelType.ShouldBe(FUEL_TYPE);
            auto.BodyStyle.ShouldBe(BODY_STYLE);
            auto.DriveTrain.ShouldBe(DRIVETRAIN);
            auto.Vin.ShouldBe(VIN);
            auto.Availablity.ShouldBe("AVAILABLE");
        }

        [Fact]
        public void Should_truncate_VehicleId_when_too_long()
        {
            var random = new Random();
            var vehicleId = TestHelpers.RandomStrings(101, 101, 1, random).First();
            var auto = Automobile.Create(vehicleId, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldNotBeNull();
            auto.VehicleId.ShouldBe(vehicleId.Truncate(100));
        }

        [Fact]
        public void Should_truncate_Make_when_too_long()
        {
            var random = new Random();
            var make = TestHelpers.RandomStrings(26, 26, 1, random).First();
            var auto = Automobile.Create(VEHICLE_ID, make, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldNotBeNull();
            auto.Make.ShouldBe(make.Truncate(25));
        }

        [Fact]
        public void Should_truncate_Model_when_too_long()
        {
            var random = new Random();
            var model = TestHelpers.RandomStrings(26, 26, 1, random).First();
            var auto = Automobile.Create(VEHICLE_ID, MAKE, model, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, VIN);
            auto.ShouldNotBeNull();
            auto.Model.ShouldBe(model.Truncate(25));
        }

        [Fact]
        public void Should_truncate_Vin_when_too_long()
        {
            var random = new Random();
            var vin = TestHelpers.RandomStrings(18, 18, 1, random).First();
            var auto = Automobile.Create(VEHICLE_ID, MAKE, MODEL, YEAR, TRANSMISSION, FUEL_TYPE, BODY_STYLE, DRIVETRAIN, vin);
            auto.ShouldNotBeNull();
            auto.Vin.ShouldBe(vin.Truncate(17));
        }
    }
}
