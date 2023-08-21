using System;
using System.Net.Http;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PoqAssignment.Application.Helpers;
using PoqAssignment.Application.Services;
using PoqAssignment.Domain.Contracts;
using PoqAssignment.Infrastructure;
using PoqAssignment.Infrastructure.Middleware;
using PoqAssignment.Infrastructure.OpenApi;
using PoqAssignment.Infrastructure.Repositories;
using PoqAssignment.Infrastructure.Repositories.Profiles;

namespace PoqAssignment.API
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
            services.AddOptions<MockySettings>().Configure<IConfiguration>((settings, configuration) =>
            {
                var configurationSection = configuration.GetSection(nameof(MockySettings));
                configurationSection.Bind(settings);
            });

            services.AddControllers()
                .AddJsonOptions(options => { options.JsonSerializerOptions.WriteIndented = true; });

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddAutoMapper(config => { config.AddMaps(typeof(MockyProfile).Assembly); });

            services.AddScoped<IMockyProductsRepository, MockyProductsRepository>();
            services.AddScoped<IFiltersService, FiltersService>();
            services.AddScoped<MockyProductsService>();
            services.AddScoped<ProductsStatisticsService>();
            services.AddScoped<ISerializationService, SerializationService>();

            services.AddTransient<ColorResolver>();

            services.AddHttpClient("MockyApiClient", (serviceProvider, httpClient) =>
            {
                var mockySettings = serviceProvider.GetRequiredService<IOptions<MockySettings>>().Value;
                httpClient.BaseAddress = new Uri(mockySettings.BaseUrl);
            });

            services.AddSingleton(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var mockyApiSettings = provider.GetRequiredService<IOptions<MockySettings>>();
        
                MockyApiClient.Initialize(httpClientFactory, mockyApiSettings);
        
                return MockyApiClient.Instance;
            });

            var settings = Configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = settings.Title,
                    Version = settings.Version,
                    Description = settings.Description
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}