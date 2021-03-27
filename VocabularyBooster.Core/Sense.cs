using System;
using System.Collections.Generic;
using System.Text;
using VocabularyBooster.Core.GraphModel;

namespace VocabularyBooster.Core
{
    public class Sense : GraphNodeBase
    {
        public string Definition { get; set; }

        public string GrammaticalCategories { get; set; }

        public string Example { get; set; }

        public string Cefr { get; set; }

        public string Thesaurus { get; set; }

        public string Callocations { get; set; }

        public string Topic { get; set; }
    }
}
