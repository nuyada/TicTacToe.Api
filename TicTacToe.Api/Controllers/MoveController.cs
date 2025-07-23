using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Services;
using TicTacToe.Api.Data;
using TicTacToe.Api.Models;

namespace TicTacToe.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoveController : ControllerBase
{
    private readonly MoveService _moveService;
    private readonly AppDbContext _dbContext;

    public MoveController(MoveService moveService, AppDbContext dbContext)
    {
        _moveService = moveService;
        _dbContext = dbContext;
    }

    [HttpPost("{gameId}")]
    public async Task<IActionResult> MakeMove(Guid gameId, [FromBody] MoveRequest request)
    {
        try
        {
            var (game, move, isDuplicate) = await _moveService.MakeMoveIdempotentAsync(gameId, request);
            Response.Headers["ETag"] = move.ETag;
            var dto = MoveService.MapToGameDto(game);
            return Ok(new { game = dto, move = MoveService.MapToMoveDto(move), isDuplicate });
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            return ex is ArgumentException
                ? NotFound(new { error = ex.Message })
                : BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("game/{gameId}")]
    public async Task<IActionResult> GetGame(Guid gameId)
    {
        var game = await _dbContext.Games.FindAsync(gameId);
        if (game == null)
            return NotFound(new { error = "Game not found" });
        // Подгружаем ходы
        await _dbContext.Entry(game).Collection(g => g.Moves).LoadAsync();
        var dto = MoveService.MapToGameDto(game);
        return Ok(dto);
    }
}
