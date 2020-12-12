using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
    }
}
