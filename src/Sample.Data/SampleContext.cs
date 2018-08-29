using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sample.Core.Domain;
using Sample.Core.Domain.Automotive;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Sample.Data
{
    public class SampleContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public SampleContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Owner> Owners { get; set;}
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = GetType().Assembly;
            modelBuilder.AddEntityConfigurationsFromAssembly(assembly);
            modelBuilder.SeedDataFromAssembly(assembly);
            modelBuilder.AddDecimalPrecision();
        }

        public void BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
