using TicTacToe.Api.Service;
using TicTacToe.Api.Models;
using Xunit;
using System.Collections.Generic;

namespace Test.Unit
{
    public class GameLogicTests
    {
        [Fact]
        public void CheckWin_ReturnsTrue_WhenRowIsFilled()
        {
            var logic = new GameLogic();
            var board = new List<List<Player?>>
            {
                new() { Player.X, Player.X, Player.X },
                new() { null, null, null },
                new() { null, null, null }
            };
            var result = logic.CheckWin(board, Player.X, 3);
            Assert.True(result);
        }

        [Fact]
        public void CheckWin_ReturnsTrue_WhenColumnIsFilled()
        {
            var logic = new GameLogic();
            var board = new List<List<Player?>>
            {
                new() { Player.O, null, null },
                new() { Player.O, null, null },
                new() { Player.O, null, null }
            };
            var result = logic.CheckWin(board, Player.O, 3);
            Assert.True(result);
        }

        [Fact]
        public void CheckWin_ReturnsTrue_WhenMainDiagonalIsFilled()
        {
            var logic = new GameLogic();
            var board = new List<List<Player?>>
            {
                new() { Player.X, null, null },
                new() { null, Player.X, null },
                new() { null, null, Player.X }
            };
            var result = logic.CheckWin(board, Player.X, 3);
            Assert.True(result);
        }

        [Fact]
        public void CheckWin_ReturnsTrue_WhenAntiDiagonalIsFilled()
        {
            var logic = new GameLogic();
            var board = new List<List<Player?>>
            {
                new() { null, null, Player.O },
                new() { null, Player.O, null },
                new() { Player.O, null, null }
            };
            var result = logic.CheckWin(board, Player.O, 3);
            Assert.True(result);
        }

        [Fact]
        public void CheckWin_ReturnsFalse_WhenNoWin()
        {
            var logic = new GameLogic();
            var board = new List<List<Player?>>
            {
                new() { Player.X, Player.O, Player.X },
                new() { Player.O, Player.X, Player.O },
                new() { Player.O, Player.X, Player.O }
            };
            var result = logic.CheckWin(board, Player.X, 3);
            Assert.False(result);
        }
    }
} 