using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RaceBay.MVC.Services.Interfaces;

namespace RaceBay.MVC.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private string? _authToken;

    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public ApiService(HttpClient httpClient, IOptions<ApiSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.BaseUrl);
    }

    public void SetAuthToken(string? token)
    {
        _authToken = token;
        _httpClient.DefaultRequestHeaders.Authorization = 
            string.IsNullOrEmpty(token) ? null : new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint) => await _httpClient.GetAsync(endpoint);
    public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T payload) => await _httpClient.PostAsJsonAsync(endpoint, payload, JsonOptions);
    public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T payload) => await _httpClient.PutAsJsonAsync(endpoint, payload, JsonOptions);
    public async Task<HttpResponseMessage> DeleteAsync(string endpoint) => await _httpClient.DeleteAsync(endpoint);
    public async Task<HttpResponseMessage> PostMultipartAsync(string endpoint, MultipartFormDataContent content) => await _httpClient.PostAsync(endpoint, content);
}
