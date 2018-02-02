using AngularMVCCoreTechTalks;
using AutoMapper;
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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.IntegrationTests
{
    public class SpeakerControllerTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private TalksContext _talksContext;

        public SpeakerControllerTests()
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

        [Fact]
        public async Task GetSpeakerById()
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
            HttpResponseMessage response = await _client.PostAsync("api/speaker/CreateSpeaker", stringContent);
            Speaker createdSpeaker = await response.Content.ReadAsJsonAsync<Speaker>();

            // Act
            HttpResponseMessage actualResponse = await _client.GetAsync($"api/speaker/GetSpeakerById/{createdSpeaker.SpeakerId}");
            Speaker actualSpeaker = await response.Content.ReadAsJsonAsync<Speaker>();

            // Assert
            Assert.True(actualResponse.IsSuccessStatusCode);
            Assert.NotNull(actualSpeaker);
        }

        [Fact]
        public async Task GetSpeakers()
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
            HttpResponseMessage response = await _client.PostAsync("api/speaker/CreateSpeaker", stringContent);
            Speaker createdSpeaker = await response.Content.ReadAsJsonAsync<Speaker>();

            // Act
            List<Speaker> actualSpeakers = null;

            var task = _client.GetAsync($"api/speaker/GetSpeakers")
          .ContinueWith((taskwithresponse) =>
          {
              response = taskwithresponse.Result;
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              actualSpeakers = JsonConvert.DeserializeObject<List<Speaker>>(jsonString.Result);

          });
            task.Wait();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(actualSpeakers);
        }

        public void Dispose()
        {
            _talksContext.Database.EnsureDeleted();
        }
    }
}
