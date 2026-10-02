using HtmlAgilityPack;

namespace DocuScope.Core;

public class HtmlExtractor
{
    public List<string> ExtractParagraphs(string RawHtml)
    {
        var Data = new List<string>();
        if(RawHtml == null)
        {
            return Data;
        }
        var doc = new HtmlDocument();
        doc.LoadHtml(RawHtml);
        var nodes = doc.DocumentNode.SelectNodes("//p");
        
        if(nodes != null)
        {
            foreach(var node in nodes)
            {
                string cleanText = HtmlEntity.DeEntitize(node.InnerText).Trim();
                if (cleanText.Length <= 1)
                {
                    continue;
                }
                Data.Add(cleanText);
            }
        }
        else
        {
            return Data;
        }

        return Data;
    }
}