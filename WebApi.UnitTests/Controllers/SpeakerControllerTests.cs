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
