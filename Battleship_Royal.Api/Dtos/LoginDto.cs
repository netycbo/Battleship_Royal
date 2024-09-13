using Battleship_Royal.Api.Requests;

namespace Battleship_Royal.Api.Dtos
{
    public class LoginDto
    {
        public string Message { get; set; }

        public LoginDto(LoginRequest loginRequest)
        {
            Message = $"Welcome {loginRequest.NickName}";
        }
    }
}