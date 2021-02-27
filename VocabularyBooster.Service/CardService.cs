using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VocabularyBooster.Core.GraphModel;
using VocabularyBooster.Data.Graph;
using VocabularyBooster.Data.Graph.Helper;

namespace VocabularyBooster.Service
{
    public class CardService : ICardService
    {
        private readonly ILogger<CardService> logger;
        private readonly GraphDbContext graphDbContext;

        public CardService(ILogger<CardService> logger, GraphDbContext graphDbContext)
        {
            this.logger = logger;
            this.graphDbContext = graphDbContext;
        }

        public async Task<bool> AddOrUpdateCard(List<Card> cards, string userEmail)
        {
            return await this.graphDbContext.WriteTransaction(
                new ParameterDictionary()
                    .AddParameter("cards", ParameterSerializer.ToDictionary(cards))
                    .AddParameter("userEmail", userEmail),
                WordCypherQueries.AddOrUpdateCard,
                result => result.To<bool>("res"));
        }

        public async Task<Card> GetByCardId(long cardId, string userEmail)
        {
            return await this.graphDbContext.WriteTransaction(
                new ParameterDictionary()
                    .AddParameter("cardId", cardId)
                    .AddParameter("userEmail", userEmail),
                WordCypherQueries.GetCardByCardId,
                result => result.To<Card>("card"));
        }
    }
}
