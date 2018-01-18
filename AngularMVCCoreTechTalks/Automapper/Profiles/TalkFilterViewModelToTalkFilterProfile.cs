﻿using AngularMVCCoreTechTalks.ViewModels;
using AutoMapper;
using BusinessLogic.Filters;

namespace AngularMVCCoreTechTalks.Automapper.Profiles
{
    /// <summary>
    /// The class represents automapper from talk filter view model to talk filter.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class TalkFilterViewModelToTalkFilterProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TalkFilterViewModelToTalkFilterProfile"/> class.
        /// </summary>
        public TalkFilterViewModelToTalkFilterProfile()
        {
            CreateMap<TalkFilterViewModel, TalkFilter>();
        }
    }
}