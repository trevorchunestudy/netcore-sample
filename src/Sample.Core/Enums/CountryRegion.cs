namespace Sample.Core.Enums
{
    public class CountryRegion : Enumeration
    {
        public static readonly CountryRegion US = new CountryRegion(1, "United States");

        private CountryRegion(int value, string displayName) : base(value, displayName)
        {

        }

        public static readonly CountryRegion[] ReadAll = { US };
    }
}
