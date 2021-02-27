using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shouldly;
using VocabularyBooster.FlashcardsService.Interface;
using VocabularyBooster.ServiceClient.Anki;
using Xunit;

namespace VocabularyBooster.Test.Integration
{
    [Collection(nameof(IntegrationCollectionDefinition))]
    public class AnkiFlashcardServiceTest
    {
        private readonly TestHostFixture fixture;
        private readonly IFlashcardService flashcardService;

        public AnkiFlashcardServiceTest(TestHostFixture fixture)
        {
            this.fixture = fixture;
            this.flashcardService = ServiceProviderAccessor.ServiceProvider.GetService<IFlashcardService>();
        }

        [Fact]
        public async Task FindCardsTest()
        {
            var res = await this.flashcardService.FindCards("\"deck:SSE 4000 Essential English Words by Paul Nation::book 6\"");
            res.Count.ShouldBe(600);
        }

        [Fact]
        public async Task GetCardsInfoTest()
        {
            var cardIdList = await this.flashcardService.FindCards("\"tag:test\"");
            var flashCardList = await this.flashcardService.GetCardsInfo(cardIdList);
            flashCardList.Count.ShouldBe(4);
        }

        [Fact]
        public async Task DeckTest()
        {
            var newDeckName = "SSE 4000 Essential English Words by Paul Nation::test";
            var orginalDeckName = "SSE 4000 Essential English Words by Paul Nation::book 6";

            var cardIdList = await this.flashcardService.FindCards("\"tag:test\"");
            cardIdList.Count.ShouldBe(4);

            await this.flashcardService.CreateDeck(newDeckName);
            var cardList = await this.flashcardService.FindCards($"\"deck:{newDeckName}\"");
            cardList.Count.ShouldBe(0);
            await this.flashcardService.ChangeDeck(newDeckName, cardIdList);
            cardList = await this.flashcardService.FindCards($"\"deck:{newDeckName}\"");
            cardList.Count.ShouldBe(4);

            await this.flashcardService.ChangeDeck(orginalDeckName, cardIdList);
            cardList = await this.flashcardService.FindCards($"\"deck:{newDeckName}\"");
            cardList.Count.ShouldBe(0);
            //await this.flashcardService.DeleteDecks(new List<string>() { newDeckName }, false);
        }
    }
}
