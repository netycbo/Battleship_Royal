using Battleship_Royal.Api.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Battleship_Royal.Api.Requests
{
    public class NewGameRequest :IRequest<NewGameResponse>
    {
        [Required]
        public string Player1{ get; set; }
        public string Player2{ get; set; } =string.Empty;
        public bool IsSpeedGame{ get; set; } = false;
        public int Timer { get; set; } = 0;
    }
}