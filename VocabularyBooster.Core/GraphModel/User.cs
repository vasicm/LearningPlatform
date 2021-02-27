using System;
using System.Collections.Generic;
using System.Text;

namespace VocabularyBooster.Core.GraphModel
{
    public class User : GraphNodeBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
