using AutoMapper;
using DataAccess.EF;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using DataAccess.Services;
using DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AngularMVCCoreTechTalks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TalksContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("TalksEntity")));

            services.AddTransient(typeof(IServiceBase<>), typeof(BaseService<>));
            services.AddTransient<ITalkService, TalkService>();
            services.AddTransient<IDisciplineService, DisciplineService>();
            services.AddTransient<ISpeakerService, SpeakerService>();

            services.AddTransient(typeof(IRepositoryBase<>), typeof(BaseRepository<>));
            services.AddTransient<ISpeakersRepository, SpeakersRepository>();
            services.AddTransient<ITalksRepository, TalksRepository>();
            services.AddTransient<IDisciplinesRepository, DisciplinesRepository>();

            services.AddAutoMapper();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
