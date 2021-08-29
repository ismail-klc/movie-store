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
    public class ActorControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ActorControllerTests(ApiWebApplicationFactory fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_401_Creating_Actor()
        {
            // act
            var response = await _client.PostAsync("/api/Actors", new JsonContent(
                new
                {
                    firstname = "actor",
                    lastname = "lastname",
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Create_Actor()
        {
            // act
            await AuthActions.Register(_client, "test2@test.com", "123456");
            await AuthActions.Login(_client, "test2@test.com", "123456");
            var response = await _client.PostAsync("/api/Actors", new JsonContent(
                new
                {
                    firstname = "actor",
                    lastname = "lastname",
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_400_With_Wrong_Inputs()
        {
            // act
            await AuthActions.Login(_client, "test2@test.com", "123456");
            var response = await _client.PostAsync("/api/Actors", new JsonContent(
                new
                {
                    firstname = "",
                    lastname = "",
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Actors()
        {
            // act
            var response = await _client.GetAsync("/api/Actors");
            var actors = await response.Content.ReadAsAsync<List<ActorViewModel>>();

            // assert
            Assert.Equal(actors.Count, 1);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}