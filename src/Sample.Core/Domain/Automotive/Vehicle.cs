using Sample.Core.Enums;
using Sample.Core.Enums.Vehicles;
using Sample.Core.Extensions;
using Sample.Core.ValueObjects;
using Sample.Core.ValueObjects.Vehicles;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Sample.Core.Domain.Automotive
{
    public class Vehicle : Entity
    {
        private Vehicle() { }

        private Vehicle(long ownerId, Automobile automobile,
            string title, string description, Mileage mileage,
            string url, string imageUrl, string imageTag,
            Condition condition, string price, string address, string exteriorColor, string salePrice,
            StateOfVehicle stateOfVehicle, Geometry geometry)
        {
            OwnerId = ownerId;
            Automobile = automobile;
            Title = title.Truncate(100);
            Description = string.IsNullOrEmpty(description) || !Regex.IsMatch(description, @"[a-zA-Z]+") ? title : description;
            Mileage = mileage;
            Url = url;
            ImageUrl = imageUrl;
            ImageTag = imageTag;
            Condition = condition.DisplayName;
            Price = price.Truncate(25);
            Address = address;
            ExteriorColor = exteriorColor.Truncate(25);
            SalePrice = salePrice.Truncate(25);
            StateOfVehicle = stateOfVehicle.DisplayName;
            Geometry = geometry;
        }

        public long OwnerId { get; private set; }

        public Automobile Automobile { get; private set; }

        [MaxLength(100)]
        public string Title { get; private set; }

        public string Description { get; private set; }


        public Mileage Mileage { get; private set; }

        [MaxLength(2083)]
        public string Url { get; private set; }

        [MaxLength(2083)]
        public string ImageUrl { get; private set; }

        [MaxLength(2083)]
        public string ImageTag { get; private set; }


        [MaxLength(10)]
        public string Condition { get; private set; }

        [MaxLength(25)]
        public string Price { get; private set; }
        public string Address { get; private set; }

        [MaxLength(25)]
        public string ExteriorColor { get; private set; }

        [MaxLength(25)]
        public string SalePrice { get; private set; }

        [MaxLength(4)]
        public string StateOfVehicle { get; private set; }

        public Geometry Geometry { get; private set; }

        public virtual Owner Owner { get; private set; }

        public static Condition SetCondition(string condition)
        {
            try
            {
                return Enumeration.FromDisplayName<Condition>(condition);
            }
            catch
            {
                return Enums.Vehicles.Condition.Excellent;
            }

        }

        public static MileageUnit SetMileageUnit(string mileageUnit)
        {
            try
            {
                return Enumeration.FromDisplayName<MileageUnit>(mileageUnit);
            }
            catch
            {
                return MileageUnit.MI;
            }
        }

        public static StateOfVehicle SetStateOfVehicle(string stateOfVehicle)
        {
            try
            {
                return Enumeration.FromDisplayName<StateOfVehicle>(stateOfVehicle);
            }
            catch
            {
                return EnumDisplayNameParser.StateOfVehicleFromName(stateOfVehicle);
            }
        }

        public static Vehicle Create(long ownerId, Automobile automobile, string title, string description,
            int mileageValue, string mileageUnit, string url, string imageUrl, string imageTag, string condition, string price,
            string address, string exteriorColor, string salePrice, string stateOfVehicle, double latitude, double longitude)
        {

            var geometry = new Geometry(latitude, longitude);

            if (automobile == null ||
                string.IsNullOrEmpty(title) ||
                string.IsNullOrEmpty(url) ||
                string.IsNullOrEmpty(imageUrl) ||
                string.IsNullOrEmpty(price) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(exteriorColor))
                return null;

            if (url.Length > 2083 ||
                imageUrl.Length > 2083 ||
                imageTag.Length > 2083)
                return null;

            var cond = SetCondition(condition);
            var mu = SetMileageUnit(mileageUnit);
            var sov = SetStateOfVehicle(stateOfVehicle);
            if (cond == null || mu == null || sov == null)
                return null;

            var mileage = new Mileage(mileageValue, mu);

            return new Vehicle(ownerId, automobile, title, description, mileage, url, imageUrl, imageTag, cond, price,
                address, exteriorColor, salePrice, sov, geometry);
        }
    }
}
