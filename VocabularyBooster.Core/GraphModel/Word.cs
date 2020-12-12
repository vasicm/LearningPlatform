namespace VocabularyBooster.Core.GraphModel
{
    public class Word : GraphNodeBase
    {
        public int Number { get; set; }

        public string Expression { get; set; }

        public string Definition { get; set; }

        public string GrammaticalCategories { get; set; }

        public string Example { get; set; }

        public string CEFR { get; set; }

        public string GSE { get; set; }

        public string Thesaurus { get; set; }

        public string Callocations { get; set; }

        public string Topic { get; set; }
    }
}
