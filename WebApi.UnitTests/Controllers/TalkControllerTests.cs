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

namespace WebApi.UnitTests.Controllers
{
    public class TalkControllerTests
    {
        [Fact]
        public void GetFilters_WhenTwoRecordsExist_ShouldReturnDistinctValues()
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
            result.LocationList.ToList().Count().CompareTo(1);
        }

        [Fact]
        public void GetFilteredTalks_WhenTwoRecordsExist_ShouldReturnTwoTalks()
        {
            // Arrange

            AutoMapper.Mapper.Reset();

            Mapper.Initialize(x =>
            {
                x.AddProfile<SelectedTalkFilterViewModelToTalkFilterProfile>();
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

            SelectedTalkFilterViewModel talkFilterViewModel = new SelectedTalkFilterViewModel
            {
                DisciplineName = filter.DisciplineName,
                Location = filter.DisciplineName,
                SpeakerName = filter.SpeakerName,
                Topic = filter.Topic,
                DateFrom = filter.DateFrom,
                DateTo = filter.DateTo
            };

            // Act
            IEnumerable<TalkViewModel> result = controller.GetFilteredTalks(talkFilterViewModel);

            // Assert
            result.Count().CompareTo(2);
        }

        [Fact]
        public void UpdateTalkTest()
        {
            // Arrange
            Talk talk = new Talk
            {
                TalkId = 1,
                Topic = "test",
                Location = "Room 67",
                AdditionalDetail = "",
                SpeakerId = 1,
                DisciplineId = 1
            };

            var mockTalkService = new Mock<ITalkService>();

            mockTalkService.Setup(x => x.Update(talk));

            var controller = new TalkController(mockTalkService.Object);

            // Act
            controller.UpdateTalk(talk.TalkId, talk);

            // Assert
            mockTalkService.Verify(mock => mock.Update(talk), Times.Once());
        }

        [Fact]
        public void CreateTalkTest()
        {
            // Arrange
            Talk talk = new Talk
            {
                Topic = "test",
                Location = "Room 67",
                AdditionalDetail = "",
                SpeakerId = 1,
                DisciplineId = 1
            };

            var mockTalkService = new Mock<ITalkService>();

            mockTalkService.Setup(x => x.Create(talk));

            var controller = new TalkController(mockTalkService.Object);

            // Act
            controller.CreateTalk(talk);

            // Assert
            mockTalkService.Verify(mock => mock.Create(talk), Times.Once());
        }

        [Fact]
        public void DeleteTalkTest()
        {
            // Arrange
            var discipline = new Discipline
            {
                DisciplineId = 1,
                DisciplineName = ".NET"
            };

            var speaker = new Speaker
            {
                SpeakerId = 1,
                FirstName = "Ivan",
                LastName = "Pupkin",
                Position = "SE",
                Location = "Minsk"
            };

            Talk talk = new Talk
            {
                Topic = "test",
                Location = "Room 67",
                AdditionalDetail = "",
                Speaker = speaker,
                SpeakerId = speaker.SpeakerId,
                Discipline = discipline,
                DisciplineId = discipline.DisciplineId
            };

            var mockTalkService = new Mock<ITalkService>();

            mockTalkService.Setup(x => x.DeleteById(talk.TalkId));

            var controller = new TalkController(mockTalkService.Object);

            // Act
            controller.DeleteTalk(talk.TalkId);

            // Assert
            mockTalkService.Verify(mock => mock.DeleteById(talk.TalkId), Times.Once());
        }

        private IEnumerable<Talk> GetAllTalks()
        {
            IList<Talk> talks = new List<Talk>
            {
                new Talk()
                {
                    TalkId = 1,
                    Discipline = new Discipline { DisciplineId = 1, DisciplineName = "Java" },
                    Location = "Location One",

                },

                new Talk()
                {
                    TalkId = 2,
                    Discipline = new Discipline { DisciplineId = 2, DisciplineName = "FT" },
                    Location = "Location One"
                }
            };

            return talks;
        }
    }
}
