namespace Sample.Core.Enums.Vehicles
{
    public class FuelType : Enumeration
    {
        public static FuelType Diesel = new FuelType(1, "DIESEL");
        public static FuelType Electric = new FuelType(2, "ELECTRIC");
        public static FuelType Gasoline = new FuelType(3, "GASOLINE");
        public static FuelType Flex = new FuelType(4, "FLEX");
        public static FuelType Hybrid = new FuelType(5, "HYBRID");
        public static FuelType Other = new FuelType(6, "OTHER");

        private FuelType(int value, string displayName) : base(value, displayName) { }
    }
}
