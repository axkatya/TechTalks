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
        public void GetById_WhenOneRecordExists_ShouldReturnOneRecord()
        {
            // Arrange
            var discipline = new Discipline { DisciplineName = "IntTest_.NET" };

            var speaker = new Speaker
            {
                FirstName = "IntTest_Ivan",
                LastName = "IntTest_Pupkin",
                Position = "IntTest_SE",
                Location = "IntTest_Minsk"
            };

            _talksContext.Talks.Add(new Talk
            {
                Location = "IntTest_L3, Room 54",
                Topic = "IntTest_SignalR",
                Discipline = discipline,
                Speaker = speaker
            });

            var changes = _talksContext.SaveChanges();
            var speakerId = _talksContext.Speakers.First().SpeakerId;

            //Act
            SpeakersRepository speakersRepository = new SpeakersRepository(_talksContext);
            var actualSpeaker = speakersRepository.GetById(speakerId);

            //Assert
            Assert.Equal("IntTest_Ivan", actualSpeaker.FirstName);
            Assert.Equal("IntTest_Pupkin", actualSpeaker.LastName);
        }

        [Fact]
        public void GetAll_WhenOneRecordExists_ShouldReturnOneRecord()
        {
            // Arrange
            var discipline = new Discipline { DisciplineName = "IntTest_.NET" };

            var speaker = new Speaker
            {
                FirstName = "IntTest_Ivan",
                LastName = "IntTest_Pupkin",
                Position = "IntTest_SE",
                Location = "IntTest_Minsk"
            };

            _talksContext.Talks.Add(new Talk
            {
                Location = "IntTest_L3, Room 54",
                Topic = "IntTest_SignalR",
                Discipline = discipline,
                Speaker = speaker
            });

            var changes = _talksContext.SaveChanges();

            //Act
            SpeakersRepository speakersRepository = new SpeakersRepository(_talksContext);
            var actualSpeakers = speakersRepository.GetAll();

            //Assert
            Assert.NotNull(actualSpeakers);
            Assert.True(actualSpeakers.Count() > 0);
        }

        [Fact]
        public void CreateSpeaker_WhenSpeakerCreated_ShouldReturnSpeakerd()
        {
            // Arrange
            var speaker = new Speaker
            {
                FirstName = "IntTest_Ivan",
                LastName = "IntTest_Pupkin",
                Position = "IntTest_SE",
                Location = "IntTest_Minsk"
            };

            //Act
            SpeakersRepository speakersRepository = new SpeakersRepository(_talksContext);
            Speaker actualResult = speakersRepository.Create(speaker);

            //Assert
            Assert.True(actualResult.SpeakerId > 0);
        }

        [Fact]
        public void UpdateSpeaker_WhenSpeakerUpdated_ShouldReturnUpdatedSpeaker()
        {
            // Arrange
            var speaker = new Speaker
            {
                SpeakerId = 1,
                FirstName = "IntTest_Ivan",
                LastName = "IntTest_Pupkin",
                Position = "IntTest_SE",
                Location = "IntTest_Minsk",
                Department = "IntTest_Java",
                Email = "IntTest_Ivan_Pupkin@mail.com"
            };

            //Act
            SpeakersRepository speakersRepository = new SpeakersRepository(_talksContext);
            speakersRepository.Update(speaker);

            //Assert
            var actualResult = speakersRepository.GetById(speaker.SpeakerId);
            Assert.Equal(speaker.FirstName, actualResult.FirstName);
        }

        public void Dispose()
        {
            _talksContext.Database.EnsureDeleted();
        }
    }
}
