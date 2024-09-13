using Battleship_Royal.Api.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Battleship_Royal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewGameController(IMediator mediator) : ControllerBase
    {
        [HttpPost("new-game")]
        public async Task<IActionResult> NewGame([FromBody] NewGameRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
