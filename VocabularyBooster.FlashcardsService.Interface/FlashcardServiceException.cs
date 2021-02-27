using System;

namespace VocabularyBooster.FlashcardsService.Interface
{
    public class FlashcardServiceException : Exception
    {
        public FlashcardServiceException(string message)
            : base(message)
        {
        }
    }
}
