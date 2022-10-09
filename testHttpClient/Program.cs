using System.Net.Http.Headers;
using System.Net.Http.Json;

class Program
{
    static readonly HttpClient client = new HttpClient();
    private static string _token;

    static async Task Main(string[] args)
    {
        await RunAsync();
     //   Console.WriteLine($"token - {_token}");
    }

    static async Task RunAsync()
    {
        client.BaseAddress = new Uri("http://45.144.64.179/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        
        try
        {
            var userLogin = new UserLogin { email = "qweqwe@qwe.qwe", password = "qweqweqwe" };
            await LoginUserAsync(userLogin);
            await GetTodos();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    static async Task LoginUserAsync(UserLogin userLogin)
    {
        var response = await client.PostAsJsonAsync(
            "api/auth/login", userLogin);
        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            _token = response.Content.ReadAsAsync<Token>().Result.access_token;
        }
    }

    static async Task GetTodos()
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await client.GetFromJsonAsync<List<Todo>>("api/todos");
        foreach (var value in response)
        {
            Console.WriteLine("/---------------------------------/");
            Console.WriteLine("Category - " + value.Category);
            Console.WriteLine("Description -" + value.Description);
            Console.WriteLine("IsCompleted - " + value.IsCompleted);
            Console.WriteLine("Date - " + value.Date);
        }
    }
}


public class UserLogin
{
    public string email { get; set; }
    public string password { get; set; }
}

public class Token
{
    public string access_token { get; set; }
}

public class Todo
{
    public Guid Id { get; set; }
    public string Category { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public long Date { get; set; }
    public bool IsCompleted { get; set; }
}