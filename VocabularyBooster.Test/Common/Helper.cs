using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace VocabularyBooster.Test.Common
{
    public static class Helper
    {
        public static string GetManifestResourceName(string resourceName)
        {
            return Assembly.GetExecutingAssembly().
                GetManifestResourceNames().
                Single(str => str.EndsWith(resourceName, StringComparison.InvariantCulture));
        }

        public static Stream GetManifestResourceStream(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetManifestResourceName(resourceName));
        }

        public static string GetManifestResourceString(string resourceName)
        {
            string str = string.Empty;
            using (StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(GetManifestResourceName(resourceName))))
            {
                str = sr.ReadToEnd().Replace(Environment.NewLine, " ", StringComparison.InvariantCulture);
            }

            return str;
        }

        public static T Deserialize<T>(string resourceName)
        {
            return JsonConvert.DeserializeObject<T>(GetManifestResourceString(resourceName));
        }
    }
}
