namespace testHttpClient.Data;

public static class TokenStorage
{
    private static string _value { get; set; }

    public static string GetToken()
    {
        return _value;
    } 
    
    public static void SetToken(string token)
    {
        _value = token;
    } 
    public static void ClearToken()
    {
        _value = "token empty";
    }
}