using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ShoesStoreApp.DAL.Data
{
    public class ShoesStoreAppDbContextFactory : IDesignTimeDbContextFactory<ShoesStoreAppDbContext>
    {
        public ShoesStoreAppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ShoesStoreApp.PLA"))
            .AddJsonFile("appsettings.json")
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ShoesStoreAppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new ShoesStoreAppDbContext(optionsBuilder.Options);
        }
    }
}
