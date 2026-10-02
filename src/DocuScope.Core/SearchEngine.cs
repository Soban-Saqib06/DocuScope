namespace DocuScope.Core;

public class SearchEngine
{
    private readonly WebScraper _scraper = new();
    private readonly HtmlExtractor _extractor = new();
    private readonly InvertedIndex _index = new();

    public async Task<bool> IndexUrlAsync(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return false;
        }
        string? rawHtml= await _scraper.FetchAsync(url);
        if(rawHtml == null)
        {
            Console.WriteLine("Invalid url or site\n");
            return false;
        }
        List<string> Data = _extractor.ExtractParagraphs(rawHtml);
        if(Data == null)
        {
            Console.WriteLine("Operation couldn't be performed on site.\n");
            return false;
        }
        _index.BuildIndex(Data);

        return true;
    }

    public List<string> SearchIndex(string keyword)
    {
        List<string> Result = _index.Search(keyword);
        return Result;
    }
}