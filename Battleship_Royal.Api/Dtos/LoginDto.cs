using Battleship_Royal.Api.Requests.Players;

namespace Battleship_Royal.Api.Dtos
{
    public class LoginDto
    {
       public string NickName { get; set; }
        public TimeOnly Time { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public LoginDto() { }
    }
}