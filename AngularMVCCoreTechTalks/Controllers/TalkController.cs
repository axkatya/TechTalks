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

            filterViewModel.DisciplineList.Insert(0, string.Empty );
            filterViewModel.LocationList.Insert(0, string.Empty);

            return filterViewModel;
        }

        [HttpGet("[action]")]
        public TalkFilterViewModel GetPossibleLists()
        {
            IList<Talk> talks = _talkService.GetAll().ToList();

            TalkFilterViewModel filterViewModel = Mapper.Map<TalkFilterViewModel>(talks);

            return filterViewModel;
        }

        [HttpGet("[action]/{id}")]
        public TalkViewModel GetTalkById(int id)
        {
            Talk talk = _talkService.GetById(id);
            TalkViewModel talksViewModel = Mapper.Map<TalkViewModel>(talk);
            return talksViewModel;
        }

        [HttpPost("[action]")]
        public IEnumerable<TalkViewModel> GetFilteredTalks([FromBody]SelectedTalkFilterViewModel selectedTalkFilter)
        {
            TalkFilter talkFilter = Mapper.Map<TalkFilter>(selectedTalkFilter);
            var talks = _talkService.ExecuteFilters(talkFilter.FilterExpression);
            IEnumerable<TalkViewModel> talksViewModel = Mapper.Map<IEnumerable<TalkViewModel>>(talks);
            return talksViewModel;
        }

        [HttpPost("[action]")]
        public Talk CreateTalk([FromBody]Talk talk)
        {
            EncodeTalkProperties(talk);
            return _talkService.Create(talk);
        }

        [HttpPut("[action]/{id}")]
        public void UpdateTalk(int id, [FromBody]Talk talk)
        {
            EncodeTalkProperties(talk);
            _talkService.Update(talk);
        }

        [HttpDelete("[action]/{id}")]
        public void DeleteTalk(int id)
        {
            _talkService.DeleteById(id);
        }

        #endregion

        private static void EncodeTalkProperties(Talk talk)
        {
            talk.AdditionalDetail = System.Net.WebUtility.HtmlEncode(talk.AdditionalDetail);
            talk.Topic = System.Net.WebUtility.HtmlEncode(talk.Topic);
            talk.Location = System.Net.WebUtility.HtmlEncode(talk.Location);
        }
    }
}
