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
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.IntegrationTests
{
    public class DisciplineControllerTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private TalksContext _talksContext;

        public DisciplineControllerTests()
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
        public async Task CreateDiscipline()
        {
            Discipline discipline = new Discipline
            {
                DisciplineName = "IntTest_Jess"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(discipline), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("api/discipline/CreateDiscipline", stringContent);
            discipline = await response.Content.ReadAsJsonAsync<Discipline>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        public void Dispose()
        {
            _talksContext.Database.EnsureDeleted();
        }
    }
}
