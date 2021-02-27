using System;
using System.Collections.Generic;
using System.Text;

namespace VocabularyBooster.Core.GraphModel
{
    public class Text : GraphNodeBase
    {
        public string Title { get; set; }

        public int Type { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }
    }
}
