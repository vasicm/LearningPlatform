using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VocabularyBooster.Core.GraphModel;
using VocabularyBooster.FlashcardsService.Interface.Model;

namespace VocabularyBooster.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<FlashCard, Card>(MemberList.Destination)
                .ForMember(dest => dest.Uuid, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Fields.Number.Value))
                .ForMember(dest => dest.Img, opt => opt.MapFrom(src => src.Fields.Img.Value))
                .ForMember(dest => dest.English, opt => opt.MapFrom(src => src.Fields.English.Value))
                .ForMember(dest => dest.Keyword, opt => opt.MapFrom(src => src.Fields.Keyword.Value))
                .ForMember(dest => dest.Transcription, opt => opt.MapFrom(src => src.Fields.Transcription.Value))
                .ForMember(dest => dest.Russian, opt => opt.MapFrom(src => src.Fields.Russian.Value))
                .ForMember(dest => dest.Sound, opt => opt.MapFrom(src => src.Fields.Sound.Value))
                .ForMember(dest => dest.AmTranscription, opt => opt.MapFrom(src => src.Fields.AmTranscription.Value))
                .ForMember(dest => dest.BrTranscription, opt => opt.MapFrom(src => src.Fields.BrTranscription.Value))
                .ForMember(dest => dest.AmBrTranscription, opt => opt.MapFrom(src => src.Fields.AmBrTranscription.Value));
        }
    }
}
