using Application.Interfaces;
using Domain.Models;
using System.Text.Json;

namespace Application.Services
{
    public class PostServiceApi : IPostServiceApi
    {
        private readonly HttpClient _client;

        public PostServiceApi(IHttpClientFactory client)
        {
            _client = client.CreateClient("SnackisAPI");
        }

        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<Post?> GetByIdAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/Post/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Post>(responseString, _options);
            }
            return null;
        }

        public async Task<List<Post>> GetBySubCategoryIdAsync(int subCategoryId)
        {
            List<Post> posts = new();
            HttpResponseMessage response = await _client.GetAsync($"api/Post/discussion/{subCategoryId}");
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                posts = JsonSerializer.Deserialize<List<Post>>(responseString, _options) ?? new();
            }
            return posts;
        }
    }
}