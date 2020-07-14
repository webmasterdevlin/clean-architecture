using System;
using System.IO;
using System.Reflection;
using ApplicationCore.interfaces;
using Infrastructure.Data.Configurations.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Web.Services;
using AutoMapper;
using MediatR;
using Microsoft.OpenApi.Models;
using Prometheus;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Web
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
            services.Configure<TourOfHeroesDatabaseSettings>(
                Configuration.GetSection(nameof(TourOfHeroesDatabaseSettings))
                );

            services.AddSingleton<ITourOfHeroesDatabaseSettings>(provider =>
                provider.GetRequiredService<IOptions<TourOfHeroesDatabaseSettings>>().Value);

            services.AddSingleton<IHeroRepository, HeroRepository>();

            services.AddMediatR(typeof(Startup));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tour of Heroes API",
                    Version = "v1",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api-docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 Docs");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles(); // Front-end should be separated from here

            app.UseRouting();

            app.UseMetricServer(); // Prometheus Monitoring Exporter

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}