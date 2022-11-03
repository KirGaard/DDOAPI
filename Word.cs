using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOCrawler
{
    internal class Word
    {
        public string name;
        public int nameIdentifier;
        public string wordClass;
    
        public Word(string name)
        {
            this.name = name;
            this.wordClass = "";
            this.nameIdentifier = 1;
        }
        public Word(string name, string wordClass)
        {
            this.name = name;
            this.wordClass = wordClass;
            this.nameIdentifier = 1;

        }

        public Word(string name, string wordClass, int nameIdentifier) : this(name, wordClass) { 
            this.nameIdentifier = nameIdentifier;

        
        }



        public override string ToString()
        {
            return "Definition: " + nameIdentifier + " | " + name + " | " + wordClass;


        }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Word other) return false;


            return other.name == name && other.nameIdentifier == nameIdentifier;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
