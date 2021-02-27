using Newtonsoft.Json.Serialization;

namespace VocabularyBooster.Core
{
    public class FirstCharacterLowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        }
    }
}