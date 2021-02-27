﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace VocabularyBooster.Test.Integration
{
    [CollectionDefinition(nameof(IntegrationCollectionDefinition))]
    public class IntegrationCollectionDefinition : ICollectionFixture<TestHostFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
