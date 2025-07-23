using TicTacToe.Api.Models;
using TicTacToe.Api.Data;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Api.Service;

namespace TicTacToe.Api.Services;

public class MoveService
{
    private readonly AppDbContext _dbContext;
    private readonly GameLogic _gameLogic;

    public MoveService(AppDbContext dbContext, GameLogic gameLogic)
    {
        _dbContext = dbContext;
        _gameLogic = gameLogic;
    }

    public async Task<(Game game, Move move, bool isDuplicate)> MakeMoveIdempotentAsync(Guid gameId, MoveRequest request)
    {
        var game = await _dbContext.Games
            .Include(g => g.Moves)
            .FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            throw new ArgumentException("Игра не найдена");

        var existingMove = game.Moves.FirstOrDefault(m => m.Row == request.Row && m.Col == request.Col && m.Player == request.Player);
        if (existingMove != null)
            return (game, existingMove, true);

        ValidateMove(game, request);
        Player actualPlayer = GetActualPlayer(request.Player, game.Moves.Count + 1);
        var move = new Move
        {
            Row = request.Row,
            Col = request.Col,
            Player = actualPlayer,
            ETag = Guid.NewGuid().ToString(),
            GameId = gameId
        };
        game.Board[request.Row][request.Col] = actualPlayer;
        game.Moves.Add(move);
        if (_gameLogic.CheckWin(game.Board, actualPlayer, game.WinLength))
            game.Winner = actualPlayer;
        else if (game.Moves.Count == game.Size * game.Size)
            game.IsDraw = true;
        else
            game.CurrentPlayer = actualPlayer == Player.X ? Player.O : Player.X;
        _dbContext.Entry(game).Property(g => g.Board).IsModified = true;
        await _dbContext.SaveChangesAsync();
        return (game, move, false);
    }

    private void ValidateMove(Game game, MoveRequest request)
    {
        if (game.Winner != null || game.IsDraw)
            throw new InvalidOperationException("Игра уже завершена");
        if (request.Row < 0 || request.Row >= game.Size || request.Col < 0 || request.Col >= game.Size)
            throw new ArgumentException("Некорректные координаты");
        if (game.Board[request.Row][request.Col] != null)
            throw new InvalidOperationException("Клетка уже занята");
        if (request.Player != game.CurrentPlayer)
            throw new InvalidOperationException("Не ваш ход");
    }

    private Player GetActualPlayer(Player requestedPlayer, int moveCount)
    {
        if (moveCount % 3 == 0 && Random.Shared.NextDouble() < 0.1)
            return requestedPlayer == Player.X ? Player.O : Player.X;
        return requestedPlayer;
    }

    public static GameDto MapToGameDto(Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Size = game.Size,
            CurrentPlayer = (int)game.CurrentPlayer,
            Winner = game.Winner.HasValue ? (int?)game.Winner.Value : null,
            IsDraw = game.IsDraw,
            WinLength = game.WinLength,
            Board = game.Board.Select(row => row.Select(cell => cell.HasValue ? (int?)cell.Value : null).ToList()).ToList(),
            Moves = game.Moves.Select(MapToMoveDto).ToList()
        };
    }

    public static MoveDto MapToMoveDto(Move move)
    {
        return new MoveDto
        {
            Row = move.Row,
            Col = move.Col,
            Player = (int)move.Player,
            ETag = move.ETag
        };
    }
}
