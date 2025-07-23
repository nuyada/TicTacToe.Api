using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Test.Integration
{
    public class InvalidJsonTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public InvalidJsonTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostMove_WithInvalidJson_ReturnsProblemDetails()
        {
            // Arrange
            var invalidJson = "{ \"row\": 1, \"col\": 2, \"player\": }"; // некорректный JSON
            var content = new StringContent(invalidJson, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/move/00000000-0000-0000-0000-000000000000", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("application/problem+json", response.Content.Headers.ContentType.MediaType);
            var body = await response.Content.ReadAsStringAsync();
            Assert.Contains("title", body, System.StringComparison.OrdinalIgnoreCase); // Проверяем, что это ProblemDetails
        }
    }
} 