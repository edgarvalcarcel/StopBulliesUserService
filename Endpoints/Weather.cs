using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using PlanifyIdentity.Infrastructure;

namespace PlanifyIdentity.Endpoints;

public class Weather : IEndpointGroupBase
{
    public void Map(WebApplication app)
    {
        app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}").RequireAuthorization();

        app.MapGet("/Weather", async () =>
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string longitude = "-74.063644";
            string latitude = "4.624335";
            string url = "https://weatherbit-v1-mashape.p.rapidapi.com/current?lon=" + longitude + "&lat=" + latitude + "&units=metric&lang=en";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
    {
        { "x-rapidapi-key", "e5d7c26cc2mshfcd70a329e479c7p1b3d94jsnac356a890845" },
        { "x-rapidapi-host", "weatherbit-v1-mashape.p.rapidapi.com" },
    },
            };
            using HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            WeatherResponse myDeserializedClass = JsonConvert.DeserializeObject<WeatherResponse>(responseString);

            return myDeserializedClass;
        }
        ).RequireAuthorization();
    }
}
