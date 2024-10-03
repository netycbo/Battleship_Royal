using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Requests;
using Battleship_Royal.Api.Requests.Game;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Battleship_Royal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IMediator mediator) : ControllerBase
    {
        //[Authorize(Roles = "Player")]
        [HttpPost("prepare-game")]
        public async Task<IActionResult> PrepareMatch([FromQuery] PrepareGameRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult>GetGameSettingsFromRedis([FromQuery]RematchRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("save-To-Db/{gameID}")]
        public async Task<IActionResult> SaveToDbGames(SaveToGamesDbDto request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("save-To-temp/{gameID}")]
        public async Task<IActionResult> SaveToTemporaryGame(SaveToTemporaryDbDto request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("game/{gameID}")]
        public async Task<IActionResult> SaveToDbGames(GameRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
