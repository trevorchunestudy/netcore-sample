using Sample.Core.Enums.Vehicles;

namespace Sample.Core.Enums
{
    public static class EnumDisplayNameParser
    {
        public static BodyStyle BodyStyleFromName(string displayName)
        {
            switch (displayName)
            {
                case "4dr Car":
                    return BodyStyle.Sedan;
                case "Hatchback":
                    return BodyStyle.Hatchback;
                case "Sport Utility":
                    return BodyStyle.SUV;
                case "Convertible":
                    return BodyStyle.Convertible;
                case "Mini-van, Passenger":
                    return BodyStyle.MiniVan;

                default:
                    return null;
            }
        }

        public static FuelType FuelTypeFromName(string displayName)
        {
            switch (displayName)
            {
                case "Gasoline Fuel":
                    return FuelType.Gasoline;
                case "Hybrid Fuel":
                    return FuelType.Hybrid;
                case "Flex Fuel":
                    return FuelType.Flex;
                default:
                    return null;
            }
        }

        public static Transmission TransmissionFromName(string displayName)
        {
            switch (displayName)
            {
                case "Manual":
                    return Transmission.Manual;
                case "Automatic":
                    return Transmission.Automatic;
                default:
                    return null;
            }
        }

        public static StateOfVehicle StateOfVehicleFromName(string displayName)
        {
            switch (displayName)
            {
                case "New":
                    return StateOfVehicle.New;
                case "Used":
                    return StateOfVehicle.Used;
                case "Cpo":
                case "Certified Pre-Owned":
                case "certified pre-owned":
                    return StateOfVehicle.CertifiedPreOwned;
                default:
                    return null;
            }

        }
    }
}
