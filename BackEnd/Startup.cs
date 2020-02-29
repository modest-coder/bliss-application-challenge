using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Business.DbConfigurations;
using Microsoft.OpenApi.Models;
using API.MappingProfiles;
using Business.Services;
using Business.Settings;
using API.Extensions;
using AutoMapper;
using System;

namespace BackEnd
{
    public class Startup
    {
        private const string CURRENT_API_VERSION = "v0.1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BusinessToDtoProfile());
                mc.AddProfile(new DtoToBusinessProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddDbContext<DataAccessContext>(options =>
                options.UseSqlite($"Filename={AppDomain.CurrentDomain.BaseDirectory}/{Configuration.GetConnectionString("DefaultConnection")}")
            );

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(CURRENT_API_VERSION, new OpenApiInfo { Title = "B.A. Challenge API", Version = CURRENT_API_VERSION });
            });

            services.AddTransient<QuestionsService>();
            services.AddTransient<ShareService>();
            services.AddTransient<EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{CURRENT_API_VERSION}/swagger.json", "B.A. Challenge API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
