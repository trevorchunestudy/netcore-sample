namespace Sample.Core.ValueObjects
{
    public class Geometry : ValueObject<Geometry>
    {
        private Geometry() { }

        public Geometry(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public double Lat { get; private set; }
        public double Lng { get; private set; }
    }
}
