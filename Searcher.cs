using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DDOAPI
{
    internal static class Searcher
    {
        public static IHtmlCollection<IElement>? GetMactches(out bool anyMatches, IDocument document)
        {
            anyMatches = false;

            var definitionBox = document?.QuerySelector(".definitionBoxTop");
            if (definitionBox == null)
            {
                return null;
            }

            var matches = definitionBox.QuerySelectorAll(".match");
            if (matches == null || matches.Length <= 0)
            {
                return null;
            }

            anyMatches = true;
            return matches;
        }

        public static bool HasSuper(IElement element)
        {
            return element.ChildElementCount > 0;
        }

        public static Word? Search(out bool validSearch, out bool hasSuper, string query, int identifier = 1)
        {
            hasSuper = false;
            validSearch = false;
            var documentTask = WebManager.DownloadDocument(query);

            documentTask.Wait();
            var document = documentTask.Result;
            if (document == null)
            {
                return null;
            }

            var matches = GetMactches(out bool anyMatches, document);
            if (!anyMatches)
            {
                return null;
            }

            
            foreach (var match in matches)
            {
                
                if (HasSuper(match))
                {
                    hasSuper = true;
                }

            }

            

            validSearch = true;
            var name = GetName(matches);
            

            return new Word(name, "", identifier);
        }

        public static List<Word> MultipleSearch(string query)
        {
            var words = new List<Word>();

            int i = 1;
            while (true)
            {
                string newQuery = $"{query},{i}";
                Word ?word = Search(out bool validSearch, out bool hasSuper, newQuery, i);
                if (!validSearch || word == null)
                {
                    break;
                }

                words.Add(word);

                if (!hasSuper)
                {
                    break;
                }



                if (i > 9) break;

                i++;

            }
            return words;
        }

        public static string GetName(IHtmlCollection<IElement> elements)
        {
            string name = "";
            for (int i = 0; i < elements.Length; i++)
            {

                if (i > 0) name += " eller ";
                
                var firstChild = elements[i].FirstChild;
                if (firstChild == null)
                {
                    name += elements[i].TextContent;
                }
                else
                {
                    name += firstChild.TextContent;
                }


                
            }

            return name;
        }


    }
}
