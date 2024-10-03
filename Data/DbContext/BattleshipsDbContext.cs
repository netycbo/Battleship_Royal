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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TemporaryGame>()
                .HasNoKey();
            modelBuilder.Entity<ComputerPlayer>().HasData(new ComputerPlayer
            {
                Id = 1,
                NickName = "Red October"
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<TemporaryGame> TemporaryGames { get; set; }
        public DbSet<ComputerPlayer> ComputerPlayers { get; set; }
    }
   
}
