using DataAccess.Entities;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AngularMVCCoreTechTalks.Controllers
{
    [Route("api/[controller]")]
    public class DisciplineController : Controller
    {
        #region Private Fields

        /// <summary>
        /// The discipline service
        /// </summary>
        private readonly IDisciplineService _disciplineService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DisciplineController"/> class.
        /// </summary>
        /// <param name="disciplineService">The discipline service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DisciplineController(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService ?? throw new ArgumentNullException();
        }

        #endregion

        [HttpPost("[action]")]
        public Discipline CreateDiscipline([FromBody]Discipline discipline)
        {
            discipline.DisciplineName = System.Net.WebUtility.HtmlEncode(discipline.DisciplineName);
            return _disciplineService.Create(discipline);
        }
    }
}
