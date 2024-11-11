using Battleship_Royal.Api.Requests.Game;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Battleship_Royal.Api.Controllers
{
    //[Authorize(Roles = "Player")]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(IMediator mediator) : ControllerBase
    {
        [HttpPost("prepare-gamevscomputer")]
        public async Task<IActionResult> PrepareMatch([FromBody] PrepareGameVsComputerRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("prepare-gameVsHuman")]
        public async Task<IActionResult> StartGame([FromBody] PrepareGameVsHumanRequest request)
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
                return BadRequest(ModelState); 
            }
            var result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
