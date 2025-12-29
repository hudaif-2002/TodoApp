using System.Net.Http.Json;
using TodoApp.Models;

namespace TodoApp.Services;

public class TodoApiService
{
    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://todoapi-production-6ab8.up.railway.app/api/todos";

    public TodoApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
