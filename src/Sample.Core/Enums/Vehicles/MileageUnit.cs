namespace Sample.Core.Enums.Vehicles
{
    public class MileageUnit : Enumeration
    {
        public static readonly MileageUnit MI = new MileageUnit(1, "MI");
        public static readonly MileageUnit KM = new MileageUnit(1, "KM");

        private MileageUnit(int value, string displayName) : base(value, displayName)
        {

        }
    }
}
