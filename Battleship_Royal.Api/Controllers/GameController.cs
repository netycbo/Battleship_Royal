using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Requests;
using Battleship_Royal.Api.Requests.Game;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Battleship_Royal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IMediator mediator) : ControllerBase
    {
        [HttpPost("prepare-game")]
        public async Task<IActionResult> PrepareMatch([FromBody] PrepareGameRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("rematch/{gameID}")]
        public async Task<IActionResult>GetGameSettingsFromDb(RematchRequest request)
        {
            var rematchGame = await mediator.Send(request);
            return Ok(rematchGame);
        }
        [HttpPost("save-To-Db/{gameID}")]
        public async Task<IActionResult> SaveToDbGames(SaveToGamesDbDto request)
        {
            var rematchGame = await mediator.Send(request);
            return Ok(rematchGame);
        }
        [HttpPost("save-To-temp/{gameID}")]
        public async Task<IActionResult> SaveToTemporaryGame(SaveToTemporaryDbDto request)
        {
            var rematchGame = await mediator.Send(request);
            return Ok(rematchGame);
        }
        [HttpPost("game/{gameID}")]
        public async Task<IActionResult> SaveToDbGames(GameRequest request)
        {
            var rematchGame = await mediator.Send(request);
            return Ok(rematchGame);
        }
    }
}
