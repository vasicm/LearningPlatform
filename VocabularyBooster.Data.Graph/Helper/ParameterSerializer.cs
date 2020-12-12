// https://bytefish.de/blog/neo4j_dotnet/
//
// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VocabularyBooster.Data.Graph.Helper
{
    public static class ParameterSerializer
    {
        public static IList<Dictionary<string, object>> ToDictionary<TSourceType>(IList<TSourceType> source)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
                ContractResolver = new FirstCharacterLowercaseContractResolver(),
            };

            string json = JsonConvert.SerializeObject(source, settings);

            return JsonConvert.DeserializeObject<IList<Dictionary<string, object>>>(json, new CustomDictionaryConverter());
        }

        // Useful for unit tests for initialization data. (Pass data trough argument instead of using cql import)
        public static IList<Dictionary<string, object>> ToDictionary(string json)
        {
            return JsonConvert.DeserializeObject<IList<Dictionary<string, object>>>(json, new CustomDictionaryConverter());
        }
    }
}
