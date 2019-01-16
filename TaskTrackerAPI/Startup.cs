using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using TaskTrackerAPI.DAL;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.DAL.Repositories;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(
                opt => opt.CreateMissingTypeMaps = true,
                Assembly.GetEntryAssembly());

            services.AddAutoMapper();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                  builder => builder.AllowAnyOrigin());
            });


            
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddTransient<LifetimeTest>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IMapper autoMapper, AppDbContext appCtx )
        {
            autoMapper.ConfigurationProvider.AssertConfigurationIsValid();

            loggerFactory.AddConsole(); 

            loggerFactory.AddDebug();

            loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());

            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllOrigins");

            appCtx.Seed();

            app.UseStatusCodePages();

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Internets are broken!" 
                    
                    );
            });
        }
    }
}
