﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGenerator.Data;
using DataGenerator.Models;
using DataGenerator.Models.Errors;
using DataGenerator.Services;
using DataGenerator.Services.Relationships;
using DataGenerator.Services.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataGenerator
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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<SQLServerContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection")));
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection")));
            services.AddDbContext<OptionsContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection")));
            services.AddScoped<IOptionsProvider, OptionsProvider>();
            services.AddScoped<ColumnGenerator>();
            //services.AddScoped<CSVTableGenerator>();
            services.AddScoped<ITableGenerator, TableGenerator>();
            // services.AddScoped<CreateRelationColumns>();
            services.AddScoped<RelationshipController>();
            services.AddScoped<CSVTableGenerator>();
            services.AddScoped<Func<string, ITableGenerator>>(serviceProvider => key =>
            {
                return serviceProvider.GetService<TableGenerator>();
            });
            services.AddScoped<Zipper>();
            services.AddScoped<RelationshipsValidator>();
            services.AddScoped<InputDataValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // else
            //  {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //   app.UseHsts();
            //}
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseMiddleware<CustomExceptionMiddleware>();
            app.UseMvc();
        }
    }
}
