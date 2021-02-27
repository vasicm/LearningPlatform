using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VocabularyBooster.Core.GraphModel;

namespace VocabularyBooster.Service
{
    public interface ICardService
    {
        Task<bool> AddOrUpdateCard(List<Card> cards, string userEmail);

        Task<Card> GetByCardId(long cardId, string userEmail);
    }
}
