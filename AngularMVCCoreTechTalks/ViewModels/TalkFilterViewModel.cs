using DataAccess.Entities;
using System.Collections.Generic;

namespace AngularMVCCoreTechTalks.ViewModels
{
    /// <summary>
    /// The class represents talk filter veiw model.
    /// </summary>
    public class TalkFilterViewModel
	{
        /// <summary>
        /// Gets or sets the discipline list.
        /// </summary>
        /// <value>
        /// The discipline list.
        /// </value>
        public List<Discipline> DisciplineList { get; set; }

        /// <summary>
        /// Gets or sets the location list.
        /// </summary>
        /// <value>
        /// The location list.
        /// </value>
        public IList<string> LocationList { get; set; }
	}
}