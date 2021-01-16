using CoreSample.DBAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreSample.Infrastructure.Settings;

namespace CoreSample.DBAccess
{
    public class CoreSampleDBContext: DbContext, ICoreSampleDBContext
    {
        private IConfigurations _configurations;

        public CoreSampleDBContext(IConfigurations configuratons)
        {
            _configurations = configuratons;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configurations.DBConnection);
            //optionsBuilder.UseSqlServer("Server=.\\SQL2014;Database=CoreSample2;User Id=sa;Password=xfolk44;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<T> DbSet<T>() where T : class
        { 
            return Set<T>();
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
