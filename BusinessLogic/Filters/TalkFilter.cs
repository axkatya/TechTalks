using DataAccess.Entities;
using System;

namespace BusinessLogic.Filters
{
    /// <summary>
    /// The class represents the filter for Talk data.
    /// </summary>
    /// <seealso cref="BusinessLogic.Filters.ITalkFilter" />
    public class TalkFilter : ITalkFilter
    {
        #region Private Fields

        DateTime? _dateFrom;
        DateTime? _dateTo;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the location name.
        /// </summary>
        /// <value>
        /// The location name.
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
        /// Gets or sets the filter date from.
        /// </summary>
        /// <value>
        /// The filter date from.
        /// </value>
        public DateTime? DateFrom
        {
            get => _dateFrom.Equals(DateTime.MinValue) ? null : _dateFrom;
            set => _dateFrom = value;
        }
        /// <summary>
        /// Gets or sets the filter date to.
        /// </summary>
        /// <value>
        /// The filter date to.
        /// </value>
        public DateTime? DateTo
        {
            get => _dateTo.Equals(DateTime.MinValue) ? null : _dateTo;
            set => _dateTo = value;
        }

        #endregion

        #region Public Methods

        public Func<Talk, bool> FilterExpression => expr => (Location == null || string.Equals(expr.Location, Location)) &&
                                                                                 (DisciplineName == null || string.Equals(expr.Discipline.DisciplineName, DisciplineName)) &&
                                                                                 (Topic == null || string.Equals(expr.Topic, Topic)) &&
                                                                                 FilterSpeakerName(expr) &&
                                                                                 (Topic == null || string.Equals(expr.Topic, Topic)) &&
                                                                                 (DateFrom == null || expr.TalkDate >= DateFrom) &&
                                                                                 (DateTo == null || expr.TalkDate <= DateTo);

        #endregion

        #region Private Methods

        private bool FilterSpeakerName(Talk expr)
        {
            if (SpeakerName == null || string.IsNullOrWhiteSpace(SpeakerName))
            {
                return true;
            }

            string[] speakerNameParts = SpeakerName.Trim().Split(SpeakerName, ' ');

            return ((speakerNameParts.Length == 1 && (string.Equals(speakerNameParts[0], expr.Speaker.FirstName) || string.Equals(speakerNameParts[0], expr.Speaker.LastName))) ||
            (string.Equals(speakerNameParts[0], expr.Speaker.FirstName) && string.Equals(speakerNameParts[1], expr.Speaker.LastName)) ||
                (string.Equals(speakerNameParts[0], expr.Speaker.LastName) && string.Equals(speakerNameParts[1], expr.Speaker.FirstName)));
        }

        #endregion
    }
}
