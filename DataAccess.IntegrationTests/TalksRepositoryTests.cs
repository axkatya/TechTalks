using BusinessLogic.Filters;
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
    public class TalksRepositoryTests : IDisposable
    {
        private TalksContext _talksContext;

        public TalksRepositoryTests()
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
        public void GetAll_WhenOneRecordExists_ShouldReturnOneRecordWithRelatedRows()
        {
            // Arrange
            var discipline = new Discipline
            {
                DisciplineName = "IntTest_.NET"
            };

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

            _talksContext.SaveChanges();

            //Act
            TalksRepository talksRepository = new TalksRepository(_talksContext);
            var talks = talksRepository.GetAll().ToList();

            //Assert
            Assert.Equal(1, talks.Count());
            var talk = talks.First();
            Assert.Equal("IntTest_SignalR", talk.Topic);
            Assert.Equal("IntTest_Ivan", talk.Speaker.FirstName);
            Assert.Equal("IntTest_Pupkin", talk.Speaker.LastName);
            Assert.Equal("IntTest_.NET", talk.Discipline.DisciplineName);
        }

        [Fact]
        public void ExecuteFilters_WhenOneRecordByFilterExists_ShouldReturnOneRecordWithRelatedRows()
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

            discipline = new Discipline { DisciplineName = "IntTest_JavaScript" };
            speaker = new Speaker
            {
                FirstName = "IntTest_Vasiliy",
                LastName = "IntTest_Mirinov",
                Position = "IntTest_SSE",
                Location = "IntTest_Moscow"
            };

            _talksContext.Talks.Add(new Talk
            {
                Location = "IntTest_L1, Room 12",
                Topic = "IntTest_JSPatterns",
                Discipline = discipline,
                Speaker = speaker
            });

            _talksContext.SaveChanges();

            TalkFilter filter = new TalkFilter
            {
                DisciplineName = "IntTest_.NET",
                Location = string.Empty,
                SpeakerName = string.Empty,
                Topic = string.Empty,
                DateFrom = null,
                DateTo = null
            };


            //Act
            TalksRepository talksRepository = new TalksRepository(_talksContext);
            var talks = talksRepository.ExecuteFilters(filter.FilterExpression).ToList();

            //Assert
            Assert.Equal(1, talks.Count());
            var talk = talks.First();
            Assert.Equal("IntTest_SignalR", talk.Topic);
            Assert.Equal("IntTest_Ivan", talk.Speaker.FirstName);
            Assert.Equal("IntTest_Pupkin", talk.Speaker.LastName);
            Assert.Equal("IntTest_.NET", talk.Discipline.DisciplineName);
        }

        [Fact]
        public void CreateTalk_WhenTalkCreated_ShouldReturnTalkId()
        {
            // Arrange
            var discipline = new Discipline
            {
                DisciplineName = "IntTest_.NET"
            };

            var speaker = new Speaker
            {
                FirstName = "IntTest_Ivan",
                LastName = "IntTest_Pupkin",
                Position = "IntTest_SE",
                Location = "IntTest_Minsk"
            };

            var talk = new Talk
            {
                Topic = "IntTest_test",
                Location = "IntTest_Room 67",
                AdditionalDetail = ""
            };

            DisciplinesRepository disciplinesRepository = new DisciplinesRepository(_talksContext);
            discipline = disciplinesRepository.Create(discipline);
            talk.DisciplineId = discipline.DisciplineId;

            SpeakersRepository speakersRepository = new SpeakersRepository(_talksContext);
            speaker = speakersRepository.Create(speaker);
            talk.SpeakerId = speaker.SpeakerId;

            //Act
            TalksRepository talksRepository = new TalksRepository(_talksContext);
            Talk actualResult = talksRepository.Create(talk);

            //Assert
            Assert.True(actualResult.TalkId > 0);
        }

        [Fact]
        public void UpdateTalk_WhenTalkUpdated_ShouldReturnUpdatedTalk()
        {
            // Arrange
            var discipline = new Discipline
            {
                DisciplineName = "IntTest_.NET"
            };

            var speaker = new Speaker
            {
                FirstName = "IntTest_Ivan",
                LastName = "IntTest_Pupkin",
                Position = "IntTest_SE",
                Location = "IntTest_Minsk"
            };

            var talk = new Talk
            {
                Topic = "IntTest_test",
                Location = "IntTest_Room 67",
                AdditionalDetail = ""
            };

            DisciplinesRepository disciplinesRepository = new DisciplinesRepository(_talksContext);
            discipline = disciplinesRepository.Create(discipline);
            talk.DisciplineId = discipline.DisciplineId;

            SpeakersRepository speakersRepository = new SpeakersRepository(_talksContext);
            speaker = speakersRepository.Create(speaker);
            talk.SpeakerId = speaker.SpeakerId;

            TalksRepository talksRepository = new TalksRepository(_talksContext);
            talk = talksRepository.Create(talk);

            //Act
            talk.Topic = "IntTest_test2";
            talksRepository.Update(talk);

            //Assert
            var actualResult = talksRepository.GetById(talk.TalkId);
            Assert.Equal("IntTest_test2", actualResult.Topic);
        }

        [Fact]
        public void DeleteTalk_WhenTalkDeleted_ShouldNotReturnDeletedTalk()
        {
            // Arrange
            var discipline = new Discipline
            {
                DisciplineName = "IntTest_.NET"
            };

            var speaker = new Speaker
            {
                FirstName = "IntTest_Ivan",
                LastName = "IntTest_Pupkin",
                Position = "IntTest_SE",
                Location = "IntTest_Minsk"
            };

            var talk = new Talk
            {
                Topic = "IntTest_test",
                Location = "IntTest_Room 67",
                AdditionalDetail = ""
            };

            DisciplinesRepository disciplinesRepository = new DisciplinesRepository(_talksContext);
            discipline = disciplinesRepository.Create(discipline);
            talk.DisciplineId = discipline.DisciplineId;

            SpeakersRepository speakersRepository = new SpeakersRepository(_talksContext);
            speaker = speakersRepository.Create(speaker);
            talk.SpeakerId = speaker.SpeakerId;

            TalksRepository talksRepository = new TalksRepository(_talksContext);
            talk = talksRepository.Create(talk);

            //Act
            talksRepository.DeleteById(talk.TalkId);

            //Assert
            var actualResult = talksRepository.GetById(talk.TalkId);
            Assert.Null(actualResult);
        }

        public void Dispose()
        {
            _talksContext.Database.EnsureDeleted();
        }
    }
}
