using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Api.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public int Size { get; set; }  // Размер поля (3x3, 5x5 и т.д.)
        public int WinLength { get; set; }
        public Player CurrentPlayer { get; set; }
        public Player? Winner { get; set; }
        public bool IsDraw { get; set; }
        
        public List<List<Player?>> Board { get; set; }

        // Навигационное свойство для ходов
        public List<Move> Moves { get; set; } = new();
        
    }
}
