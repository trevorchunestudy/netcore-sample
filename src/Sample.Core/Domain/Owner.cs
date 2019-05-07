using Sample.Core.Domain.Automotive;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sample.Core.Domain
{
    [Table("Owner")]
    public class Owner : Entity
    {
        private HashSet<Vehicle> _vehicles;

        //needed by EF
        private Owner() { }

        public Owner(string name)
        {
            Name = name;
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; private set; }


        public void Update(string name)
        {
            Name = name;
        }

        public IEnumerable<Vehicle> Vehicles => _vehicles?.ToList();

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new NullReferenceException("You must use .Include(x => x.Vehicles) before calling this method.");

            _vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new NullReferenceException("You must use .Include(x => x.Vehicles) before calling this method.");

            _vehicles.Remove(vehicle);
        }
    }
}
