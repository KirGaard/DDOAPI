using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOAPI
{
    internal class Word
    {
        public string name;
        public int nameIdentifier = 0;
        public string wordClass;

        public Word(string name, string wordClass)
        {
            this.name = name;
            this.wordClass = wordClass; 


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
            if (obj == null) return false;
            
            var other = obj as Word;
            return other.name == name && other.nameIdentifier == nameIdentifier;
        }



    }
}
