using DataAccess.EF;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace DataAccess.IntegrationTests
{
    public class SpeakersRepositoryTests : IDisposable
    {
        private TalksContext _talksContext;

        public SpeakersRepositoryTests()
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
        public void GetById_WhenOneRecordExists_ShouldReturnOneRecordWithRelatedRows()
        {
            // Arrange
            var discipline = new Discipline { DisciplineName = ".NET" };
            var speaker = new Speaker { FirstName = "Ivan", LastName = "Pupkin", Position = "SE", Location = "Minsk" };
            _talksContext.Talks.Add(new Talk
            {
                Location = "L3, Room 54",
                Topic = "SignalR",
                Discipline = discipline,
                Speaker = speaker
            });

            var changes = _talksContext.SaveChanges();
            var speakerId = _talksContext.Speakers.First().SpeakerId;

            //Act
            SpeakersRepository speakersRepository = new SpeakersRepository(_talksContext);
            var actualSpeaker = speakersRepository.GetById(speakerId);

            //Assert
            Assert.Equal("Ivan", actualSpeaker.FirstName);
            Assert.Equal("Pupkin", actualSpeaker.LastName);
        }

        public void Dispose()
        {
            _talksContext.Database.EnsureDeleted();
        }
    }
}
