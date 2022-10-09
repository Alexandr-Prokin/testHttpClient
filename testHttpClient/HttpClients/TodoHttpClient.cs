using System.Net.Http.Headers;
using System.Net.Http.Json;
using testHttpClient.Data;
using testHttpClient.Model;

namespace testHttpClient.HttpClients;

public interface ITodoHttpClient
{
    Task<List<Todo?>> GetTodosAsync();
    Task UserLogin(UserLogin userLogin);
}

public class TodoHttpClient : ITodoHttpClient
{
    private readonly HttpClient _httpClient;
    private const string TodosUrl = "api/todos";
    private const string LoginUrl = "api/auth/login";

    public TodoHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Todo?>> GetTodosAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", TokenStorage.GetToken());
        return await _httpClient.GetFromJsonAsync<List<Todo>>(TodosUrl);
    }

    public async Task UserLogin(UserLogin userLogin)
    {
        var response = await _httpClient.PostAsJsonAsync(LoginUrl, userLogin);
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            TokenStorage.SetToken(response.Content.ReadAsAsync<Token>().Result.access_token);
        }
    }
}