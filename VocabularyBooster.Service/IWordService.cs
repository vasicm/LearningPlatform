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

        Task AddOrUpdateWord(Word word);

        Task<Word> GetWord(string expression);

        Task MakeTextLearned(Guid userUuid, Guid textUuid);
    }
}
