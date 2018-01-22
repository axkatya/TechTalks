using DataAccess.EF;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    /// <summary>
    /// The class represents the talk repository.
    /// </summary>
    /// <seealso cref="DataAccess.Repositories.BaseRepository{DataAccess.Entities.Talk}" />
    /// <seealso cref="DataAccess.Repositories.Interfaces.ITalksRepository" />
    public class TalksRepository : BaseRepository<Talk>, ITalksRepository
    {
        #region Private Fields

        private TalksContext _talksContext;

        #endregion

        #region Contructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TalksRepository"/> class.
        /// </summary>
        /// <param name="talksContext">The talks context.</param>
        public TalksRepository(TalksContext talksContext) : base(talksContext)
        {
            _talksContext = talksContext;
        }

        #endregion

        #region Interface methods

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// Returns all rows.
        /// </returns>
        public override IEnumerable<Talk> GetAll()
        {
            return Db.Talks.Include(discipline => discipline.Discipline).Include(speaker => speaker.Speaker).ToList();
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
            return Db.Talks.Include(discipline => discipline.Discipline).Include(speaker => speaker.Speaker).Where(filterExpressiion).ToList();
        }

        /// <summary>
        /// Deletes the talk by talk identifier.
        /// </summary>
        /// <param name="talkId">The talk identifier.</param>
        public void DeleteTalkByTalkId(int talkId)
        {
            Talk talk = new Talk() { TalkId = talkId };
            Db.Talks.Attach(talk);
            this.Delete(talk);
        }

        #endregion
    }
}
