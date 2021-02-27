namespace VocabularyBooster.ServiceClient.Anki.Model
{
    public class Request
    {
        public string Action { get; set; }

        public int Version { get; set; }

        public Params Params { get; set; }
    }
}