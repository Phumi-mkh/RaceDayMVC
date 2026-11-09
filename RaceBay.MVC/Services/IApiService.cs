namespace RaceBay.MVC.Services.Interfaces;

public interface IApiService
{
    Task<HttpResponseMessage> GetAsync(string endpoint);
    Task<HttpResponseMessage> PostAsync<T>(string endpoint, T payload);
    Task<HttpResponseMessage> PutAsync<T>(string endpoint, T payload);
    Task<HttpResponseMessage> DeleteAsync(string endpoint);
    Task<HttpResponseMessage> PostMultipartAsync(string endpoint, MultipartFormDataContent content);
    void SetAuthToken(string? token);
}
