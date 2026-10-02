namespace DocuScope.Core;

public class WebScraper
{
    private readonly HttpClient _client = new();
    public WebScraper()
    {
        _client.DefaultRequestHeaders.Add("User-Agent", "DocusScope/1.0");
    }
    public async Task<string?> FetchAsync(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            if (result == null)
            {
                return null;
            }
            return result;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {

            Console.WriteLine("Could not fetch the page. Please check the URL.");
            return null;

        }
        catch
        {
            Console.WriteLine("Could not fetch the page. Please check the URL.");
            return null;
        }
    }
}