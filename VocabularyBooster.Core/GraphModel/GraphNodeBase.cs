using System;

namespace VocabularyBooster.Core.GraphModel
{
    public abstract class GraphNodeBase
    {
        public Guid Uuid { get; set; }

        public string Created { get; set; }
    }
}
