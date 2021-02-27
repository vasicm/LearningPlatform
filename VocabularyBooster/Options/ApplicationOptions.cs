using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VocabularyBooster.Data.Graph;
using VocabularyBooster.ServiceClient.Anki;

namespace VocabularyBooster.Options
{
    public class ApplicationOptions
    {
        public GraphDbOptions GraphDbOptions { get; set; }

        public AnkiApiOptions AnkiApiOptions { get; set; }
    }
}
