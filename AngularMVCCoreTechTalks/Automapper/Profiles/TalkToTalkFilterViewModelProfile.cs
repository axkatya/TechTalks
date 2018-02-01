using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;
using AngularMVCCoreTechTalks.ViewModels;

namespace AngularMVCCoreTechTalks.Automapper.Profiles
{
    /// <summary>
    /// The class represents automapper from talk collection to talk filter view model.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class TalkToTalkFilterViewModelProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TalkToTalkFilterViewModelProfile"/> class.
        /// </summary>
        public TalkToTalkFilterViewModelProfile()
        {
            CreateMap<IEnumerable<Talk>, TalkFilterViewModel>()
                .ForMember(
                dest => dest.DisciplineList,
                opt => opt.MapFrom(src => src.Select(p => p.Discipline))
                )
                .ForMember(
                dest => dest.LocationList,
                opt => opt.MapFrom(src => src.Select(p => p.Location).Distinct().OrderBy(l => l))
                );
        }
    }
}