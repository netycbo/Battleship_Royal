using Battleship_Royal.Data.Entities.Identity;

namespace Battleship_Royal.Api.Handlers.Services
{
    public interface IGenerateJwtToken
    {
        Task<string> GenerateToken(ApplicationUser user, IList<string> role);
    }
}