using AngularMVCCoreTechTalks.Controllers;
using DataAccess.Entities;
using DataAccess.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebApi.UnitTests.Controllers
{
    public class DisciplineControllerTests
    {
        [Fact]
        public void CreateDisciplineTest()
        {
            // Arrange
            Discipline discipline = new Discipline
            {
                DisciplineName = "FT"
            };

            var mockDisciplineService = new Mock<IDisciplineService>();

            mockDisciplineService.Setup(x => x.Create(discipline));

            var controller = new DisciplineController(mockDisciplineService.Object);

            // Act
            controller.CreateDiscipline(discipline);

            // Assert
            mockDisciplineService.Verify(mock => mock.Create(discipline), Times.Once());
        }
    }
}
