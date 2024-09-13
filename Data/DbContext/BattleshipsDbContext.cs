using Battleship_Royal.Data.Entities;
using Battleship_Royal.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Battleship_Royal.Data.DbContext
{
    public class BattleshipsDbContext : IdentityDbContext<ApplicationUser>
    {
        public BattleshipsDbContext(DbContextOptions<BattleshipsDbContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
    }
}
