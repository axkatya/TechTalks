using System;

namespace AngularMVCCoreTechTalks.ViewModels
{
    /// <summary>
    /// The class represents talk view model.
    /// </summary>
    public class TalkViewModel
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
        public int SpeakerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the speaker.
        /// </summary>
        /// <value>
        /// The name of the speaker.
        /// </value>
        public string SpeakerName { get; set; }

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
        public string DisciplineName { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }
    }
}