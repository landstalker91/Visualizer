using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using Visualizer.Models;

namespace Visualizer
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //Чтобы кирилические символы не переводились в соответствующий Unicode Hex Character Code
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            //Get parameters variables
            Settings.CONNECTION_STRING = Configuration.GetConnectionString("DefaultConnection");
            Settings.NODE_TABLE_NAME = Configuration.GetSection("Node")["Tablename"];
            Settings.NODE_QUERY = Configuration.GetSection("Node")["Query"];
            Settings.NODE_LABEL_DEFAULT_COLOR = Configuration.GetSection("Node")["LabelDefaultColor"];
            Settings.NODE_LABEL_ROOT_COLOR = Configuration.GetSection("Node")["LabelRootColor"];
            Settings.NODE_PK = Configuration.GetSection("Node")["PK"];
            Settings.LINK_QUERY = Configuration.GetSection("Link")["Query"];
            Settings.LINK_TABLE_NAME = Configuration.GetSection("Link")["TableName"];
            Settings.LINK_DEFAULT_COLOR = Configuration.GetSection("Link")["DefaultColor"];
            Settings.LINK_PK = Configuration.GetSection("Link")["PK"];
            Settings.CLIENT_ID = Configuration.GetSection("Link")["ClientId"];
            Settings.RESOURCE_ID = Configuration.GetSection("Link")["ResourceId"];
            Settings.IMAGE_PATH = Configuration.GetSection("Parameters")["ImagePath"];
            Settings.IMAGE_EXTENSION = Configuration.GetSection("Parameters")["ImageExtension"];
            Settings.LINK_COLORS = Configuration.GetSection("Link")["Colors"].Split(",");
            Settings.LINK_SELECTED_COLOR = Configuration.GetSection("Link")["SelectedColor"];

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
