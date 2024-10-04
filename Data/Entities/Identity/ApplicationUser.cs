using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
