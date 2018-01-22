using System;
using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Services.Interfaces
{
	public interface ITalkService : IServiceBase<Talk>
    {
		 IEnumerable<Talk> ExecuteFilters(Func<Talk, bool> filterExpression);

        /// <summary>
        /// Deletes the talk by talk identifier.
        /// </summary>
        /// <param name="talkId">The talk identifier.</param>
        void DeleteTalkByTalkId(int talkId);
    }
}
