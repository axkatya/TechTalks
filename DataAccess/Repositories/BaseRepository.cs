using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.EF;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    /// <summary>
    /// The class represents the base repository class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="DataAccess.Repositories.Interfaces.IRepositoryBase{T}" />
    public class BaseRepository<T> : IDisposable, IRepositoryBase<T> where T : class
	{
        #region Protected Fields

        protected TalksContext Db;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="talksContext">The talks context.</param>
        public BaseRepository(TalksContext talksContext)
        {
            Db = talksContext;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Create(T obj)
		{
            if (obj == null)
            {
                throw new ArgumentException("obj is null");
            }

            Db.Set<T>().Add(obj);
            Db.SaveChanges();
		}

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>Returns all rows.</returns>
        public virtual  IEnumerable<T> GetAll()
		{
			return Db.Set<T>().ToList();
		}

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetById(int id)
		{
			return Db.Set<T>().Find(id);
		}

        /// <summary>
        /// Deletes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <exception cref="ArgumentException">obj is null</exception>
        public void Delete(T obj)
		{
            if (obj == null)
            {
                throw new ArgumentException("obj is null");
            }

            Db.Set<T>().Remove(obj);
            Db.SaveChanges();
		}

        /// <summary>
        /// Updates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <exception cref="ArgumentException">obj is null</exception>
        public void Update(T obj)
		{
            if (obj == null)
            {
                throw new ArgumentException("obj is null");
            }

            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
		}

        #endregion
    }
}
