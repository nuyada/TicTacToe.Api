using TicTacToe.Api.Models;
using Microsoft.Extensions.Configuration;
using TicTacToe.Api.Data;

namespace TicTacToe.Api.Services;

public class GameService
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _dbContext;
    public GameService(IConfiguration config, AppDbContext dbContext)
    {
        _config = config;
        _dbContext = dbContext;
    }

    public async Task<Game> CreateGameAsync(int? size, int? winLength)
    {
        int boardSize = size ?? _config.GetValue<int>("BoardSize");
        int win = winLength ?? _config.GetValue<int>("WinLength");
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Size = boardSize,
            WinLength = win,
            CurrentPlayer = Random.Shared.Next(2) == 0 ? Player.X : Player.O,
            Board = Enumerable.Range(0, boardSize).Select(_ => new List<Player?>(new Player?[boardSize])).ToList()
        };
        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync();
        return game;
    }
}
