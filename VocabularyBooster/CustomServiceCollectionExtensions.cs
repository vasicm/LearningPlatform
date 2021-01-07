using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using VocabularyBooster.Data.Graph;
using VocabularyBooster.Options;

namespace VocabularyBooster
{
    public static class CustomServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .Configure<GraphDbOptions>(configuration.GetSection(nameof(ApplicationOptions.GraphDbOptions)))
                .AddSingleton(x => x.GetRequiredService<IOptions<GraphDbOptions>>().Value);
        }

        /// <summary>
        ///     Adds Swagger services and configures the Swagger services.
        /// </summary>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSwaggerGen(
                options =>
                {
                    Assembly assembly = typeof(Startup).Assembly;
                    string assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                    string assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

                    options.DescribeAllParametersInCamelCase();
                    //options.DescribeStringEnumsInCamelCase();
                    options.EnableAnnotations();

                    // Add the XML comment file for this assembly, so it's contents can be displayed.
                    options.IncludeXmlCommentsIfExists(assembly);

                    options.OperationFilter<ApiVersionOperationFilter>();

                    // if behind proxy we might need to insert prefix
                    // string prefix = configuration.GetSection(nameof(ApplicationSettings))[nameof(ApplicationSettings.SwaggerPathPrefixToInsert)];
                    options.DocumentFilter<PathPrefixInsertDocumentFilter>(string.Empty);

                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (ApiVersionDescription apiVersionDescription in provider.ApiVersionDescriptions)
                    {
                        var info = new OpenApiInfo
                        {
                            Title = assemblyProduct,
                            Description = apiVersionDescription.IsDeprecated
                                ? $"{assemblyDescription} This API version has been deprecated."
                                : assemblyDescription,
                            Version = apiVersionDescription.ApiVersion.ToString()
                        };
                        options.SwaggerDoc(apiVersionDescription.GroupName, info);
                    }
                })
                .AddSwaggerGenNewtonsoftSupport(); // explicit opt-in
        }

        public static SwaggerGenOptions IncludeXmlCommentsIfExists(this SwaggerGenOptions options, Assembly assembly)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            string filePath = Path.ChangeExtension(assembly.Location, ".xml");
            if (!IncludeXmlCommentsIfExists(options, filePath) && assembly.CodeBase != null)
            {
                filePath = Path.ChangeExtension(new Uri(assembly.CodeBase).AbsolutePath, ".xml");
                IncludeXmlCommentsIfExists(options, filePath);
            }

            return options;
        }

        /// <summary>
        ///     Includes the XML comment file if it exists at the specified file path.
        /// </summary>
        /// <param name="options">The Swagger options.</param>
        /// <param name="filePath">The XML comment file path.</param>
        /// <returns><c>true</c> if the comment file exists and was added, otherwise <c>false</c>.</returns>
        /// <exception cref="System.ArgumentNullException">options or filePath.</exception>
        public static bool IncludeXmlCommentsIfExists(this SwaggerGenOptions options, string filePath)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (File.Exists(filePath))
            {
                options.IncludeXmlComments(filePath);
                return true;
            }

            return false;
        }

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(
                options =>
                {
                    // no implicit version
                    options.AssumeDefaultVersionWhenUnspecified = false;
                    // support only url segment version reader
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                    // report api versions in response header
                    //options.ReportApiVersions = true;
                });
        }
    }
}
