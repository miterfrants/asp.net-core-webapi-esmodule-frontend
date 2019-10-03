using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Sample.Constants;
using Sample.Data;
using Sample.Helpers;

namespace Sample
{
    public class Startup
    {
        [System.Obsolete]
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"secrets.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            AppSettings appSettings = new AppSettings();
            Configuration.GetSection("Config").Bind(appSettings);

            services.AddDbContext<SampleContext>(options => options.UseSqlServer(appSettings.Secrets.DBConnectionString));
            services.Configure<AppSettings>(Configuration.GetSection("Config"));

            // add JWT secret to application layer 
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(appSettings));
            var encodedJwtSecret = Encoding.ASCII.GetBytes(appSettings.Secrets.JwtKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(encodedJwtSecret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        byte[] bytes = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = CUSTOM_RESPONSE.STATUS.FAILED.ToString(),
                            data = new
                            {
                                message = "此頁面需要登入"
                            }
                        }));
                        return context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        byte[] bytes = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = CUSTOM_RESPONSE.STATUS.FAILED.ToString(),
                            data = new
                            {
                                message = "憑證失效"
                            }
                        }));
                        return context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
                endpoints.MapControllers();
            });
        }
    }
}
