using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sample.Data
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<SampleContext>
    {
        //this is for EF Core Command line (localdb) work..
        public SampleContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SampleContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Sample-Test;Integrated Security=True");
            return new SampleContext(optionsBuilder.Options);
        }
    }
}
