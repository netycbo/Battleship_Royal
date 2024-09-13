using Battleship_Royal.Api.Dtos;

namespace Battleship_Royal.Api.Responses
{
    public class LoginResponse : BaseResponse<LoginDto>
    {
        public string Token { get; set; }
        
    }
}