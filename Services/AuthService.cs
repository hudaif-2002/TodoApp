using System.Net.Http.Json;
using TodoApp.Models;

namespace TodoApp.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://apigateway-production-85e5.up.railway.app"; // Local Gateway
    // For production: "https://apigateway-production-85e5.up.railway.app"

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AuthResponse?> LoginAsync(string email, string password)
    {
        var request = new { Email = email, Password = password };
        var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/auth/login", request);
        
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AuthResponse>();
    }

    public async Task<AuthResponse?> RegisterAsync(string email, string password, string fullName)
    {
        var request = new { Email = email, Password = password, FullName = fullName };
        var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/auth/register", request);
        
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AuthResponse>();
    }
}

