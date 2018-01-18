using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Speaker
    {
        public Speaker()
        {
            Talks = new HashSet<Talk>();
        }

        public int SpeakerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }

        public ICollection<Talk> Talks { get; set; }
    }
}
