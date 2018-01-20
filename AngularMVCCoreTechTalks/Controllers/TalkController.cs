using AngularMVCCoreTechTalks.ViewModels;
using AutoMapper;
using BusinessLogic.Filters;
using DataAccess.Entities;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AngularMVCCoreTechTalks.Controllers
{
    /// <summary>
    /// The class represents Talk controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class TalkController : Controller
    {
        #region Private Fields

        private readonly ITalkService _talkService;

        #endregion

        #region Constructor

        public TalkController(ITalkService talkService)
        {
            _talkService = talkService ?? throw new ArgumentNullException();
        }

        #endregion

        #region Actions

        [HttpGet("[action]")]
        public TalkFilterViewModel GetFilters()
        {
            IList<Talk> talks = _talkService.GetAll().ToList();

            TalkFilterViewModel filterViewModel = Mapper.Map<TalkFilterViewModel>(talks);

            filterViewModel.DisciplineList.Insert(0, string.Empty);
            filterViewModel.LocationList.Insert(0, string.Empty);

            return filterViewModel;
        }

        [HttpPost("[action]")]
        public IEnumerable<TalkViewModel> GetFilteredTalks([FromBody]TalkFilterViewModel talkFilterViewModel)
        {
            TalkFilter filter = Mapper.Map<TalkFilter>(talkFilterViewModel);
            var talks = _talkService.ExecuteFilters(filter.FilterExpression);
            IEnumerable<TalkViewModel> talksViewModel = Mapper.Map<IEnumerable<TalkViewModel>>(talks);
            return talksViewModel;
        }

        #endregion
    }
}
