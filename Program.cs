using AngleSharp.Dom;
using DDOCrawler;


Console.WriteLine("Hvilket ord vil du slå op?");
var input = Console.ReadLine();
if (input == null)
{
    throw new Exception("Input is null");
}

var word = Searcher.Search(out bool validSearch, out bool hasSuper, input);

if (!validSearch || word == null)
{
    Console.WriteLine("This word does not excist in the DOO");
    return;
}

Console.WriteLine(word.ToString());
if (hasSuper)
{
    Console.WriteLine("This word has mulitple definitions");
    Console.WriteLine("Do you whish to see all definitions? (yes/y)");
    var answer = Console.ReadLine();

    if (answer == "yes" || answer == "y")
    {
        Console.WriteLine("All definitions:");
        Searcher.MultipleSearch(input).ForEach(w => Console.WriteLine(w.ToString()));
    }
}

    














