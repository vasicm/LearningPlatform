using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using VocabularyBooster.FlashcardsService.Interface;
using VocabularyBooster.Service;
using VocabularyBooster.ServiceClient.Anki;

namespace VocabularyBooster
{
    public static class ContainerBuilderExtensions
    {
        public static void AddProjectServices(this ContainerBuilder builder)
        {
            builder.RegisterType<WordService>().As<IWordService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<AnkiFlashcardService>().As<IFlashcardService>();
        }
    }
}
