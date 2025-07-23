namespace TicTacToe.Api.Models
{
    public class Move
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public Player Player { get; set; }
        public string ETag { get; set; }  // Для идемпотентности
        public Guid Id { get; set; } // Первичный ключ
        public Guid GameId { get; set; } // Связь с игрой
        public Game Game { get; set; }   // Навигационное свойство
    }
}
