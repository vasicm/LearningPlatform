using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using VocabularyBooster.Core.GraphModel;
using VocabularyBooster.FlashcardsService.Interface;
using VocabularyBooster.Service;
using Xunit;

namespace VocabularyBooster.Test.Integration
{
    [Collection(nameof(IntegrationCollectionDefinition))]
    public class CardServiceTest
    {
        private readonly TestHostFixture fixture;
        private readonly ICardService cardService;
        private readonly IFlashcardService flashcardService;
        private readonly IMapper mapper;

        public CardServiceTest(TestHostFixture fixture)
        {
            this.fixture = fixture;
            this.cardService = ServiceProviderAccessor.ServiceProvider.GetService<ICardService>();
            this.flashcardService = ServiceProviderAccessor.ServiceProvider.GetService<IFlashcardService>();
            this.mapper = ServiceProviderAccessor.ServiceProvider.GetService<IMapper>();
        }

        [Fact]
        public async Task AddOrUpdateCardTest()
        {
            var userEmail = "vasic.marko@mail.ru";
            var cardIdList = await this.flashcardService.FindCards("\"tag:test\"");
            var flashCardList = await this.flashcardService.GetCardsInfo(cardIdList);

            var carList = flashCardList.Select(fc => this.mapper.Map<Card>(fc)).ToList();
            await this.cardService.AddOrUpdateCard(carList, userEmail);

            foreach (var expectedCard in flashCardList)
            {
                var card = await this.cardService.GetByCardId(expectedCard.CardId, userEmail);
                card.Keyword.ShouldBe(card.Keyword);
                card.Question.ShouldBe(card.Question);
                card.Answer.ShouldBe(card.Answer);
            }
        }
    }
}
