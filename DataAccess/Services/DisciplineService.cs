using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using DataAccess.Services.Interfaces;

namespace DataAccess.Services
{
    /// <summary>
    /// The class represents discipline service.
    /// </summary>
    /// <seealso cref="DataAccess.Services.BaseService{DataAccess.Entities.Discipline}" />
    /// <seealso cref="DataAccess.Services.Interfaces.IDisciplineService" />
    public class DisciplineService : BaseService<Discipline>, IDisciplineService
    {
        #region Private Fields

        private IDisciplinesRepository _repository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DisciplineService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public DisciplineService(IDisciplinesRepository repository) : base(repository)
        {
            _repository = repository;
        }

        #endregion
    }
}
