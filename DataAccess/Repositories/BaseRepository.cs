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

        protected TalksContext Context;
        private DbSet<T> entity;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="talksContext">The talks context.</param>
        public BaseRepository(TalksContext talksContext)
        {
            Context = talksContext;
            entity = talksContext.Set<T>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public T Create(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("obj is null");
            }

            try
            {
                Context.Entry(obj).State = EntityState.Added;
                Context.SaveChanges();
            }
            catch (DbUpdateException)
            {

            }

            return obj;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>Returns all rows.</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return entity.ToList();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T GetById(int id)
        {
            return entity.Find(id);
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
            try
            {
                entity.Remove(obj);
                Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                CatchDbUpdateException(ex);
            }
        }

        /// <summary>
        /// Deletes the specified object by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteById(int id)
        {
            try
            {
                T obj = entity.Find(id);
                this.Delete(obj);
            }
            catch (DbUpdateException ex)
            {
                CatchDbUpdateException(ex);
            }
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

            try
            {
                Context.Entry(obj).State = EntityState.Modified;
                Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                CatchDbUpdateException(ex);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        private void CatchDbUpdateException(DbUpdateException ex)
        {
            foreach (var entry in ex.Entries)
            {
                if (entry.Entity is T)
                {
                    var databaseEntity = entity.AsNoTracking().FirstOrDefault(p => p == (T)entry.Entity);
                    if (databaseEntity != null)
                    {
                        var databaseEntry = Context.Entry(databaseEntity);

                        foreach (var property in entry.Metadata.GetProperties())
                        {
                            var proposedValue = entry.Property(property.Name).CurrentValue;
                            var originalValue = entry.Property(property.Name).OriginalValue;
                            var databaseValue = databaseEntry.Property(property.Name).CurrentValue;

                            entry.Property(property.Name).OriginalValue = databaseEntry.Property(property.Name).CurrentValue;
                        }
                    }
                }
                else
                {
                    throw new NotSupportedException("Don't know how to handle concurrency conflicts for " + entry.Metadata.Name);
                }
            }
        }
    }
}
