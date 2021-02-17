using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Playlistofy.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Playlistofy
{
    public class Startup
    {
        private string _spotifyClientId = null;
        private string _spotifyClientSecret = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(options =>
<<<<<<< HEAD
                options.UseSqlServer(
                    Configuration.GetConnectionString("PlaylistofyAzureCS")));
=======
                options.UseSqlServer(Configuration.GetConnectionString("PlaylistofyAzureCS")));

            services.AddDatabaseDeveloperPageExceptionFilter();
            /*----------------------------------------------------------------------------------------*/

>>>>>>> ab020f0eb5c9e258711a0c08346a97708819a152
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            _spotifyClientId = Configuration["Spotify:ClientId"];
            _spotifyClientSecret = Configuration["Spotify:ClientSecret"];
            services.AddAuthentication()
                .AddSpotify(options => {
                    options.ClientId = _spotifyClientId;
                    options.ClientSecret = _spotifyClientSecret;
                    options.CallbackPath = "/Index";
                    options.Events.OnRemoteFailure = (context) =>
                        {
                            // Handle failed login attempts here
                            return Task.CompletedTask;
                        };
                    options.SaveTokens = true;
                    });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
