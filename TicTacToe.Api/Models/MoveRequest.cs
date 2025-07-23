namespace TicTacToe.Api.Models;

public class MoveRequest
{
    public int Row { get; set; }
    public int Col { get; set; }
    public Player Player { get; set; }
}
