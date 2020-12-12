using System;
using System.Collections.Generic;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace VocabularyBooster.Data.Graph.Helper
{
    public static class RecordExtension
    {
        public static T AsNodeTo<T>(this IRecord record, string keyName)
        {
            if (keyName == null)
            {
                throw new ArgumentNullException(nameof(keyName));
            }

            if (record[keyName] == null)
            {
                return default(T);
            }

            var recordItem = record[keyName];

            string nodeProps = JsonConvert.SerializeObject(recordItem.As<INode>().Properties);
            return JsonConvert.DeserializeObject<T>(nodeProps);
        }

        public static T To<T>(this object record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            string nodeProps = JsonConvert.SerializeObject(record);
            return JsonConvert.DeserializeObject<T>(nodeProps);
        }

        public static T To<T>(this IRecord record, string keyName)
        {
            if (keyName == null)
            {
                throw new ArgumentNullException(nameof(keyName));
            }

            if (record[keyName] == null)
            {
                return default(T);
            }

            string nodeProps = JsonConvert.SerializeObject(record[keyName]);
            return JsonConvert.DeserializeObject<T>(nodeProps);
        }

        public static List<T> ToList<T>(this IRecord record, string keyName)
        {
            if (keyName == null)
            {
                throw new ArgumentNullException(nameof(keyName));
            }

            if (record[keyName] == null)
            {
                return default(List<T>);
            }

            var values = (IList<object>)record.Values[keyName];
            var list = new List<T>(values.Count);

            foreach (var value in values)
            {
                string nodeProps = JsonConvert.SerializeObject(value);
                var item = JsonConvert.DeserializeObject<T>(nodeProps);
                list.Add(item);
            }

            return list;
        }

        public static string AsString(this IRecord record, string keyName)
        {
            if (keyName == null)
            {
                throw new ArgumentNullException(nameof(keyName));
            }

            string ret = record[keyName].As<string>();

            if (ret == null)
            {
                return string.Empty;
            }

            return ret;
        }
    }
}
