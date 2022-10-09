using Microsoft.Extensions.DependencyInjection;
using testHttpClient.Data;
using testHttpClient.HttpClients;
using testHttpClient.Model;

namespace testHttpClient;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureService(serviceCollection);
        var service = serviceCollection.BuildServiceProvider();
        var todoClient = service.GetRequiredService<ITodoHttpClient>();
        await todoClient.UserLogin(new UserLogin
        {
            email = "qweqwe@qwe.qwe",
            password = "qweqweqwe"
        });
        
        Console.WriteLine("Token - " + TokenStorage.GetToken());
        var responseTodos = await todoClient.GetTodosAsync();

        foreach (var value in responseTodos)
        {
            Console.WriteLine("/---------------------------------/");
            Console.WriteLine("Category - " + value.Category);
            Console.WriteLine("Description -" + value.Description);
            Console.WriteLine("IsCompleted - " + value.IsCompleted);
            Console.WriteLine("Date - " + value.Date);
        }
    }

    private static void ConfigureService(ServiceCollection service)
    {
        service.AddHttpClient<ITodoHttpClient, TodoHttpClient>(options =>
        {
            options.BaseAddress = new Uri("http://45.144.64.179/");
        });
    }
}