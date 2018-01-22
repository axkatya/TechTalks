using AngularMVCCoreTechTalks;
using AngularMVCCoreTechTalks.ViewModels;
using DataAccess.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.IntegrationTests
{
    public class TalkControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TalkControllerTests()
        {
            AutoMapper.Mapper.Reset();

            // Arrange
            var path = PlatformServices.Default.Application.ApplicationBasePath;
            var setDir = Path.GetFullPath(path);

            var _webHostBuilder = new WebHostBuilder()
                .UseContentRoot(setDir)
                .UseStartup<Startup>();
            _webHostBuilder.ConfigureServices(s => s.AddDbContext<TalksContext>(options =>
    options.UseSqlServer("Server=EPBYMOGW0013;Database=TalksDB;Integrated Security=True;MultipleActiveResultSets=True")));

            _server = new TestServer(_webHostBuilder);
            _client = _server.CreateClient().AcceptJson();
        }

        [Fact]
        public async Task GetFilters_WhenRequestFilters_ReturnFilters()
        {
            // Act
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
            TalkFilterViewModel talkFilterViewModel = new TalkFilterViewModel
            {
                DisciplineName = string.Empty,
                Location = string.Empty,
                SpeakerName = string.Empty,
                Topic = string.Empty,
                DateFrom = null,
                DateTo = null
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(talkFilterViewModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/talk/GetFilteredTalks", stringContent);
            IEnumerable<TalkViewModel> talks = await response.Content.ReadAsJsonAsync<IEnumerable<TalkViewModel>>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(talks);
            Assert.True(talks.ToList().Count > 0);
        }
    }
}
