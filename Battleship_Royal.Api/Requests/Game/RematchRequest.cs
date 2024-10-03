using Battleship_Royal.Api.Responses.Games;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Battleship_Royal.Api.Requests.Game
{
    public class RematchRequest : IRequest<RematchRersponse>
    {
        [Required]
        public string GameId { get; set; }
    }
}