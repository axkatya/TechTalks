using System.Collections.Generic;
using DataAccess.Repositories.Interfaces;
using DataAccess.Services.Interfaces;

namespace DataAccess.Services
{
    /// <summary>
    /// The class represents the base service.
    /// </summary>
    /// <typeparam name="T">The type represents some type of entity.</typeparam>
    /// <seealso cref="DataAccess.Services.Interfaces.IServiceBase{T}" />
    public class BaseService<T> : IServiceBase<T> where T : class
    {
        #region Private Fields

        private IRepositoryBase<T> _repositoryBase;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BaseService(IRepositoryBase<T> repository)
        {
            _repositoryBase = repository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Create(T item)
        {
            _repositoryBase.Create(item);
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Delete(T item)
        {
            _repositoryBase.Delete(item);
        }

        /// <summary>
        /// Deletes the specified item by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteById(int id)
        {
            _repositoryBase.DeleteById(id);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return _repositoryBase.GetById(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return _repositoryBase.GetAll();
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Update(T item)
        {
            _repositoryBase.Update(item);
        }

        #endregion
    }
}
