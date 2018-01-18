using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using DataAccess.Services.Interfaces;

namespace DataAccess.Services
{
    /// <summary>
    /// The class represents speaker service.
    /// </summary>
    /// <seealso cref="DataAccess.Services.BaseService{DataAccess.Entities.Speaker}" />
    /// <seealso cref="DataAccess.Services.Interfaces.ISpeakerService" />
    public class SpeakerService : BaseService<Speaker>, ISpeakerService
    {
        #region Private Fields

        private ISpeakersRepository _repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public SpeakerService(ISpeakersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        #endregion
    }
}
