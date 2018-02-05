using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppTracker.Models.DB;
using Microsoft.EntityFrameworkCore;
using AppTracker.Models.Repositories;
using AppTracker.Models.Repositories.Interfaces;

namespace AppTracker
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
            // EF DB Setup
            services.AddDbContext<AppTrackerDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppTrackerDB")));

            // Repositories
            services.AddTransient<IContactRepo, ContactRepo>();
            services.AddTransient<ICompanyRepo, CompanyRepo>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
