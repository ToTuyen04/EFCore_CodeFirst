using EFCore_CodeFirst.Dto;
using EFCore_CodeFirst.Dto.Players;
using EFCore_CodeFirst.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayersAsync([FromQuery] UrlQueryParameters urlQueryParameters)
        {
            var players = await _playerService.GetPlayersAsync(urlQueryParameters);
            return Ok(players);
        }

        [HttpGet("{id:long}/detail")]
        public async Task<IActionResult> GetPlayerDetailAsync(int id)
        {
            var player = await _playerService.GetPlayerDetailAsync(id);
            if (player == null || !player.Any())
                return NotFound($"Player with ID {id} not found");
            return Ok(player.First());
        }
        [HttpPost]
        public async Task<IActionResult> PostPlayerAsync([FromBody] CreatePlayerRequest playerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _playerService.CreatePlayerAsync(playerRequest);
            return Ok("Player created successfully");
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutPlayerAsync(int id, [FromBody] UpdatePlayerRequest playerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = await _playerService.UpdatePlayerAsync(id, playerRequest);
            if (isUpdated)
            {
                return Ok("Player updated successfully");
            } else
            {
                return NotFound($"Player with ID {id} not found");
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeletePlayerAsync(int id)
        {
            var isDeleted = await _playerService.DeletePlayerAsync(id);
            if (isDeleted)
            {
                return Ok("Player deleted successfully");
            }
            else
            {
                return NotFound($"Player with ID {id} not found");
            }
        }
    }
}
