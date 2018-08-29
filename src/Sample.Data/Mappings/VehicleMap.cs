using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Domain.Automotive;

namespace Sample.Data.Mappings
{
    public class VehicleMap : EntityMappingConfiguration<Vehicle>
    {
        public override void Map(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicle");
            builder.OwnsOne(e => e.Automobile);
            builder.OwnsOne(e => e.Mileage);
            builder.OwnsOne(e => e.Geometry);
            builder.HasOne(e => e.Owner)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(e => e.OwnerId);
        }
    }
}
