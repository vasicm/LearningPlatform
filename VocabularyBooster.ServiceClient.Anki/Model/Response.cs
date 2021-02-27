namespace VocabularyBooster.ServiceClient.Anki.Model
{
    public class Response<T>
    {
        public T Result { get; set; }

        public object Error { get; set; }
    }
}