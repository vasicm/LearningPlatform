using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VocabularyBooster
{
    public class PathPrefixInsertDocumentFilter : IDocumentFilter
    {
        private readonly string pathPrefix;

        public PathPrefixInsertDocumentFilter(string prefix)
        {
            this.pathPrefix = prefix;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.Keys.ToList();
            foreach (var path in paths)
            {
                var pathToChange = swaggerDoc.Paths[path];
                swaggerDoc.Paths.Remove(path);
                swaggerDoc.Paths.Add(this.pathPrefix + path, pathToChange);
            }
        }
    }
}
