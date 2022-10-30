using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DDOAPI
{
    internal static class Scanner
    {
        // Regex pattern to check if the html file contains a match for a query
        private static Regex basicInfoMatch = new Regex("<span class=\"match\".+<\\/div>");

        // Regex pattern to remove all of the html styling, and capsuling all of the words in groups after
        private static Regex removeHtml = new Regex(">([^<>]+)<");

        public static bool IsValidSearch(string text)
        {
            // Testing if the searh have found a match
            if (basicInfoMatch.IsMatch(text)) return true;


            return false;
        }

        
        public static List<Word>? GetMatches(out bool match, string word)
        {
            // Gets all of the matches of the search query


            var words = new List<Word>();
            string text = WebManager.DownloadString(word);

            // Seeing if the search turns up an Error
            if (!IsValidSearch(text))
            {
                match = false;
                return null;
            }

            // Variables for creating the Word class
            string name;
            string wordClass;
            string identifier;

            // Variables to use in the regex search
            string matchText;
            List<string> matchSearch;


            // First base search to find the first matching word - - Incase that this is the only occurence
            matchText = basicInfoMatch.Match(text).ToString();
            matchSearch = GetGroups(matchText);

            // Variable values for the word class in accordance to the groups from the regex
            name = matchSearch[0];
            wordClass = matchSearch[^1];

            words.Add(new Word(name, wordClass, 1));



            int i = 2;
            while (true)
            {
                // Continuing from the base search to find all of the other definitions of the same query

                // Searching to find another definition
                string query = $"{word},{i}";
                string search = WebManager.DownloadString(query);
                bool canContinue = IsValidSearch(search);
                if (!canContinue) break;

                matchText = basicInfoMatch.Match(search).ToString();
                matchSearch = GetGroups(matchText);

                // Variable values for the word class in accordance to the groups from the regex
                name = matchSearch[0];
                wordClass = matchSearch[^1];
                identifier = matchSearch[^3];

                Word newWord = new Word(name, wordClass, int.Parse(identifier));
                
                if (!words.Contains(newWord))
                {
                    words.Add(newWord);
                }


                i++;
            }

            match = true;
            return words;


        }

        private static List<string> GetGroups(string text)
        {
            // Get the value of the different groups which are captured by the regular expression

            List<string> matchInfo = new List<string>();

            var matchEnum = removeHtml.Matches(text).GetEnumerator();

           
            while (matchEnum.MoveNext()) matchInfo.Add(ToASCII(((Match)matchEnum.Current).Groups.Values.ToList()[1].Value));

            return matchInfo;

        }

        private static string ToASCII(string word)
        {

            // Adds æøå
            word = word.Replace("&#230;", "æ");
            word = word.Replace("&#198;", "Æ");
            word = word.Replace("&#197;", "Å");
            word = word.Replace("&#229;", "å");
            word = word.Replace("&#216;", "Ø");
            word = word.Replace("&#248;", "ø");

            return word;
        }




    }
}
