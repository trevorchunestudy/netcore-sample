using Sample.Core.Enums.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace Sample.Core.ValueObjects.Vehicles
{
    public class Mileage : ValueObject<Mileage>
    {
        private Mileage() { }

        public Mileage(int value, MileageUnit mileageUnit)
        {
            Value = value;
            Unit = mileageUnit.DisplayName;
        }

        public int Value { get; private set; }

        [MaxLength(2)]
        public string Unit { get; private set; }
    }
}
