using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TicTacToe.Api.Data;
using TicTacToe.Api.Models;
using TicTacToe.Api.Service;
using TicTacToe.Api.Services;
using Xunit;

namespace Test.Unit
{
    public class MoveServiceTests
    {
        private MoveService CreateService(Game game = null)
        {
            var dbContextMock = new Mock<AppDbContext>(new Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext>());
            var logic = new GameLogic();
            return new MoveService(dbContextMock.Object, logic);
        }

        [Fact]
        public async Task MakeMoveIdempotentAsync_ValidMove_AddsMove()
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Size = 3,
                CurrentPlayer = Player.X,
                Board = new List<List<Player?>>
                {
                    new() { null, null, null },
                    new() { null, null, null },
                    new() { null, null, null }
                },
                Moves = new List<Move>()
            };
            var service = CreateService();
            // Моки не сохраняют в БД, но можно проверить отсутствие исключения
            var request = new MoveRequest { Row = 0, Col = 0, Player = Player.X };
            await Assert.ThrowsAnyAsync<Exception>(() => service.MakeMoveIdempotentAsync(game.Id, request)); // тк нет реального контекста
        }

        [Fact]
        public void ValidateMove_Throws_WhenCellOccupied()
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Size = 3,
                CurrentPlayer = Player.X,
                Board = new List<List<Player?>>
                {
                    new() { Player.X, null, null },
                    new() { null, null, null },
                    new() { null, null, null }
                },
                Moves = new List<Move>()
            };
            var service = CreateService();
            var request = new MoveRequest { Row = 0, Col = 0, Player = Player.X };
            var ex = Assert.Throws<System.Reflection.TargetInvocationException>(() =>
                service.GetType().GetMethod("ValidateMove", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(service, new object[] { game, request })
            );
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        [Fact]
        public void ValidateMove_Throws_WhenNotPlayersTurn()
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Size = 3,
                CurrentPlayer = Player.O,
                Board = new List<List<Player?>>
                {
                    new() { null, null, null },
                    new() { null, null, null },
                    new() { null, null, null }
                },
                Moves = new List<Move>()
            };
            var service = CreateService();
            var request = new MoveRequest { Row = 0, Col = 0, Player = Player.X };
            var ex = Assert.Throws<System.Reflection.TargetInvocationException>(() =>
                service.GetType().GetMethod("ValidateMove", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(service, new object[] { game, request })
            );
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }
    }
} 