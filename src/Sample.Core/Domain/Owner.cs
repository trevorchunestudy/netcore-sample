using Sample.Core.Domain.Automotive;
using Sample.Core.ValueObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.Core.Domain
{
    public class Owner : Entity
    {
        //needed by EF
        private Owner() { }

        public Owner(string name)
        {
            Name = name;
        }


        public Owner(string name, Geometry geometry)
        {
            Name = name;
            Geometry = geometry;
        }

        [MaxLength(100)]
        public string Name { get; private set; }

        public Geometry Geometry { get; private set; }

        public void UpdateGeometry(Geometry geometry)
        {
            var lat = geometry.Lat;
            var lng = geometry.Lng;
            Geometry = new Geometry(lat, lng);
        }

        public void UpdateLocation(double lat, double lng)
        {
            Geometry = new Geometry(lat, lng);
        }

        public void Update(string name)
        {
            Name = name;
        }

        //TODO: make read only
        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}
