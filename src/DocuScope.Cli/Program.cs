using System.Data;
using DocuScope.Core;

Console.WriteLine("========Welcoe to DocuScope=======\n");

Console.WriteLine("Enter your Url:\n ");
string? url;
url = Console.ReadLine();
while (url != null)
{
    var engine = new SearchEngine();
    Console.WriteLine("We are working to extract data from your site. Please wait.....\n");
    bool check = await engine.IndexUrlAsync(url);
    if (!check)
    {
        Console.WriteLine("Invalid Url or Data\n");
        break;
    }
    else
    {
        Console.WriteLine("Your data has now been indexed and stored.\n");
    }
    string? input = "1";
    while (input != " " && input != "exit")
    {
        Console.WriteLine("Enter search keyword: ");
        input = Console.ReadLine();
        if (input != null && input != "exit" && input != " ")
        {
            List<string> Answer = engine.SearchIndex(input);
            if (Answer.Count == 0)
            {
                Console.WriteLine("No matches found for that keyword.\n");
            }
            else
            {
                Console.WriteLine($"Found {Answer.Count} matching paragraph(s):\n");
                foreach (string c in Answer)
                {
                    Console.WriteLine($"- {c}\n");
                }
            }
        }
    }
    url = null;
}
