using System.Collections.Generic;

namespace Entities.Entities
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }

        public ICollection<Talk> Talks { get; set; }
    }
}
