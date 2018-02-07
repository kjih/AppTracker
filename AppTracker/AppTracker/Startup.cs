using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppTracker.Models.DB;
using Microsoft.EntityFrameworkCore;
using AppTracker.Models.Repositories;
using AppTracker.Models.Repositories.Interfaces;
using AppTracker.Providers.Interfaces;
using AppTracker.Providers;

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
            // EF Context
            services.AddDbContext<AppTrackerDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppTrackerDB")));

            // Repositories
            services.AddTransient<IContactRepo, ContactRepo>();
            services.AddTransient<ICompanyRepo, CompanyRepo>();
            services.AddTransient<IApplicationRepo, ApplicationRepo>();
            services.AddTransient<IApplicationStatusRepo, ApplicationStatusRepo>();

            // Providers
            services.AddTransient<IMetricsProvider, MetricsProvider>();

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
