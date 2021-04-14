using Autofac;
using CoreTest.Config;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using AppSetting;
using K4os.Hash.xxHash;
using Microsoft.IdentityModel.Tokens;


namespace CoreTest
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
            services.AddControllers();

            #region Swagger




            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("V1", new OpenApiInfo()
                {
                    Title = "API",
                    Description = "²âÊÔ",
                    Version = "v1",

                });
                x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml"), true);
            });

            #endregion

            #region ids

            var builder = services.AddIdentityServer()
                .AddDeveloperSigningCredential()        //This is for dev only scenarios when you don¡¯t have a certificate to use.
                .AddInMemoryApiScopes(IdsConfig.ApiScope)
                .AddInMemoryClients(IdsConfig.Clients);

            // .AddInMemoryApiResources(IdsConfig.Apis);
            //.AddInMemoryIdentityResources(IdsConfig.GetIdentityResources());


            //idsÊÚÈ¨

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5003";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                    options.RequireHttpsMetadata = false;
                });


            #endregion

        }
        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterTypes(Assembly.Load("Repository").GetExportedTypes()).AsImplementedInterfaces();
            builder.RegisterTypes(Assembly.Load("Service").GetExportedTypes()).AsImplementedInterfaces();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            #region swagger

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/V1/swagger.json", "API v1");
                x.RoutePrefix = "swagger";
                x.DocumentTitle = "Swagger API";
            });

            #endregion


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }



    }
}
