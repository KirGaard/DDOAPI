

using DDOAPI;


Console.WriteLine("Hvilket ord ønsker du at slå op?");


string ?query = Console.ReadLine();
if (query == null) 
{ 
    Console.WriteLine("This is not a word");
    return;
}


var words = Scanner.GetMatches(out bool match, query);




if (match)
{
    words?.ForEach(w => Console.WriteLine(w));
}
else
{
    Console.WriteLine("This word does not exist in DDO");
}


