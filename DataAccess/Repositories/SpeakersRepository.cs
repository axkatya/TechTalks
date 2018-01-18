using DataAccess.EF;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    /// <summary>
    /// The class represents the speaker repository.
    /// </summary>
    /// <seealso cref="DataAccess.Repositories.BaseRepository{DataAccess.Entities.Speaker}" />
    /// <seealso cref="DataAccess.Repositories.Interfaces.ISpeakersRepository" />
    public class SpeakersRepository: BaseRepository<Speaker>, ISpeakersRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakersRepository"/> class.
        /// </summary>
        /// <param name="talksContext">The talks context.</param>
        public SpeakersRepository(TalksContext talksContext):base(talksContext)
        {
        }
    }
}
