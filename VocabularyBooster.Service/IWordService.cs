using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VocabularyBooster.Core.GraphModel;

namespace VocabularyBooster.Service
{
    public interface IWordService
    {
        Task<Guid> AddText(Text text);

        Task<Text> GetText(Guid textUuid);

        Task<List<Text>> SearchText(string phrase);

        Task<List<Word>> GetWordListFromText(Guid textUuid);

        Task AddOrUpdateWord(Word word);

        Task<Word> GetWord(string expression);

        Task<List<Word>> SearchWord(string expression);

        Task MakeTextLearned(Guid userUuid, Guid textUuid);
    }
}
