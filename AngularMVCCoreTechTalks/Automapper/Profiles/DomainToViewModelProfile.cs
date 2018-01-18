using AngularMVCCoreTechTalks.ViewModels;
using AutoMapper;
using DataAccess.Entities;

namespace AngularMVCCoreTechTalks.Automapper.Profiles
{
    /// <summary>
    /// The class represents automapper from talk to talk view model.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class TalkToTalkViewModelProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TalkToTalkViewModelProfile"/> class.
        /// </summary>
        public TalkToTalkViewModelProfile()
        {
            CreateMap<Talk, TalkViewModel>()
                .ForMember(
                dest => dest.SpeakerName,
                opt => opt.MapFrom(src => $"{src.Speaker.FirstName} {src.Speaker.LastName}")
                )
                .ForMember(
                    dest => dest.DisciplineName,
                    opt => opt.MapFrom(src => src.Discipline.DisciplineName)
                );
        }
    }
}