using System;
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

        public async Task<Guid> AddText(Text text)
        {
            return await this.graphDbContext.WriteTransaction(
                new ParameterDictionary().AddParameter("text", text),
                WordCypherQueries.AddText,
                result => Guid.Parse(result.To<string>("textUuid")));
        }

        public async Task<Text> GetText(Guid textUuid)
        {
            return await this.graphDbContext.ReadTransactionSingle(
                new ParameterDictionary().AddParameter("textUuid", textUuid.ToString()),
                WordCypherQueries.GetText,
                c => c.AsNodeTo<Text>("Text"));
        }

        public async Task AddOrUpdateWord(Word word)
        {
            await this.graphDbContext.WriteTransaction(
                new ParameterDictionary().AddParameter("word", word),
                WordCypherQueries.AddOrUpdate);
        }

        public async Task<Word> GetWord(string expression)
        {
            return await this.graphDbContext.ReadTransactionSingle(
                new ParameterDictionary().AddParameter("expression", expression),
                WordCypherQueries.GetWord,
                c => c.AsNodeTo<Word>(nameof(Word)));
        }

        public async Task MakeTextLearned(Guid userUuid, Guid textUuid)
        {
            await this.graphDbContext.WriteTransaction(
                new ParameterDictionary()
                    .AddParameter("userUuid", userUuid.ToString())
                    .AddParameter("textUuid", textUuid.ToString()),
                WordCypherQueries.MakeTextLearned);
        }
    }
}
