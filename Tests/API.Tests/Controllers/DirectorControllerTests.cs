using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using API.Tests.Helpers;
using Entities.ViewModels;
using FluentAssertions;
using Xunit;

namespace API.Tests.Controllers
{
    public class DirectorControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public DirectorControllerTests(ApiWebApplicationFactory fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_401_Creating_Director()
        {
            // act
            var response = await _client.PostAsync("/api/Directors", new JsonContent(
                new
                {
                    firstname = "actor",
                    lastname = "lastname",
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Create_Director()
        {
            // act
            await AuthActions.Register(_client, "test4@test.com", "123456");
            await AuthActions.Login(_client, "test4@test.com", "123456");
            var response = await _client.PostAsync("/api/Directors", new JsonContent(
                new
                {
                    firstname = "director",
                    lastname = "lastname",
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_400_With_Wrong_Inputs()
        {
            // act
            await AuthActions.Register(_client, "test5@test.com", "123456");
            var loginResponse = await AuthActions.Login(_client, "test5@test.com", "123456");
            var response = await _client.PostAsync("/api/Directors", new JsonContent(
                new
                {
                    firstname = "",
                    lastname = "",
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Directors()
        {
            // act
            var response = await _client.GetAsync("/api/Directors");
            var content = await response.Content.ReadAsStringAsync();
            var actors = JsonSerializer.Deserialize<List<DirectorViewModel>>(content);

            // assert
            Assert.Equal(actors.Count, 1);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}