using AngularMVCCoreTechTalks;
using DataAccess.EF;
using DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.IntegrationTests
{
    public class SpeakerControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SpeakerControllerTests()
        {
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
        public async Task GetSpeakerById()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("api/speaker/GetSpeakerById/1");
            Speaker speaker = await response.Content.ReadAsJsonAsync<Speaker>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(speaker);
        }
    }
}
