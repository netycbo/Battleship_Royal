using Battleship_Royal.Api.Dtos;

namespace Battleship_Royal.Api.Responses.Players
{
    public class LoginResponse : BaseResponse<LoginDto>
    {
        public string Token { get; set; }

    }
}