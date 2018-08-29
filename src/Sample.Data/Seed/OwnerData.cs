using Microsoft.EntityFrameworkCore;
using Sample.Core.Domain;
using System;

namespace Sample.Data.Seed
{
    public class OwnerData : ISeedData
    {
        //dates
        private static readonly DateTime now = new DateTime(2018, 08, 21, 17, 0, 0, DateTimeKind.Utc);

        //owners
        private static readonly Owner c1 = new Owner("Joe") { Id = 1L, CreatedOn = now };

        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(c => { c.HasData(c1); });
        }
    }
}
