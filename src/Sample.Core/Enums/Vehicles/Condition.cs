namespace Sample.Core.Enums.Vehicles
{
    public class Condition : Enumeration
    {
        public static Condition Excellent = new Condition(1, "EXCELLENT");
        public static Condition Good = new Condition(2, "GOOD");
        public static Condition Fair = new Condition(3, "FAIR");
        public static Condition Poor = new Condition(4, "POOR");
        public static Condition Other = new Condition(5, "OTHER");

        private Condition(int value, string displayName) : base(value, displayName) { }
    }
}
