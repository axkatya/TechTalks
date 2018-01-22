using DataAccess.Entities;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AngularMVCCoreTechTalks.Controllers
{
    /// <summary>
    /// The class represents Speaker controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class SpeakerController : Controller
    {
        #region Private Fields

        private readonly ISpeakerService _speakerService;

        #endregion

        #region Constructor

        public SpeakerController(ISpeakerService speakerService)
        {
            _speakerService = speakerService ?? throw new ArgumentNullException();
        }

        #endregion

        #region Actions

        [HttpGet("[action]/{id}")]
        public Speaker GetSpeakerById(int id)
        {
            var speaker = _speakerService.GetById(id);
            return speaker;
        }

        #endregion
    }
}
