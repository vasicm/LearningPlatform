using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using VocabularyBooster.Core.GraphModel;
using VocabularyBooster.Data.Graph;
using VocabularyBooster.Service;
using VocabularyBooster.Test.Common;
using Xunit;

namespace VocabularyBooster.Test.Integration
{
    [Collection(nameof(IntegrationCollectionDefinition))]
    public class WordServiceTest
    {
        private readonly TestHostFixture fixture;
        private readonly IWordService wordService;
        private readonly IUserService userService;

        public WordServiceTest(TestHostFixture fixture)
        {
            this.fixture = fixture;
            this.wordService = ServiceProviderAccessor.ServiceProvider.GetService<IWordService>();
            this.userService = ServiceProviderAccessor.ServiceProvider.GetService<IUserService>();
        }

        [Fact]
        public async Task AddOrUpdateWordTest()
        {
            var wordList = Helper.Deserialize<List<Word>>("TestData.WordList.json");

            foreach (var word in wordList)
            {
                await this.wordService.AddOrUpdateWord(word);
                var returnedWord = await this.wordService.GetWord(word.Expression);
            }
        }

        [Fact]
        public async Task AddTextTest()
        {
            var text = new Text()
            {
                // Text after word Lemmatize
                Content = @"If you had to sum up George Washington's life in one word, that word would have to be unforgettable. George's story is one of travel and adventure, full of risks and, most of all, full of glory."
            };

            var textUuid = await this.wordService.AddText(text);
            var words = await this.wordService.GetWordListFromText(textUuid);

            // words.Where(w => w.Expression == "word")
        }

        [Fact]
        public async Task MakeTextLearned()
        {
            var text = new Text()
            {
                // Text after word Lemmatize
                Content = "if you have to sum up george washington 's life in one word , that word would have to be unforgettable"
            };

            var textUuid = await this.wordService.AddText(text);

            var user = new User()
            {
                Email = "user@etfbl.net",
                FirstName = "FirstName",
                LastName = "LastName"
            };

            var userUuid = await this.userService.AddUser(user);

            await this.wordService.MakeTextLearned(userUuid: userUuid, textUuid: textUuid);
        }
    }
}
