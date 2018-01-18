using AngularMVCCoreTechTalks.Automapper.Profiles;
using AngularMVCCoreTechTalks.Controllers;
using AngularMVCCoreTechTalks.ViewModels;
using AutoMapper;
using BusinessLogic.Filters;
using DataAccess.Entities;
using DataAccess.Services.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WebApiTests.Controllers
{
    public class TalkControllerTests
    {
        [Fact]
        public void GetFilters_WhenTwoRecordsExist_ShouldReturnTwoPossibleDisciplines()
        {
            // Arrange
            AutoMapper.Mapper.Reset();

            Mapper.Initialize(x =>
            {
                x.AddProfile<TalkToTalkFilterViewModelProfile>();
            });

            var mockTalkService = new Mock<ITalkService>();

            mockTalkService.Setup(x => x.GetAll()).Returns(GetAllTalks());

            var controller = new TalkController(mockTalkService.Object);

            // Act
            TalkFilterViewModel result = controller.GetFilters();

            // Assert
            result.DisciplineList.ToList().Count().CompareTo(2);
        }

        [Fact]
        public void GetFilteredTalks_WhenTwoRecordsExist_ShouldReturnTwoTalks()
        {
            // Arrange

            AutoMapper.Mapper.Reset();

            Mapper.Initialize(x =>
            {
                x.AddProfile<TalkFilterViewModelToTalkFilterProfile>();
                x.AddProfile<TalkToTalkViewModelProfile>();
            });

            TalkFilter filter = new TalkFilter
            {
                DisciplineName = string.Empty,
                Location = string.Empty,
                SpeakerName = string.Empty,
                Topic = string.Empty,
                DateFrom = null,
                DateTo = null
            };
           
            var mockTalkService = new Mock<ITalkService>();

            mockTalkService.Setup(x => x.ExecuteFilters(filter.FilterExpression)).Returns(GetAllTalks());

            var controller = new TalkController(mockTalkService.Object);

            // Act
            IEnumerable<TalkViewModel> result = controller.GetFilteredTalks(
                filter.DisciplineName,
                filter.Location,
                filter.SpeakerName,
                filter.Topic,
                filter.DateFrom,
                filter.DateTo);

            // Assert
            result.Count().CompareTo(2);
        }

        private IEnumerable<Talk> GetAllTalks()
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
