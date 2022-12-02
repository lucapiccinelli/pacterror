namespace consolex86;

public class MyService
{
    public static Task<string> GetAsync(string address)
    {
        HttpClient client = new HttpClient();
        return client.GetAsync(address).Result.Content.ReadAsStringAsync();
    }
}