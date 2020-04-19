using HRAssessmentAPI.Helpers;
using HRAssessmentAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Context;
using Shared.Entities;
using Shared.Repositories.ConsultantRepository;
using Shared.Repositories.UserRepository;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAssessmentAPI
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
            // var corsBuilder = new CorsPolicyBuilder();
            // corsBuilder.AllowAnyHeader();
            // corsBuilder.AllowAnyMethod();
            // corsBuilder.AllowAnyOrigin();// For anyone access.
            // corsBuilder.AllowCredentials();
            // corsBuilder.WithOrigins("http://localhost:4200/"); // for a specific url. Don't add a forward slash on the end!

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    // builder => {
                    //     builder.WithOrigins("http://localhost:4200");
                    // }
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
                // options.AddPolicy("SiteCorsPolicy",corsBuilder.Build());
                // builder =>  builder.AllowAnyOrigin()
                // .AllowAnyMethod()
                // .AllowAnyHeader()
                // .AllowCredentials());
            });

            // services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HRAssessmentAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme()
                    {
                        In = "header",
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "Bearer",
                        Enumerable.Empty<string>()
                    }
                });
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IConsultantRepository, ConsultantRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddMvc(
            //Apply authorize globally
            //options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                    .RequireAuthenticatedUser()
            //                    .Build();

            //    options.Filters.Add(new AuthorizeFilter(policy));
            //}
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //Asif jwt configuration
            var jwtSettings = appSettingsSection.Get<AppSettings>();
            services.AddAuthentication().AddCookie(config =>
            {
                config.SlidingExpiration = true;
                config.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(10));
                //config.CookieSecure = CookieSecurePolicy.Always;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };
                });

            // configure jwt authentication
            // var appSettings = appSettingsSection.Get<AppSettings>();
            // var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            // services.AddAuthentication(x =>
            // {
            //     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // })
            // .AddJwtBearer(x =>
            // {
            //     //x.RequireHttpsMetadata = false;
            //     //x.SaveToken = true;
            //     //x.TokenValidationParameters = new TokenValidationParameters
            //     //{
            //     //    ValidateIssuerSigningKey = true,
            //     //    IssuerSigningKey = new SymmetricSecurityKey(key),
            //     //    ValidateIssuer = false,
            //     //    ValidateAudience = false
            //     //};

            //      x.TokenValidationParameters = new TokenValidationParameters
            //      {
            //          ValidateIssuer = true,
            //          ValidateAudience = true,
            //          ValidateLifetime = true,
            //          ValidateIssuerSigningKey = true,
            //          ValidIssuer = appSettings.Issuer,
            //          ValidAudience = appSettings.Audience,
            //          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret))
            //      };
            // });

            services.AddDbContext<HRADbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("hrConn")));
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<HRADbContext>();
            //services.AddSpaStaticFiles();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRAssessmentAPI V1");
            });

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            // app.UseSpaStaticFiles();

            app.UseAuthentication();
            // app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            // app.UseSpa(spa =>
            // {
            //     // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //     // see https://go.microsoft.com/fwlink/?linkid=864501

            //     spa.Options.SourcePath = "ClientApp";

            //     if (env.IsDevelopment())
            //     {
            //         spa.UseAngularCliServer(npmScript: "start");
            //     }
            // });
        }
    }
}
