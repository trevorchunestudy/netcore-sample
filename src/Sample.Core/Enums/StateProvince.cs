namespace Sample.Core.Enums
{
    public class StateProvince : Enumeration
    {
        public static readonly StateProvince Oregon = new StateProvince(38, "Oregon");

        private StateProvince(int value, string displayName) : base(value, displayName)
        {

        }
    }
}
