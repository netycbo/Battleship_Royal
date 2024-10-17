using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Battleship_Royal.Data.DbContext
{
    public class BattleshipsDbContextFactory : IDesignTimeDbContextFactory<BattleshipsDbContext>
    {
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Battleship_Royal.Api");
        public BattleshipsDbContext CreateDbContext(string[] args)
        {
         
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BattleshipsDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new BattleshipsDbContext(builder.Options);
        }
    }
}
