namespace Sample.Core.Enums.Vehicles
{
    public class StateOfVehicle : Enumeration
    {
        public static StateOfVehicle New = new StateOfVehicle(1, "NEW");
        public static StateOfVehicle Used = new StateOfVehicle(2, "USED");
        public static StateOfVehicle CertifiedPreOwned = new StateOfVehicle(3, "CPO");

        private StateOfVehicle(int value, string displayName) : base(value, displayName) { }
    }
}
