using AngularMVCCoreTechTalks;
using AngularMVCCoreTechTalks.Automapper.Profiles;
using AngularMVCCoreTechTalks.ViewModels;
using AutoMapper;
using BusinessLogic.Filters;
using DataAccess.EF;
using DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.IntegrationTests
{
    public class TalkControllerTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private TalksContext _talksContext;

        public TalkControllerTests()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;

            // Arrange
            var path = PlatformServices.Default.Application.ApplicationBasePath;
            var setDir = Path.GetFullPath(path);

            var _webHostBuilder = new WebHostBuilder()
                .UseContentRoot(setDir)
                .UseStartup<Startup>();

            var builder = new DbContextOptionsBuilder<TalksContext>();

            string testConnectionString =
            $"Server=(localdb)\\mssqllocaldb;Database=TalksDB_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true";
            builder.UseSqlServer(testConnectionString);

            _talksContext = new TalksContext(builder.Options);
            _talksContext.Database.EnsureCreated();

            _webHostBuilder.ConfigureServices(s => s.AddDbContext<TalksContext>(options =>
    options.UseSqlServer(testConnectionString)));

            _server = new TestServer(_webHostBuilder);
            _client = _server.CreateClient().AcceptJson();
        }

        [Fact]
        public async Task GetFilters_WhenRequestFilters_ReturnFilters()
        {
            // Act
            AutoMapper.Mapper.Reset();

            Mapper.Initialize(x =>
            {
                x.AddProfile<TalkToTalkFilterViewModelProfile>();
            });

            HttpResponseMessage response = await _client.GetAsync("/api/talk/GetFilters");
            TalkFilterViewModel talkFilterViewModel = await response.Content.ReadAsJsonAsync<TalkFilterViewModel>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(talkFilterViewModel.DisciplineList.Count > 0);
            Assert.True(talkFilterViewModel.LocationList.Count > 0);
        }

        [Fact]
        public async Task GetFilteredTalks_WhenRequestAllTalks_ReturnAllTalks()
        {
            // Act
            AutoMapper.Mapper.Reset();

            Mapper.Initialize(x =>
            {
                x.AddProfile<SelectedTalkFilterViewModelToTalkFilterProfile>();
                x.AddProfile<TalkToTalkViewModelProfile>();
            });

            SelectedTalkFilterViewModel talkFilter = new SelectedTalkFilterViewModel
            {
                DisciplineName = string.Empty,
                Location = string.Empty,
                SpeakerName = string.Empty,
                Topic = string.Empty,
                DateFrom = null,
                DateTo = null
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(talkFilter), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/talk/GetFilteredTalks", stringContent);
            IEnumerable<TalkViewModel> talks = await response.Content.ReadAsJsonAsync<IEnumerable<TalkViewModel>>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(talks);
        }

        [Fact]
        public async Task CreateTalk()
        {
            //Arrange
            Talk talk = new Talk
            {
                Topic = "IntTest_test",
                Location = "IntTest_Room 67",
                AdditionalDetail = "IntTest_",
                SpeakerId = 1,
                DisciplineId = 1
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(talk), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("api/talk/CreateTalk", stringContent);
            talk = await response.Content.ReadAsJsonAsync<Talk>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdateTalk()
        {
            //Arrange
            Talk talk = new Talk
            {
                TalkId = 1,
                Topic = "IntTest_test",
                Location = "IntTest_Room 67",
                AdditionalDetail = "IntTest_",
                SpeakerId = 85,
                DisciplineId = 1
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(talk), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PutAsync($"api/talk/UpdateTalk/{talk.TalkId}", stringContent);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public void DeleteTalk()
        {
            //Arrange
            Talk newTalk = new Talk
            {
                TalkDate = new System.DateTime(2018, 9, 9),
                Topic = "IntTest_test",
                Location = "IntTest_Room 67",
                AdditionalDetail = "IntTest_",
                SpeakerId = 1,
                DisciplineId = 1
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(newTalk), Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage();

            // Act
            Task t = new Task(() =>
            {
                response = _client.PostAsync("api/talk/CreateTalk", stringContent).GetAwaiter().GetResult();
                Talk createdTalk = response.Content.ReadAsJsonAsync<Talk>().GetAwaiter().GetResult();
                response = _client.DeleteAsync("api/talk/DeleteTalk/" + createdTalk.TalkId).GetAwaiter().GetResult();
            });
            t.RunSynchronously();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        public void Dispose()
        {
            _talksContext.Database.EnsureDeleted();
        }
    }
}
