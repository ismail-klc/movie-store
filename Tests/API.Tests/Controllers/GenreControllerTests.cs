using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using API.Tests.Helpers;
using Entities.ViewModels;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace API.Tests.Controllers
{
    public class GenreControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public GenreControllerTests(ApiWebApplicationFactory fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_401_Creating_Genre()
        {
            // act
            var response = await _client.PostAsync("/api/Genres", new JsonContent(
                new
                {
                    name = "genre",
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Get_400_With_Wrong_Inputs()
        {
            // act
            await AuthActions.Register(_client, "test7@test.com", "123456");
            var loginResponse = await AuthActions.Login(_client, "test7@test.com", "123456");
            var response = await _client.PostAsync("/api/Genres", new JsonContent(
                new
                {
                    name = "",
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Genre()
        {
            // act
            await AuthActions.Register(_client, "test6@test.com", "123456");
            await AuthActions.Login(_client, "test6@test.com", "123456");
            var response = await _client.PostAsync("/api/Genres", new JsonContent(
                new
                {
                    name = "genre",
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_Genres()
        {
            // act
            await AuthActions.Register(_client, "test8@test.com", "123456");
            await AuthActions.Login(_client, "test8@test.com", "123456");
            await _client.PostAsync("/api/Genres", new JsonContent(
                new
                {
                    name = "genre",
                }));

            var response = await _client.GetAsync("/api/Genres");
            var genres = await response.Content.ReadAsAsync<List<GenreViewModel>>();

            // assert
            Assert.Equal(genres.Count, 1);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}