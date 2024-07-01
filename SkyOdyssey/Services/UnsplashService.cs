using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;

namespace SkyOdyssey.Services
{
    public class UnsplashService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.unsplash.com/";
        private const string ClientId = "OEaA7ycBs3zuGgQDSYojIxv5GLhD4GTsTOveEwJ4Lh0";

        public UnsplashService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<string> GetRandomHotelRoomImageAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"photos/random?query=hotel-room&client_id={ClientId}");
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var imageUrl = JObject.Parse(jsonResponse)["urls"]["regular"].ToString();
                return imageUrl;
            }

            return null;
        }

        public async Task DownloadImageAsync(string imageUrl, string savePath)
        {
            var response = await _httpClient.GetAsync(imageUrl);
            if (response.IsSuccessStatusCode)
            {
                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                await System.IO.File.WriteAllBytesAsync(savePath, imageBytes);
            }
        }
    }
}
