using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Talk
    {
        public int TalkId { get; set; }
        public DateTime TalkDate { get; set; }
        public string Topic { get; set; }
        public string AdditionalDetail { get; set; }
        [Column("Speaker")]
        public int? SpeakerId { get; set; }

        [Column("Discipline")]
        public int? DisciplineId { get; set; }
        public string PresentationLink { get; set; }
        public string Location { get; set; }

        public Discipline Discipline { get; set; }
        public Speaker Speaker { get; set; }
    }
}
