using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Services;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;
    public GameController(GameService gameService) => _gameService = gameService;

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromQuery] int? size, [FromQuery] int? winLength)
        => Ok(await _gameService.CreateGameAsync(size, winLength));
}
