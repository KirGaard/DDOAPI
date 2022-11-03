using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DDOCrawler
{
    internal static class Searcher
    {
        public static IHtmlCollection<IElement>? GetMactches(out bool anyMatches, IElement definitionBox)
        {
            anyMatches = false;

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


            var definitionBox = document.QuerySelector(".definitionBoxTop");
            if (definitionBox == null) return null;

            var matches = GetMactches(out bool anyMatches, definitionBox);
            if (!anyMatches || matches == null)
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
            var wordClass = GetWordClass(definitionBox);

            return new Word(name, wordClass, identifier);
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

        private static string GetName(IHtmlCollection<IElement> matches)
        {
            string name = "";
            for (int i = 0; i < matches.Length; i++)
            {

                if (i > 0) name += " eller ";
                
                var firstChild = matches[i].FirstChild;
                if (firstChild == null)
                {
                    name += matches[i].TextContent;
                }
                else
                {
                    name += firstChild.TextContent;
                }


                
            }

            return name;
        }
        private static string GetWordClass(IElement definitionBox)
        {
            if (definitionBox.LastChild == null) throw new Exception("Definition box does not contain a last child");
            if (definitionBox.QuerySelector(".definitionBox") == null)
            {
                return definitionBox.LastChild.TextContent;
            }
            else
            {
                
                return definitionBox.Children[^2].TextContent;
            }


            
        }
    }


}
