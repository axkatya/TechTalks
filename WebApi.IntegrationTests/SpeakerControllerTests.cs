using AngularMVCCoreTechTalks;
using AutoMapper;
using DataAccess.EF;
using DataAccess.Entities;
using IntegrationTestsCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.IntegrationTests
{
    public class SpeakerControllerTests : IntegrationTestHelper, IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SpeakerControllerTests()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;

            // Arrange
            var path = PlatformServices.Default.Application.ApplicationBasePath;
            var setDir = Path.GetFullPath(path);

            var _webHostBuilder = new WebHostBuilder()
                .UseContentRoot(setDir)
                .UseStartup<Startup>();

            CreateTestDatabase();

            _webHostBuilder.ConfigureServices(s => s.AddDbContext<TalksContext>(options =>
    options.UseSqlServer(Constants.TestConnectionString)));

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

        [Fact]
        public async Task CreateSpeaker()
        {
            Speaker speaker = new Speaker
            {
                FirstName = "IntTest_Jess",
                LastName = "IntTest_Amanda",
                Location = "IntTest_Room 45",
                Department = "IntTest_Java",
                Position = "IntTest_D2"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(speaker), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("api/speaker/CreateSpeaker", stringContent);
            speaker = await response.Content.ReadAsJsonAsync<Speaker>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdateSpeaker()
        {
            Speaker speaker = new Speaker
            {
                SpeakerId = 1,
                FirstName = "IntTest_test",
                LastName = "IntTest_Amanda",
                Location = "IntTest_Room 45",
                Department = "IntTest_Java",
                Position = "IntTest_D2"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(speaker), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PutAsync("api/speaker/UpdateSpeaker/1", stringContent);
            speaker = await response.Content.ReadAsJsonAsync<Speaker>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        public void Dispose()
        {
            DetachTestDatabase();
            DeleteTestDatabase();
        }
    }
}
