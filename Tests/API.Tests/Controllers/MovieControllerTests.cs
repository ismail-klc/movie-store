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
    public class MovieControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public MovieControllerTests(ApiWebApplicationFactory fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_401_Creating_Movie()
        {
            // act
            var response = await _client.PostAsync("/api/Genres", new JsonContent(
                new
                {

                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Get_400_With_Wrong_Inputs()
        {
            // act
            await AuthActions.Register(_client, "test10@test.com", "123456");
            var loginResponse = await AuthActions.Login(_client, "test10@test.com", "123456");
            var response = await _client.PostAsync("/api/Genres", new JsonContent(
                new
                {
                    name = "",
                    year = 0,
                    price = 0,
                    genreId = 0,
                    directorId = 0
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_400_With_Wrong_Genre()
        {
            // act
            await AuthActions.Register(_client, "test10@test.com", "123456");
            var loginResponse = await AuthActions.Login(_client, "test10@test.com", "123456");
            var response = await _client.PostAsync("/api/Movies", new JsonContent(
                new
                {
                    name = "movie",
                    year = 2000,
                    price = 10,
                    genreId = 0,
                    directorId = 0
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_400_With_Wrong_Director()
        {
            // act
            await AuthActions.Register(_client, "test11@test.com", "123456");
            var loginResponse = await AuthActions.Login(_client, "test11@test.com", "123456");

            // create and get genre
            await _client.PostAsync("/api/Genres", new JsonContent(
                new
                {
                    name = "genre",
                }));
            var genreResponse = await _client.GetAsync("/api/Genres");
            var content = await genreResponse.Content.ReadAsStringAsync();
            var genres = JsonSerializer.Deserialize<List<GenreViewModel>>(content);

            // create movie
            var response = await _client.PostAsync("/api/Movies", new JsonContent(
                new
                {
                    name = "movie",
                    year = 2000,
                    price = 15,
                    genreId = genres[0].Id,
                    directorId = 0
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_Movie_Success()
        {
            // act
            await AuthActions.Register(_client, "test12@test.com", "123456");
            var loginResponse = await AuthActions.Login(_client, "test12@test.com", "123456");

            // create and get genre
            await _client.PostAsync("/api/Genres", new JsonContent(
                new
                {
                    name = "genre",
                }));
            var genreResponse = await _client.GetAsync("/api/Genres");
            var genres = await genreResponse.Content.ReadAsAsync<List<GenreViewModel>>();

            // create and get director
            await _client.PostAsync("/api/Directors", new JsonContent(
                new
                {
                    firstname = "director",
                    lastname = "lastname"
                }));
            var directorResponse = await _client.GetAsync("/api/Directors");
            var directors = await directorResponse.Content.ReadAsAsync<List<DirectorViewModel>>();

            // create movie
            var response = await _client.PostAsync("/api/Movies", new JsonContent(
                new
                {
                    name = "movie",
                    year = 2000,
                    price = 15,
                    genreId = genres[0].Id,
                    directorId = directors[0].Id
                }));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_Movies()
        {
            var loginResponse = await AuthActions.Login(_client, "test12@test.com", "123456");

            // create and get genre
            await _client.PostAsync("/api/Genres", new JsonContent(
                new
                {
                    name = "genre",
                }));
            var genreResponse = await _client.GetAsync("/api/Genres");
            var genres = await genreResponse.Content.ReadAsAsync<List<GenreViewModel>>();

            // create and get director
            await _client.PostAsync("/api/Directors", new JsonContent(
                new
                {
                    firstname = "director",
                    lastname = "lastname"
                }));
            var directorResponse = await _client.GetAsync("/api/Directors");
            var directors = await directorResponse.Content.ReadAsAsync<List<DirectorViewModel>>();

            // create movie
            var response = await _client.PostAsync("/api/Movies", new JsonContent(
                new
                {
                    name = "movie",
                    year = 2000,
                    price = 15,
                    genreId = genres[0].Id,
                    directorId = directors[0].Id
                }));

            var getMovieResponse = await _client.GetAsync("/api/Movies");
            var movies = await getMovieResponse.Content.ReadAsAsync<List<MovieViewModel>>();

            // assert
            movies.Count.Should().BeGreaterThan(0);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_Movie_By_Id()
        {
            var loginResponse = await AuthActions.Login(_client, "test12@test.com", "123456");

            // create and get genre
            await _client.PostAsync("/api/Genres", new JsonContent(
                new
                {
                    name = "genre",
                }));
            var genreResponse = await _client.GetAsync("/api/Genres");
            var genres = await genreResponse.Content.ReadAsAsync<List<GenreViewModel>>();

            // create and get director
            await _client.PostAsync("/api/Directors", new JsonContent(
                new
                {
                    firstname = "director",
                    lastname = "lastname"
                }));
            var directorResponse = await _client.GetAsync("/api/Directors");
            var directors = await directorResponse.Content.ReadAsAsync<List<DirectorViewModel>>();

            // create movie
            var response = await _client.PostAsync("/api/Movies", new JsonContent(
                new
                {
                    name = "movie",
                    year = 2000,
                    price = 15,
                    genreId = genres[0].Id,
                    directorId = directors[0].Id
                }));

            var getMovieResponse = await _client.GetAsync("/api/Movies");
            var movies = await getMovieResponse.Content.ReadAsAsync<List<MovieViewModel>>();

            var getByIdResponse = await _client.GetAsync("/api/Movies/" + movies[0].Id);

            // assert
            movies.Count.Should().BeGreaterThan(0);
            getByIdResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}