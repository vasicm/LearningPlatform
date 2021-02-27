using System.Collections.Generic;
using System.Threading.Tasks;
using VocabularyBooster.FlashcardsService.Interface.Model;

namespace VocabularyBooster.FlashcardsService.Interface
{
    public interface IFlashcardService
    {
        Task<List<long>> FindCards(string query);

        Task<List<FlashCard>> GetCardsInfo(List<long> cardIdList);

        Task<long> CreateDeck(string deckName);

        Task DeleteDecks(List<string> deckNameList, bool cardsToo);

        Task ChangeDeck(string deckName, List<long> cardIdList);
    }
}
