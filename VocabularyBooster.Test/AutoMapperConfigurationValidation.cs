using System;
using System.Collections.Generic;
using System.Text;
using VocabularyBooster.Mapping;
using Xunit;

namespace VocabularyBooster.Test
{
    public class AutoMapperConfigurationValidation
    {
        [Fact]
        public void ValidateConfiguration()
        {
            AutoMapperConfig.CreateMapping().ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
