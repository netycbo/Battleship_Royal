using System.Security.Claims;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests;
using Battleship_Royal.Data.Entities;

namespace Battleship_Royal.Api.Handlers.Services
{
    public class UserIdService(IHttpContextAccessor accessor) : IUserIdService
    {
        public string GetUserId()
        {
            var userId = accessor.HttpContext?.User;

            var userClaims = userId.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaims == null)
            {
                return "-1";
            }
            return userClaims.Value;
        }
    }
}
