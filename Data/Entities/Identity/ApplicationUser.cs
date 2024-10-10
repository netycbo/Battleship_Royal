using Microsoft.AspNetCore.Identity;

namespace Battleship_Royal.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }
        public int NumberOFGames { get; set; }
        public bool IsInGame { get; set; } = false;
        public DateTime JoinedDate { get; set; } = DateTime.Now;
        public List<Game> Games { get; set; } = new List<Game>();
    }
       
}
