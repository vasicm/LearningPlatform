using System;
using System.Collections.Generic;
using System.Text;

namespace VocabularyBooster.Core.GraphModel
{
    public class Card : GraphNodeBase
    {
        public long CardId { get; set; }

        public string Number { get; set; }

        public string Img { get; set; }

        public string English { get; set; }

        public string Keyword { get; set; }

        public string Transcription { get; set; }

        public string Russian { get; set; }

        public string Sound { get; set; }

        public string AmTranscription { get; set; }

        public string BrTranscription { get; set; }

        public string AmBrTranscription { get; set; }

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
