using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VocabularyBooster.Core.GraphModel;
using VocabularyBooster.Data.Graph;
using VocabularyBooster.Data.Graph.Helper;

namespace VocabularyBooster.Service
{
    public class WordService : IWordService
    {
        private readonly ILogger<WordService> logger;
        private readonly GraphDbContext graphDbContext;

        public WordService(ILogger<WordService> logger, GraphDbContext graphDbContext)
        {
            this.logger = logger;
            this.graphDbContext = graphDbContext;
        }

        public async Task AddOrUpdateWord(Word word)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "word", ParameterSerializer.ToDictionary(new[] { word }) },
            };
            await this.graphDbContext.WriteTransaction(parameters, WordCypherQueries.AddOrUpdate);
        }

        public async Task<Word> GetWord(string expression)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "expression", expression },
            };
            var users = await this.graphDbContext.ReadTransaction(parameters, WordCypherQueries.GetWord, c => c.AsNodeTo<Word>("Word") );
            return users.SingleOrDefault();
        }
    }
}
