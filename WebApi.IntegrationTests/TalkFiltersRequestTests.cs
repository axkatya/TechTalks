using AngularMVCCoreTechTalks;
using DataAccess.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class TalkFiltersRequestTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public TalkFiltersRequestTests()
        {
            // Arrange
            //_server = new TestServer(new WebHostBuilder()
            //    .UseStartup<Startup>());
            //_client = _server.CreateClient();

            //    var webHostBuilder = new WebHostBuilder()
            //.UseContentRoot(CalculateRelativeContentRootPath())
            //.UseEnvironment("Development")
            //.UseStartup<AngularMVCCoreTechTalks.Startup>()
            //.UseApplicationInsights();
            //    var testServer = new TestServer(webHostBuilder);
            //    _client = testServer.CreateClient();
            //    string CalculateRelativeContentRootPath() =>
            //      Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
            //         @"..\..\..\..\AngularMVCCoreTechTalks");

            string curDir = Directory.GetCurrentDirectory();
            var _webHostBuilder = new WebHostBuilder()
                .UseContentRoot(curDir)
                .UseStartup<Startup>();
            _server = new TestServer(_webHostBuilder);
            _client = _server.CreateClient();

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<TalksContext>();

            builder.UseSqlServer($"Server=EPBYMOGW0013;Database=TalksDB;Integrated Security=True;MultipleActiveResultSets=True")
                 .UseInternalServiceProvider(serviceProvider);

        }

        [Fact]
        public async Task GetFilters()
        {
            // Act
            var response = await _client.GetAsync("/api/talk/GetFilters");

            var responseString = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }

            // Assert
            Assert.Equal("Hello World!",
                responseString);
        }


    }
}
