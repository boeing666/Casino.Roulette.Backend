#define MOCKING

using System;
using Autofac;
using Casino.Roulette.Backend.Hubs;
using Casino.Roulette.Backend.Interfaces;
using Casino.Roulette.Backend.Interfaces.Repository;
using Casino.Roulette.Backend.Interfaces.Services;
using Casino.Roulette.Backend.Models;
using Casino.Roulette.Backend.Repository.Database;
using Casino.Roulette.Backend.Repository.Mocking;
using Casino.Roulette.Backend.Services;
using Casino.Roulette.Backend.Services.Managers;
using Casino.Roulette.Backend.Services.Roulette;
using Casino.Roulette.Backend.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Casino.Roulette.Backend
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
            services.AddSignalR();
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader().
                            AllowAnyMethod().
                            WithOrigins("https://localhost",
                                        "http://localhost",
                                        "https://localhost:4200",
                                        "http://localhost:4200",
                                        "https://localhost:4500",
                                        "http://localhost:4500")
                            .AllowCredentials();
                });

            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<RouletteEngine>().As<IRouletteEngine>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<HubMessageBroker>().As<IMessageBroker>();
            builder.RegisterType<UserManager>().SingleInstance();
            builder.RegisterType<TableManager>().SingleInstance();
            builder.RegisterType<ValidationManager>().SingleInstance();
            builder.RegisterType<MerchantManager>().SingleInstance();
            builder.RegisterType<RouletteTable>();

            builder.RegisterType<UserRepository>().As<IUserRepository>();
            
#if MOCKING
            builder.RegisterType<MockUserRepository>().As<IUserRepository>();
            builder.RegisterType<MockRoundRepository>().As<IRoundRepository>();
#endif


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<RouletteHub>("/roulette");
                endpoints.MapControllers();
            });

            var rouletteEngine = app.ApplicationServices.GetService<IRouletteEngine>();

        }
    }
}
