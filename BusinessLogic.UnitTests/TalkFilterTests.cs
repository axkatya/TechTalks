using BusinessLogic.Filters;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BusinessLogic.UnitTests
{
    public class TalkFilterTests
    {
        [Fact]
        public void FilterExpression_WhenOneRowByFilter_ReturnTrueForOneRow()
        {
            // Arrange
            TalkFilter filter = new TalkFilter
            {
                DisciplineName = "FT",
                Location = string.Empty,
                SpeakerName = string.Empty,
                Topic = string.Empty,
                DateFrom = null,
                DateTo = null
            };

            var talks = GenerateTalks();

            //Act       
            var result1 = filter.FilterExpression(talks.Where(t => t.TalkId == 1).FirstOrDefault());
            var result2 = filter.FilterExpression(talks.Where(t => t.TalkId == 2).FirstOrDefault());

            //Assert
            Assert.True(result2);
            Assert.False(result1);
        }

        private IEnumerable<Talk> GenerateTalks()
        {
            IList<Talk> talks = new List<Talk>();

            talks.Add(new Talk()
            {
                TalkId = 1,
                Discipline = new Discipline { DisciplineId = 1, DisciplineName = "Java" },
                Location = "Location One",

            });

            talks.Add(new Talk()
            {
                TalkId = 2,
                Discipline = new Discipline { DisciplineId = 2, DisciplineName = "FT" },
                Location = "Location Two"
            });

            return talks;
        }
    }
}
