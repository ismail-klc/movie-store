using System.Net.Http;
using System.Threading.Tasks;

namespace API.Tests.Helpers
{
    public static class ActorActions
    {
        public static async Task<HttpResponseMessage> CreateActor(HttpClient _client)
        {
            var response = await _client.PostAsync("/api/Actors", new JsonContent(
                new
                {
                    firstname = "actor",
                    lastname = "lastname",
                }));

            return response;
        }
    }
}