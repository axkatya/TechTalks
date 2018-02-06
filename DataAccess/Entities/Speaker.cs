using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    /// <summary>
    /// The class represents the speaker entity.
    /// </summary>
    public class Speaker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Speaker"/> class.
        /// </summary>
        public Speaker()
        {
            Talks = new HashSet<Talk>();
        }

        /// <summary>
        /// Gets or sets the speaker identifier.
        /// </summary>
        /// <value>
        /// The speaker identifier.
        /// </value>
        public int SpeakerId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [MaxLength(20)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [MaxLength(20)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [MaxLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        [MaxLength(50)]
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        [MaxLength(50)]
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [MaxLength(100)]
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
        /// Gets or sets the talks.
        /// </summary>
        /// <value>
        /// The talks.
        /// </value>
        public ICollection<Talk> Talks { get; set; }
    }
}
