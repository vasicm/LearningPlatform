using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace VocabularyBooster.Mapping
{
    public static class AutoMapperConfig
    {
        public static IMapper CreateMapping()
        {
           return new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
        }
    }
}
