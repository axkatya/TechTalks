using System;
using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Repositories.Interfaces
{
	public interface ITalksRepository : IRepositoryBase<Talk>
    {
		IEnumerable<Talk> ExecuteFilters(Func<Talk, bool> filterExpression);

        void DeleteTalkByTalkId(int talkId);
    }
}
