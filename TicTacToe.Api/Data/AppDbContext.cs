using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TicTacToe.Api.Models;

namespace TicTacToe.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Move> Moves { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи Game -> Moves (один-ко-многим)
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Moves)
                .WithOne(m => m.Game)
                .HasForeignKey(m => m.GameId);

            // Сериализация массива Board в JSON для PostgreSQL
            modelBuilder.Entity<Game>()
                .Property(g => g.Board)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<List<Player?>>>(v, (JsonSerializerOptions)null)
                );
        }
    }
}
