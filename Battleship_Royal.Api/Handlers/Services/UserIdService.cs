using System.Security.Claims;
using Azure.Core;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Data.DbContext;

namespace Battleship_Royal.Api.Handlers.Services
{
    public class UserIdService(IHttpContextAccessor accessor, BattleshipsDbContext context ) : IUserIdService
    {
        public string GetUserId()
        {
            var userId = accessor.HttpContext?.User;

            if (userId != null)
            {
                var allClaims = userId.Claims.ToList();
                foreach (var claim in allClaims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }
            }

            var userClaims = userId.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaims == null)
            {
                Console.WriteLine("No Name claim found");
                return "-1";
            }

            return userClaims.Value;
        }

        public string GetUserNickName(string userId)
        {
            string nickName = context.Users
                   .Where(u => u.Id == userId)
                   .Select(u => u.NickName)
                   .FirstOrDefault();
            return nickName;
        }
    }
}
