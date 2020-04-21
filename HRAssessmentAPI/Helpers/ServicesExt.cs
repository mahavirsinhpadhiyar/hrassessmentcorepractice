using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Context;
using Shared.Entities;
using Shared.Repositories.GenericRepository;
using Shared.Services;
using Shared.UnitOfWork;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAssessmentAPI.Helpers
{
    public static class ServicesExt
    {
        public static void ConfigureUOWAndRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IConsultantService, ConsultantService>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
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
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

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
        }

        public static void ConfigureContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HRADbContext>(item => item.UseSqlServer(configuration.GetConnectionString("hrConn")));
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<HRADbContext>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
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
            });
        }
    }
}
