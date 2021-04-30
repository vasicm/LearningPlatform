using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VocabularyBooster.ViewModel
{
    public class Word
    {
        public string Expression { get; set; }

        public List<Sense> Sense { get; set; }
    }
}
