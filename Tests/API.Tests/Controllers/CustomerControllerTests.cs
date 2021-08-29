using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API.Tests.Helpers;
using API.Tests.Helpers.Actions;
using FluentAssertions;
using Xunit;

namespace API.Tests.Controllers
{
    public class CustomerControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CustomerControllerTests(ApiWebApplicationFactory fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Create_Customer()
        {
            // act
            var response = await CustomerActions.CreateCustomer(_client, "test3@test.com", "123456");
            
            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Login_Customer()
        {
            // act
            var response = await CustomerActions.CustomerLogin(_client, "test3@test.com", "123456");
            response.Headers.TryGetValues("Set-Cookie", out var setCookie);

            // assert
            setCookie.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Logout()
        {
            // act
            await CustomerActions.CustomerLogin(_client, "test3@test.com", "123456");
            var response = await _client.PostAsync("/api/Customers/logout", new JsonContent(new {}));

            response.Headers.TryGetValues("Set-Cookie", out var setCookie);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            setCookie.Should().Equal("jwt=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/");
        }
    }
}