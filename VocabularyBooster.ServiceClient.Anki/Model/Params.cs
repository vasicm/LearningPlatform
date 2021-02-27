using System.Collections.Generic;

namespace VocabularyBooster.ServiceClient.Anki.Model
{
    public class Params : Dictionary<string, object>
    {
        public Params AddParameter<T>(string key, T value)
        {
            this.Add(key, value);
            return this;
        }
    }
}