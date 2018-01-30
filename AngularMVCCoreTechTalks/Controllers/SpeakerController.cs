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

        [HttpPost("[action]")]
        public Speaker CreateSpeaker([FromBody]Speaker speaker)
        {
            EncodeSpeakerProperties(speaker);
            return _speakerService.Create(speaker);
        }

        [HttpPut("[action]/{id}")]
        public void UpdateSpeaker(int id, [FromBody]Speaker speaker)
        {
            EncodeSpeakerProperties(speaker);
            _speakerService.Update(speaker);
        }

        #endregion

        private static void EncodeSpeakerProperties(Speaker speaker)
        {
            speaker.FirstName = System.Net.WebUtility.HtmlEncode(speaker.FirstName);
            speaker.LastName = System.Net.WebUtility.HtmlEncode(speaker.LastName);
            speaker.Position = System.Net.WebUtility.HtmlEncode(speaker.Position);
            speaker.Location = System.Net.WebUtility.HtmlEncode(speaker.Location);
        }
    }
}
