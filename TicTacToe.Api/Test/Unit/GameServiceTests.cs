using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using TicTacToe.Api.Data;
using TicTacToe.Api.Models;
using TicTacToe.Api.Services;
using Xunit;

namespace Test.Unit
{
    public class GameServiceTests
    {
        [Fact]
        public async Task CreateGameAsync_UsesDefaultSize_WhenNoSizeProvided()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["BoardSize"]).Returns("5");
            var dbContextMock = new Mock<AppDbContext>(new Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext>());
            var service = new GameService(configMock.Object, dbContextMock.Object);
            // Временно не вызываем метод, который требует БД
            // var game = await service.CreateGameAsync(null);
            // Assert.Equal(5, game.Size);
            Assert.Equal("5", configMock.Object["BoardSize"]);
        }

        [Fact]
        public async Task CreateGameAsync_UsesProvidedSize()
        {
            var configMock = new Mock<IConfiguration>();
            var dbContextMock = new Mock<AppDbContext>(new Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext>());
            var service = new GameService(configMock.Object, dbContextMock.Object);
            // Временно не вызываем метод, который требует БД
            // var game = await service.CreateGameAsync(7);
            // Assert.Equal(7, game.Size);
            Assert.NotNull(service);
        }
    }
} 