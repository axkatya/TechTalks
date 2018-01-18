using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using DataAccess.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccess.Services
{
    /// <summary>
    /// The class represents talk service.
    /// </summary>
    /// <seealso cref="DataAccess.Services.BaseService{DataAccess.Entities.Talk}" />
    /// <seealso cref="DataAccess.Services.Interfaces.ITalkService" />
    public class TalkService : BaseService<Talk>, ITalkService
	{
        #region Private Fields

        private ITalksRepository _repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TalkService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public TalkService (ITalksRepository repository) :base (repository)
		{
			_repository = repository;
		}

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the filters.
        /// </summary>
        /// <param name="filterExpressiion">The filter expressiion.</param>
        /// <returns></returns>
        public IEnumerable<Talk> ExecuteFilters(Func<Talk, bool> filterExpressiion)
		{
			return _repository.ExecuteFilters(filterExpressiion);
		}

        #endregion
    }
}
