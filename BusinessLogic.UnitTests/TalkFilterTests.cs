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
        public void FilterExpression_WhenOneRowByDisciplineFilter_ReturnTrueForOneRow()
        {
            // Arrange
            TalkFilter filter = new TalkFilter
            {
                DisciplineName = "UnitTest_FT",
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

        [Fact]
        public void FilterExpression_WhenOneRowBySpeakerFirstNameFilter_ReturnTrueForOneRow()
        {
            // Arrange
            TalkFilter filter = new TalkFilter
            {
                DisciplineName = string.Empty,
                Location = string.Empty,
                SpeakerName = "   UnitTest_oleg ",
                Topic = string.Empty,
                DateFrom = null,
                DateTo = null
            };

            var talks = GenerateTalks();

            //Act       
            var result1 = filter.FilterExpression(talks.Where(t => t.TalkId == 1).FirstOrDefault());
            var result2 = filter.FilterExpression(talks.Where(t => t.TalkId == 2).FirstOrDefault());

            //Assert
            Assert.True(result1);
            Assert.False(result2);
        }

        [Fact]
        public void FilterExpression_WhenOneRowBySpeakerLastNameFilter_ReturnTrueForOneRow()
        {
            // Arrange
            TalkFilter filter = new TalkFilter
            {
                DisciplineName = string.Empty,
                Location = string.Empty,
                SpeakerName = "   UnitTest_ivanov ",
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

        [Fact]
        public void FilterExpression_WhenOneRowBySpeakerFullNameFilter_ReturnTrueForOneRow()
        {
            // Arrange
            TalkFilter filter = new TalkFilter
            {
                DisciplineName = string.Empty,
                Location = string.Empty,
                SpeakerName = "   UnitTest_oleg UnitTest_pupkin ",
                Topic = string.Empty,
                DateFrom = null,
                DateTo = null
            };

            var talks = GenerateTalks();

            //Act       
            var result1 = filter.FilterExpression(talks.Where(t => t.TalkId == 1).FirstOrDefault());
            var result2 = filter.FilterExpression(talks.Where(t => t.TalkId == 2).FirstOrDefault());

            //Assert
            Assert.True(result1);
            Assert.False(result2);
        }

        [Fact]
        public void FilterExpression_WhenOneRowBySpeakerFullNameFilterWithSpaces_ReturnTrueForOneRow()
        {
            // Arrange
            TalkFilter filter = new TalkFilter
            {
                DisciplineName = string.Empty,
                Location = string.Empty,
                SpeakerName = "   UnitTest_oleg   UnitTest_pupkin ",
                Topic = string.Empty,
                DateFrom = null,
                DateTo = null
            };

            var talks = GenerateTalks();

            //Act       
            var result1 = filter.FilterExpression(talks.Where(t => t.TalkId == 1).FirstOrDefault());
            var result2 = filter.FilterExpression(talks.Where(t => t.TalkId == 2).FirstOrDefault());

            //Assert
            Assert.True(result1);
            Assert.False(result2);
        }

        [Fact]
        public void FilterExpression_WhenOneRowByTopicFilter_ReturnTrueForOneRow()
        {
            // Arrange
            TalkFilter filter = new TalkFilter
            {
                DisciplineName = string.Empty,
                Location = string.Empty,
                SpeakerName = string.Empty,
                Topic = " UnitTest_async ",
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
                Discipline = new Discipline { DisciplineId = 1, DisciplineName = "UnitTest_Java" },
                Speaker = new Speaker { SpeakerId = 1, FirstName = "UnitTest_Oleg",
                    LastName = "UnitTest_Pupkin" },
                Location = "UnitTest_Location One",
                Topic = "UnitTest_Tread topic"

            });

            talks.Add(new Talk()
            {
                TalkId = 2,
                Discipline = new Discipline { DisciplineId = 2, DisciplineName = "UnitTest_FT" },
                Speaker = new Speaker { SpeakerId = 2, FirstName = "UnitTest_Petr", LastName = "UnitTest_Ivanov" },
                Location = "UnitTest_Location Two",
                Topic = "UnitTest_Async topic"
            });

            return talks;
        }
    }
}
