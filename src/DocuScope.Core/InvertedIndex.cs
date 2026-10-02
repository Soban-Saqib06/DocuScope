using System.Security.Cryptography;

namespace DocuScope.Core;

public class InvertedIndex
{
    private Dictionary<string, HashSet<int>> _index = new();
    private List<string> _paragraphs = new();
    private readonly char[] Punctuation = new[] { '.', ',', '!', '?', ';', ':', '"', '\'', '(', ')', '[', ']', '{', '}' };
    public void BuildIndex(List<string> paragraphs)
    {
        _paragraphs = paragraphs;
        _index.Clear();
        for (int i = 0; i < paragraphs.Count(); i++)
        {

            if (paragraphs[i] != null)
            {
                string[] wordArray = paragraphs[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string c in wordArray)
                {
                    string cleanword = c.Trim(Punctuation).ToLower();
                    if (string.IsNullOrWhiteSpace(cleanword))
                    {
                        continue;
                    }
                    //need to implement a check if it is a common word or a pronoun or stuff
                    _index.TryAdd(cleanword, new HashSet<int>());
                    _index[cleanword].Add(i);
                }
            }
        }
    }

    public List<string> Search(string keyword)
    {
        string searchTerm = keyword.Trim(Punctuation).ToLower();
        List<string> Result = new List<string>();
        if(_index.TryGetValue(searchTerm,out HashSet<int>? ID))
        {
            foreach (int i in ID) 
            {
                string text = _paragraphs[i];
                if (!string.IsNullOrWhiteSpace(text))
                {
                    Result.Add(text);                
                } 
            }
            return Result;
        }
        else
        {
            Console.WriteLine("No Results Found");
            return Result;
        }
    }
}