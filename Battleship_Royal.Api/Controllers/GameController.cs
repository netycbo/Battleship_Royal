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
        [Authorize(Roles = "Player")]
        [HttpPost("prepare-game")]
        public async Task<IActionResult> PrepareMatch([FromBody] PrepareGameVsComputerRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("rematch")]
        public async Task<IActionResult>GetGameSettingsFromRedis([FromQuery]RematchRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("save-To-Db")]
        public async Task<IActionResult> SaveToDbGames([FromBody] SaveToDbRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
        
        [HttpGet("player-stats")]
        public async Task<IActionResult> ShowPlayerStats([FromQuery] GameStatsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // This helps in debugging if the model binding is incorrect.
            }
            var result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
