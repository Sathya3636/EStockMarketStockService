using EStockMarketStockService.Services;
using EStockMarketStockService.Services.Interfaces;
using EStockMarketStockService.Application.Commands;
using EStockMarketStockService.Domain.Entities;
using EStockMarketStockService.Domain.Interfaces;
using EStockMarketStockService.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Collections.Generic;
using EStockMarketStockService.Application.Queries;
using EStockMarketStockService.Message.Receive;

namespace EStockMarketStockService
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
            var corsUrl = Configuration.GetSection("UIUrlforCors").Value;
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins(corsUrl).AllowAnyHeader());
            });

            services.AddControllers();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IStockService, StockService>();
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddTransient<IRequestHandler<AddStockCommand, Stock>, AddStockCommandHandler>();
            services.AddTransient<IRequestHandler<GetStockQuery, List<Stock>>, GetStockQueryHandler>();
            services.AddTransient<IRequestHandler<GetStockPriceQuery, List<Stock>>, GetStockPriceQueryHandler>();
            services.AddHostedService<DeleteStockRabbitMqService>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "EStock Stock Service API",
                    Version = "v1",
                    Description = "EStock Stock Service",
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


            var corsUrl = Configuration.GetSection("UIUrlforCors").Value;
            app.UseCors(options => options.WithOrigins(corsUrl).AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "EStock Stock Service"));
        }
    }
}
