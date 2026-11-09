using Microsoft.Extensions.Options;
using RaceBay.MVC.Models.Dto;
using RaceBay.MVC.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using RaceBay.MVC.Models.Dto;
namespace RaceBay.MVC.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _settings;
    private string? _authToken;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiService(HttpClient httpClient, IOptions<ApiSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public void SetAuthToken(string? token)
    {
        _authToken = token;
        _httpClient.DefaultRequestHeaders.Authorization =
            string.IsNullOrEmpty(token)
                ? null
                : new AuthenticationHeaderValue("Bearer", token);
    }
    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", request, JsonOptions);
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<AuthResponseDto>(JsonOptions);
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", request, JsonOptions);
        if (!response.IsSuccessStatusCode) return null;

        // Some APIs return the token immediately after register, some don't.
        // If yours doesn't return an AuthResponse, return a dummy so the controller knows it succeeded.
        if (response.Content.Headers.ContentLength == 0)
            return new AuthResponseDto(); // success but no token

        return await response.Content.ReadFromJsonAsync<AuthResponseDto>(JsonOptions);
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
        => await _httpClient.GetAsync(endpoint);

    public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T payload)
        => await _httpClient.PostAsJsonAsync(endpoint, payload, JsonOptions);

    public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T payload)
        => await _httpClient.PutAsJsonAsync(endpoint, payload, JsonOptions);

    public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        => await _httpClient.DeleteAsync(endpoint);

    public async Task<HttpResponseMessage> PostMultipartAsync(string endpoint, MultipartFormDataContent content)
        => await _httpClient.PostAsync(endpoint, content);
}