using System;
using System.Collections.Generic;
using System.Text;

namespace VocabularyBooster.Data.Graph.Helper
{
    public class ParameterDictionary : Dictionary<string, object>
    {
        public ParameterDictionary()
        {
        }

        public new object this[string key]
        {
            get => this[key];

            set
            {
                ParameterDictionary parameterDictionary = this;
                parameterDictionary.AddParameter(key, value);
            }
        }

        public ParameterDictionary AddParameter<T>(string key, T value)
        {
            string ns = value?.GetType().Namespace;
            if (ns != null && !ns.StartsWith("System", StringComparison.CurrentCulture))
            {
                this.Add(key, ParameterSerializer.ToDictionary(new[] { value }));
            }
            else
            {
                this.Add(key, value);
            }

            return this;
        }
    }
}
