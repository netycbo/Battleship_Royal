using Battleship_Royal.Data.Entities;
using Battleship_Royal.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
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
            
            modelBuilder.Entity<ComputerPlayer>().HasData(new ComputerPlayer
            {
                Id = 1,
                NickName = "Red October"
            });
            if ((!modelBuilder.Model.GetEntityTypes().Any(e => e.Name == typeof(IdentityRole).Name)))
            {
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Player",
                    NormalizedName = "PLAYER"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

            }
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<ComputerPlayer> ComputerPlayers { get; set; }
    }
   
}
