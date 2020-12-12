using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using VocabularyBooster.Service;

namespace VocabularyBooster
{
    public static class ContainerBuilderExtensions
    {
        public static void AddProjectServices(this ContainerBuilder builder)
        {
            builder.RegisterType<WordService>().As<IWordService>();
        }
    }
}
