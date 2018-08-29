namespace Sample.Core.Enums.Vehicles
{
    public class Drivetrain : Enumeration
    {
        public static Drivetrain RearWheelDrive = new Drivetrain(1, "RWD");
        public static Drivetrain FrontWheelDrive = new Drivetrain(2, "FWD");
        public static Drivetrain AllWheelDrive = new Drivetrain(3, "AWD");
        public static Drivetrain FourByTwo = new Drivetrain(4, "4X2");
        public static Drivetrain FourByFour = new Drivetrain(5, "4X4");
        public static Drivetrain Other = new Drivetrain(6, "Other");

        private Drivetrain(int value, string displayName) : base(value, displayName) { }
    }
}
