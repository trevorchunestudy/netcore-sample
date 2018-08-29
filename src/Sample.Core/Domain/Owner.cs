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

        [MaxLength(100)]
        public string Name { get; private set; }


        public void Update(string name)
        {
            Name = name;
        }

        //TODO: make read only
        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}
