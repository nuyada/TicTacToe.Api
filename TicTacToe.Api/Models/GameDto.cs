using System;
using System.Collections.Generic;

namespace TicTacToe.Api.Models;

public class GameDto
{
    public Guid Id { get; set; }
    public int Size { get; set; }
    public int CurrentPlayer { get; set; }
    public int? Winner { get; set; }
    public bool IsDraw { get; set; }
    public int WinLength { get; set; }
    public List<List<int?>> Board { get; set; }
    public List<MoveDto> Moves { get; set; }
}

public class MoveDto
{
    public int Row { get; set; }
    public int Col { get; set; }
    public int Player { get; set; }
    public string ETag { get; set; }
} 