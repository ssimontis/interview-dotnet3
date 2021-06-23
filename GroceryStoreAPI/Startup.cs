﻿using System;
using System.IO;
using System.Reflection;
using FluentValidation;
using GroceryStoreAPI.Commands;
using GroceryStoreAPI.Dal;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Queries;
using GroceryStoreAPI.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GroceryStoreAPI
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
            services.AddDbContext<CustomerContext>(cfg =>
                cfg.UseInMemoryDatabase("Customers"));

            services.AddScoped<IDatabaseLoader, JsonDatabaseLoader>();

            services.AddScoped<IValidator<CustomerQueryRequest>, CustomerRequestValidator>();
            services.AddScoped<IValidator<NewCustomerRequest>, NewCustomerValidator>();
            services.AddScoped<IValidator<UpdateCustomerRequest>, UpdateCustomerValidator>();

            services.AddScoped<CustomerQuery>();
            services.AddScoped<CustomersQuery>();
            services.AddScoped<NewCustomerCommand>();
            services.AddScoped<UpdateCustomerCommand>();
            
            services.AddControllers();

            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo {Title = "Grocery Store API", Version = "v1"});
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(cfg =>
                {
                    cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Grocery Store API");
                    cfg.RoutePrefix = string.Empty;
                });

                var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

                using var scope = scopeFactory.CreateScope();
                var initializer = scope.ServiceProvider.GetService<IDatabaseLoader>();
                initializer.LoadData();
            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
