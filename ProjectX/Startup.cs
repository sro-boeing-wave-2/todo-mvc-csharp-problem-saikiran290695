﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ProjectX.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace ProjectX
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment IhostEnv)
        {
            Configuration = configuration;
            Ihost = IhostEnv;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Ihost { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            if (Ihost.IsDevelopment())
                services.AddDbContext<NoteContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("NoteContext")));
            else
            {
                services.AddDbContext<NoteContext>(options =>
                    options.UseInMemoryDatabase("NoteContext"));
            }
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

            });
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
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
