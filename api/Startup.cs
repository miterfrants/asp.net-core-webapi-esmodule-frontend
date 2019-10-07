using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Sample.Data;
using Sample.Constants;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            AppSettings appSettings = new AppSettings();
            Configuration.GetSection("Config").Bind(appSettings);

            services.AddDbContext<DBContext>(options => options.UseSqlServer(appSettings.Secrets.DBConnectionString));
            services.Configure<AppSettings>(Configuration.GetSection("Config"));

            // add JWT secret to application layer 
            var encodedJwtSecret = Encoding.Default.GetBytes(appSettings.Secrets.JwtKey);
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
                    ValidateAudience = false,
                    RequireExpirationTime = true
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
                            message = ErrorHelper.GetErrorMessageByCode(ERROR_CODE.LOGIN_REQUIRE)
                        }));
                        return context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    },
                    OnTokenValidated = context =>
                    {
                        string authorization = context.Request.Headers["Authorization"];
                        string token = authorization.Substring("Bearer ".Length).Trim();
                        // Console.WriteLine(JWTHelper.DecodeToken(token)["exp"]);
                        DateTime expirationTime = new DateTime(1970, 1, 1).ToLocalTime().AddSeconds((int)JWTHelper.DecodeToken(token)["exp"]);
                        Console.WriteLine(expirationTime.ToLongTimeString());
                        if (expirationTime < DateTime.Now)
                        {
                            byte[] bytes = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                message = ErrorHelper.GetErrorMessageByCode(ERROR_CODE.TOKEN_EXPIRED)
                            }));
                            return context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        byte[] bytes = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            message = ErrorHelper.GetErrorMessageByCode(ERROR_CODE.UNAUTH_TOKEN)
                        }));
                        return context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Newtonsoft.Json.JsonConvert.SerializeObject(env);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();

        }
    }
}