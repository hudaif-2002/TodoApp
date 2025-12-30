using System.Net.Http.Json;
using System.Net.Http.Headers;
using TodoApp.Models;

namespace TodoApp.Services;

public class TodoApiService
{
    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://apigateway-production-85e5.up.railway.app/todos"; // Local Gateway
    // For production: "https://apigateway-production-85e5.up.railway.app/todos"

    private string? _token;

    public TodoApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void SetAuthToken(string token)
    {
        _token = token;
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<TodoItem>> GetTodosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TodoItem>>(ApiBaseUrl) ?? new List<TodoItem>();
    }

    public async Task<TodoItem?> GetTodoAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<TodoItem>($"{ApiBaseUrl}/{id}");
    }

    public async Task<TodoItem?> CreateTodoAsync(TodoItem todo)
    {
        var response = await _httpClient.PostAsJsonAsync(ApiBaseUrl, todo);
        return await response.Content.ReadFromJsonAsync<TodoItem>();
    }

    public async Task UpdateTodoAsync(int id, TodoItem todo)
    {
        await _httpClient.PutAsJsonAsync($"{ApiBaseUrl}/{id}", todo);
    }

    public async Task DeleteTodoAsync(int id)
    {
        await _httpClient.DeleteAsync($"{ApiBaseUrl}/{id}");
    }
}

