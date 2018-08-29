namespace Sample.Core.Enums.Vehicles
{
    public class Transmission : Enumeration
    {
        public static Transmission Manual = new Transmission(1, "Manual");
        public static Transmission Automatic = new Transmission(2, "Automatic");

        private Transmission(int value, string displayName) : base(value, displayName) { }
    }
}
