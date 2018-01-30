using System;
using System.ComponentModel.DataAnnotations;

namespace AngularMVCCoreTechTalks.ViewModels
{
    public class SelectedTalkFilterViewModel
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the name of the discipline.
        /// </summary>
        /// <value>
        /// The name of the discipline.
        /// </value>
        public string DisciplineName { get; set; }

        /// <summary>
        /// Gets or sets the name of the speaker.
        /// </summary>
        /// <value>
        /// The name of the speaker.
        /// </value>
        public string SpeakerName { get; set; }

        /// <summary>
        /// Gets or sets the topic.
        /// </summary>
        /// <value>
        /// The topic.
        /// </value>
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets the date from.
        /// </summary>
        /// <value>
        /// The date from.
        /// </value>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the date to.
        /// </summary>
        /// <value>
        /// The date to.
        /// </value>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateTo { get; set; }
    }
}
