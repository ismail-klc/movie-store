using System.Net.Http;
using System.Threading.Tasks;

namespace API.Tests.Helpers.Actions
{
    public static class CustomerActions
    {
         public static async Task<HttpResponseMessage> CreateCustomer(HttpClient _client, string email, string password)
        {
            var response = await _client.PostAsync("/api/Customers", new JsonContent(
                new
                {
                    firstname = "Customer",
                    lastname = "lastname",
                    email = email,
                    password = password
                }));

            return response;
        }

        public static async Task<HttpResponseMessage> CustomerLogin(HttpClient _client, string email, string password)
        {
            var response = await _client.PostAsync("/api/Customers/login", new JsonContent(
                new
                {
                    email = email,
                    password = password
                }));

            return response;
        }
    }
}