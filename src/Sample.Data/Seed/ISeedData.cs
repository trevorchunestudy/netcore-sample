using Microsoft.EntityFrameworkCore;

namespace Sample.Data.Seed
{
    public interface ISeedData
    {
        void Seed(ModelBuilder modelBuilder);
    }
}
