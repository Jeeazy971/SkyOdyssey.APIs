using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkyOdyssey.Services
{
    public class ExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetFlightsAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://sky-scanner3.p.rapidapi.com/flights/airports"),
                Headers =
                {
                    { "x-rapidapi-key", "0b8f669389mshf8bdfbfb167105cp195499jsn9d657db7a8af" },
                    { "x-rapidapi-host", "sky-scanner3.p.rapidapi.com" },
                },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> GetHotelsAsync(string entityId, DateTime checkin, DateTime checkout)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://sky-scanner3.p.rapidapi.com/hotels/search?entityId={entityId}&checkin={checkin:yyyy-MM-dd}&checkout={checkout:yyyy-MM-dd}"),
                Headers =
                {
                    { "x-rapidapi-key", "0b8f669389mshf8bdfbfb167105cp195499jsn9d657db7a8af" },
                    { "x-rapidapi-host", "sky-scanner3.p.rapidapi.com" },
                },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
