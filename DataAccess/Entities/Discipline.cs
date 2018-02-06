using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    /// <summary>
    /// The class represents discipline entity.
    /// </summary>
    public class Discipline
    {
        /// <summary>
        /// Gets or sets the discipline identifier.
        /// </summary>
        /// <value>
        /// The discipline identifier.
        /// </value>
        public int DisciplineId { get; set; }

        /// <summary>
        /// Gets or sets the name of the discipline.
        /// </summary>
        /// <value>
        /// The name of the discipline.
        /// </value>
        [MaxLength(50)]
        public string DisciplineName { get; set; }
    }
}
