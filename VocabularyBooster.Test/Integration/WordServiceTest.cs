using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using VocabularyBooster.Core.GraphModel;
using VocabularyBooster.Service;
using Xunit;

namespace VocabularyBooster.Test.Integration
{
    [Collection("Integration")]
    public class WordServiceTest
    {
        private readonly TestHostFixture fixture;
        private readonly IWordService wordService;

        public WordServiceTest(TestHostFixture fixture)
        {
            this.fixture = fixture;
            this.wordService = ServiceProviderAccessor.ServiceProvider.GetService<IWordService>();
        }

        [Fact]
        public async Task Test()
        {
            var word = new Word()
            {
                Number = 456,
                Expression = "test",
                Definition = "a set of questions or exercises for measuring your knowledge of a subject, or your skill"
            };

            await this.wordService.AddOrUpdateWord(word);

            var returnedWord = await this.wordService.GetWord(word.Expression);
            returnedWord.Definition.ShouldBe(word.Definition);
        }
    }
}
