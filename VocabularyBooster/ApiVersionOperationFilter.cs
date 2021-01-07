using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VocabularyBooster
{
    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            ApiVersion apiVersion = context.ApiDescription.GetApiVersion();

            // If the api explorer did not capture an API version for this operation then the action must be API
            // version-neutral, so there's nothing to add.
            if (apiVersion.MajorVersion == null)
            {
                return;
            }

            IList<OpenApiParameter> parameters = operation.Parameters;

            if (parameters == null)
            {
                operation.Parameters = parameters = new List<OpenApiParameter>();
            }

            // Note: In most applications, service authors will choose a single, consistent approach to how API
            // versioning is applied. We support
            // URL path segment with the route parameter name "api-version".

            OpenApiParameter parameter = parameters.FirstOrDefault(p => p.Name == "api-version");

            if (parameter == null)
            {
                // the only other method in this sample is by query string
                parameter = new OpenApiParameter
                {
                    Name = "api-version",
                    Required = true,
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema { Type = "string", Default = new OpenApiString(apiVersion.ToString()) }
                };
                parameters.Add(parameter);
            }
            else if (parameter.In == ParameterLocation.Query || parameter.In == ParameterLocation.Path || parameter.In == ParameterLocation.Header)
            {
                // Update the default value with the current API version so that the route can be invoked in the
                // "Try It!" feature.
                parameter.Schema.Default = new OpenApiString(apiVersion.ToString());
            }

            parameter.Description = "The requested API version";
        }
    }
}
