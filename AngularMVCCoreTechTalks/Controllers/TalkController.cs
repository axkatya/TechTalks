using AngularMVCCoreTechTalks.ViewModels;
using AutoMapper;
using BusinessLogic.Filters;
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
            var talks = _talkService.GetAll().ToList();
            TalkFilterViewModel filterViewModel = Mapper.Map<TalkFilterViewModel>(talks);
            return filterViewModel;
        }

        [HttpGet("[action]")]
        public IEnumerable<TalkViewModel> GetFilteredTalks(
            [FromQuery]string disciplineName,
            string locationName,
            string speakerName,
            string topic,
            DateTime? dateFrom,
            DateTime? dateTo)
        {
            TalkFilterViewModel talkFilterViewModel = new TalkFilterViewModel();
            talkFilterViewModel.DisciplineName = disciplineName;
            talkFilterViewModel.Location = locationName;
            talkFilterViewModel.SpeakerName = speakerName;
            talkFilterViewModel.Topic = topic;
            talkFilterViewModel.DateFrom = dateFrom;
            talkFilterViewModel.DateTo = dateTo;

            TalkFilter filter = Mapper.Map<TalkFilter>(talkFilterViewModel);
            var talks = _talkService.ExecuteFilters(filter.FilterExpression);
            IEnumerable<TalkViewModel> talksViewModel = Mapper.Map<IEnumerable<TalkViewModel>>(talks);
            return talksViewModel;
        }

        #endregion
    }
}
