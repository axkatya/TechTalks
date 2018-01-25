using AngularMVCCoreTechTalks.Controllers;
using DataAccess.Entities;
using DataAccess.Services.Interfaces;
using Moq;
using Xunit;

namespace WebApi.UnitTests.Controllers
{
    public class SpeakerControllerTests
    {
        [Fact]
        public void GetSpeakerTest()
        {
            // Arrange
            int id = 1;
            var mockSpeakerService = new Mock<ISpeakerService>();

            mockSpeakerService.Setup(x => x.GetById(id)).Returns(GetSpeaker());

            var controller = new SpeakerController(mockSpeakerService.Object);

            // Act
            Speaker result = controller.GetSpeakerById(id);

            // Assert
            Assert.NotNull(result);
            result.SpeakerId.CompareTo(id);
        }

        [Fact]
        public void UpdateSpeakerTest()
        {
            // Arrange
            Speaker speaker = new Speaker
            {
                SpeakerId = 1,
                FirstName = "test",
                LastName = "Amanda",
                Location = "Room 45",
                Department = "Java",
                Position = "D2"
            };

            var mockSpeakerService = new Mock<ISpeakerService>();

            mockSpeakerService.Setup(x => x.Update(speaker));

            var controller = new SpeakerController(mockSpeakerService.Object);

            // Act
            controller.UpdateSpeaker(speaker.SpeakerId, speaker);

            // Assert
            mockSpeakerService.Verify(mock => mock.Update(speaker), Times.Once());
        }

        [Fact]
        public void CreateSpeakerTest()
        {
            // Arrange
            Speaker speaker = new Speaker
            {
                FirstName = "test",
                LastName = "Amanda",
                Location = "Room 45",
                Department = "Java",
                Position = "D2"
            };

            var mockSpeakerService = new Mock<ISpeakerService>();

            mockSpeakerService.Setup(x => x.Create(speaker));

            var controller = new SpeakerController(mockSpeakerService.Object);

            // Act
            controller.CreateSpeaker(speaker);

            // Assert
            mockSpeakerService.Verify(mock => mock.Create(speaker), Times.Once());
        }

        private Speaker GetSpeaker()
        {
            Speaker speaker = new Speaker
            {
                SpeakerId = 1,
                FirstName = "Petr",
                LastName = "Pupkin"
            };

            return speaker;
        }
    }
}
