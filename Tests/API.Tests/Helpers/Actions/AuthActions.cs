using System.Net.Http;
using System.Threading.Tasks;

namespace API.Tests.Helpers
{
    public static class AuthActions
    {
        public static async Task<HttpResponseMessage> Register(HttpClient _client, string email, string password)
        {
            var response = await _client.PostAsync("/api/Auth/register", new JsonContent(
                new
                {
                    firstname = "Test",
                    lastname = "lastname",
                    email = email,
                    password = password
                }));

            return response;
        }

        public static async Task<HttpResponseMessage> Login(HttpClient _client, string email, string password)
        {
            var response = await _client.PostAsync("/api/Auth/login", new JsonContent(
                new
                {
                    email = email,
                    password = password
                }));

            return response;
        }
    }
}