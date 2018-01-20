using AngularMVCCoreTechTalks.ViewModels;
using AutoMapper;
using DataAccess.Entities;

namespace AngularMVCCoreTechTalks.Automapper.Profiles
{
    /// <summary>
    /// The class represents automapper from talk view model to talk.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class TalkViewModelToTalkProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TalkViewModelToTalkProfile"/> class.
        /// </summary>
        public TalkViewModelToTalkProfile()
        {
            CreateMap<TalkViewModel, Talk>();
        }
    }
}