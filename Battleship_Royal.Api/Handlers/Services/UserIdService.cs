using System.Security.Claims;
using Azure.Core;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Data.DbContext;

namespace Battleship_Royal.Api.Handlers.Services
{
    public class UserIdService(IHttpContextAccessor accessor, BattleshipsDbContext context ) : IUserIdService
    {
        public string GetUserId(string userNickName)
        {
            var userIdClaim = accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                Console.WriteLine($"User ID: {userIdClaim.Value}");
                return userIdClaim.Value;
            }

            throw new Exception("User ID claim not found.");
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
