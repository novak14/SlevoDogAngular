using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SlevoDogAngular.Data;
using SlevoDogAngular.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using AutoMapper;
using SlevoDogAngular.Models.AdminViewModels;
using Admin.Dal.Entities;
using SlevoDogAngular.Models.CatalogViewModels;
using Catalog.Dal.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SlevoDogAngular.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SlevoDogAngular
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SlevoDogLocal")));

            // services.ConfigureSwaggerGen(options =>
            // {
            //     options.CustomSchemaIds(x => x.FullName);
            // });

            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            // });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                           {
                               options.TokenValidationParameters = new TokenValidationParameters
                               {
                                   ValidateIssuer = true,
                                   ValidateAudience = true,
                                   ValidateLifetime = true,
                                   ValidateIssuerSigningKey = true,

                                   ValidIssuer = "https://localhost:5001",
                                   ValidAudience = "https://localhost:5001",
                                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                               };
                           });

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SaleAdminViewModel, SaleAdmin>();
                cfg.CreateMap<Comments, CommentsViewModel>();
            });

            services.AddScoped<AccountService, AccountService>();
            services.AddScoped<HelpService, HelpService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // services.AddModuleCatalog(o => o.connectionString = Configuration.GetConnectionString("DefaultConnection"));
            // services.AddModuleAdmin(o => o.connectionString = Configuration.GetConnectionString("DefaultConnection"));


            services.AddModuleCatalog(o => o.connectionString = Configuration.GetConnectionString("SlevoDogLocal"));
            services.AddModuleAdmin(o => o.connectionString = Configuration.GetConnectionString("SlevoDogLocal"));
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // app.UseSwagger();
            // app.UseSwaggerUI(c =>
            // {
            //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            // });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
