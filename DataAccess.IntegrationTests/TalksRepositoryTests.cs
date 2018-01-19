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
            var discipline = new Discipline { DisciplineName = ".NET" };
            var speaker = new Speaker { FirstName = "Ivan", LastName = "Pupkin", Position = "SE", Location = "Minsk" };
            _talksContext.Talks.Add(new Talk
            {
                Location = "L3, Room 54",
                Topic = "SignalR",
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
            Assert.Equal("SignalR", talk.Topic);
            Assert.Equal("Ivan", talk.Speaker.FirstName);
            Assert.Equal("Pupkin", talk.Speaker.LastName);
            Assert.Equal(".NET", talk.Discipline.DisciplineName);
        }

        [Fact]
        public void ExecuteFilters_WhenOneRecordByFilterExists_ShouldReturnOneRecordWithRelatedRows()
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

            discipline = new Discipline { DisciplineName = "JavaScript" };
            speaker = new Speaker { FirstName = "Vasiliy", LastName = "Mirinov", Position = "SSE", Location = "Moscow" };
            _talksContext.Talks.Add(new Talk
            {
                Location = "L1, Room 12",
                Topic = "JSPatterns",
                Discipline = discipline,
                Speaker = speaker
            });

            _talksContext.SaveChanges();

            TalkFilter filter = new TalkFilter
            {
                DisciplineName = ".NET",
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
            Assert.Equal("SignalR", talk.Topic);
            Assert.Equal("Ivan", talk.Speaker.FirstName);
            Assert.Equal("Pupkin", talk.Speaker.LastName);
            Assert.Equal(".NET", talk.Discipline.DisciplineName);
        }

        public void Dispose()
        {
            _talksContext.Database.EnsureDeleted();
        }
    }
}
