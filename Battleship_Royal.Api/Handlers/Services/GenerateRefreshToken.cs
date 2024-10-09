using Battleship_Royal.Api.Handlers.Services.Interfaces;

namespace Battleship_Royal.Api.Handlers.Services
{
    public class GenerateRefreshToken : IGenerateRefreshToken
    {
        public string Generate()
        {
            var randomNumber = new byte[32];
            var rng = new Random();
            rng.NextBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
