using Battleship_Royal.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Royal.Data.Entities
{
    public class Token
    {
        [Key]
        public string UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.Now;
        public List<ApplicationUser> UserIds { get; set; } = new List<ApplicationUser>();
    }
}
