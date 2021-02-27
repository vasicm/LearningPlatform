namespace VocabularyBooster.FlashcardsService.Interface.Model
{
    public class FlashCard
    {
        public long CardId { get; set; }

        public Fields Fields { get; set; }

        public int FieldOrder { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string ModelName { get; set; }

        public int Ord { get; set; }

        public string DeckName { get; set; }

        public string Css { get; set; }

        public int Factor { get; set; }

        public int Interval { get; set; }

        public long Note { get; set; }

        public int Type { get; set; }

        public int Queue { get; set; }

        public int Due { get; set; }

        public int Reps { get; set; }

        public int Lapses { get; set; }

        public int Left { get; set; }
    }
}
