using Assingment.Model;
using Assingment.Repository;
using Assingment.TokenManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using OMDbApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assingment
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
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                var key = Configuration.GetValue<string>("JwtConfig:Key");
                var keyBytes = Encoding.ASCII.GetBytes(key);
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                 
                };
            });
            services.AddSingleton(typeof(IJwtTokenManager), typeof(JwtTokenManager)); 

            services.Configure<Settings>(
                options =>
                {
                    options.ConnectionString =
                        Configuration.GetSection("MongoDb:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoDb:Database").Value;
                    options.OmdbApiKey = Configuration.GetSection("ApiKey:Omdb").Value;
                });

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddSingleton<IMongoClient, MongoClient>(
                _ => new MongoClient(Configuration.GetSection("MongoDb:ConnectionString").Value));

            services.AddTransient<IDBContext, DBContext>();
            services.AddTransient<IOmdbSearchRepository, OmdbSearchRepository>();
            
            //services.TryAddSingleton<IOmdbClient>(new OmdbClient(Configuration.GetSection("ApiKey:Omdb").Value));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
