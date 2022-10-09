namespace testHttpClient.Model;

public class Todo
{
    public Guid Id { get; set; }
    public string Category { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public long Date { get; set; }
    public bool IsCompleted { get; set; }
}