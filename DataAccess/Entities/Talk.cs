using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    /// <summary>
    /// The class represents the talk entity.
    /// </summary>
    public class Talk
    {
        /// <summary>
        /// Gets or sets the talk identifier.
        /// </summary>
        /// <value>
        /// The talk identifier.
        /// </value>
        public int TalkId { get; set; }

        /// <summary>
        /// Gets or sets the talk date.
        /// </summary>
        /// <value>
        /// The talk date.
        /// </value>
        public DateTime TalkDate { get; set; }

        /// <summary>
        /// Gets or sets the topic.
        /// </summary>
        /// <value>
        /// The topic.
        /// </value>
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets the additional detail.
        /// </summary>
        /// <value>
        /// The additional detail.
        /// </value>
        public string AdditionalDetail { get; set; }

        /// <summary>
        /// Gets or sets the speaker identifier.
        /// </summary>
        /// <value>
        /// The speaker identifier.
        /// </value>
        [Column("Speaker")]
        public int? SpeakerId { get; set; }

        /// <summary>
        /// Gets or sets the discipline identifier.
        /// </summary>
        /// <value>
        /// The discipline identifier.
        /// </value>
        [Column("Discipline")]
        public int? DisciplineId { get; set; }

        /// <summary>
        /// Gets or sets the presentation link.
        /// </summary>
        /// <value>
        /// The presentation link.
        /// </value>
        public string PresentationLink { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the row version.
        /// </summary>
        /// <value>
        /// The row version.
        /// </value>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Gets or sets the discipline.
        /// </summary>
        /// <value>
        /// The discipline.
        /// </value>
        public Discipline Discipline { get; set; }

        /// <summary>
        /// Gets or sets the speaker.
        /// </summary>
        /// <value>
        /// The speaker.
        /// </value>
        public Speaker Speaker { get; set; }
    }
}
