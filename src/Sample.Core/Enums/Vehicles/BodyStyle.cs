namespace Sample.Core.Enums.Vehicles
{
    public class BodyStyle : Enumeration
    {

        public static BodyStyle Convertible = new BodyStyle(1, "CONVERTIBLE");
        public static BodyStyle Coupe = new BodyStyle(2, "COUPE");
        public static BodyStyle Crossover = new BodyStyle(3, "CROSSOVER");
        public static BodyStyle Hatchback = new BodyStyle(4, "HATCHBACK");
        public static BodyStyle MiniVan = new BodyStyle(5, "MINIVAN");
        public static BodyStyle Truck = new BodyStyle(6, "TRUCK");
        public static BodyStyle SUV = new BodyStyle(7, "SUV");
        public static BodyStyle Sedan = new BodyStyle(8, "SEDAN");
        public static BodyStyle Van = new BodyStyle(9, "VAN");
        public static BodyStyle Wagon = new BodyStyle(10, "WAGON");
        public static BodyStyle Other = new BodyStyle(11, "OTHER");

        private BodyStyle(int value, string displayName) : base(value, displayName) { }

    }
}
