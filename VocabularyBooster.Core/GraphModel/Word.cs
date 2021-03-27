using System.Collections.Generic;

namespace VocabularyBooster.Core.GraphModel
{
    public class Word : GraphNodeBase
    {
        public string Expression { get; set; }

        public List<Sense> Sense { get; set; }
    }
}
