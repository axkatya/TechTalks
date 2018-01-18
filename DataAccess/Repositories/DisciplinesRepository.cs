using DataAccess.EF;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    /// <summary>
    /// The class represents the discipline repository.
    /// </summary>
    /// <seealso cref="DataAccess.Repositories.BaseRepository{DataAccess.Entities.Discipline}" />
    /// <seealso cref="DataAccess.Repositories.Interfaces.IDisciplinesRepository" />
    public class DisciplinesRepository : BaseRepository<Discipline>, IDisciplinesRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisciplinesRepository"/> class.
        /// </summary>
        /// <param name="talksContext">The talks context.</param>
        public DisciplinesRepository(TalksContext talksContext):base(talksContext)
        {
        }
    }
}
