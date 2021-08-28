using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using API.Tests.Helpers;
using System.Linq;
using System.Web;
using System;

namespace API.Tests.Controllers
{
    public class AuthControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AuthControllerTests(ApiWebApplicationFactory fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Register_Success()
        {
            // act
            var response = await AuthActions.Register(_client, "test@test.com", "123456");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Login_Success()
        {
            // act
            var response = await AuthActions.Login(_client, "test@test.com", "123456");
            response.Headers.TryGetValues("Set-Cookie", out var setCookie);

            // assert
            setCookie.Should().NotBeEmpty();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Logout()
        {
            // act
            await AuthActions.Login(_client, "test@test.com", "123456");
            var response = await _client.PostAsync("/api/Auth/logout", new JsonContent(new {}));

            response.Headers.TryGetValues("Set-Cookie", out var setCookie);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            setCookie.Should().Equal("jwt=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/");
        }
    }
}