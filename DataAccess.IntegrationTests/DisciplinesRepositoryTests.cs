using DataAccess.EF;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace DataAccess.IntegrationTests
{
    public class DisciplinesRepositoryTests : IDisposable
    {
        private TalksContext _talksContext;

        public DisciplinesRepositoryTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<TalksContext>();

            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=TalksDB_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true")
                 .UseInternalServiceProvider(serviceProvider);

            _talksContext = new TalksContext(builder.Options);
            _talksContext.Database.EnsureCreated();
        }

        [Fact]
        public void CreateSpeaker_WhenSpeakerCreated_ShouldReturnSpeakerd()
        {
            // Arrange
            var discipline = new Discipline
            {
                DisciplineName = "FT"
            };

            //Act
            DisciplinesRepository disciplinesRepository = new DisciplinesRepository(_talksContext);
            Discipline actualResult = disciplinesRepository.Create(discipline);

            //Assert
            Assert.True(actualResult.DisciplineId > 0);
        }

        public void Dispose()
        {
            _talksContext.Database.EnsureDeleted();
        }
    }
}
