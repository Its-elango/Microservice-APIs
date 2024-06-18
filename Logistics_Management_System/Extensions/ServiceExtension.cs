using Logistics_Management_System.Models.Core;
using Logistics_Management_System.Respository;
using Logistics_Management_System.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Logistics_Management_System.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<ILogistics, LogisticsRepository>();
            services.AddTransient<Connection>();
            return services;
        }

        public static IServiceCollection RegisterClientApi(this IServiceCollection services)
        {
            services.AddHttpClient(); 
            services.AddTransient<InvokeApi>();
            return services;
        }


        public static IServiceCollection RegisterAuthenticationSettings(this IServiceCollection services)
        {
            services.AddTransient(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return configuration.GetSection("JWT").Get<JWTSettings>() ?? new JWTSettings();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
                 var jwtSettings = configuration?.GetSection("JWT");

                 if (jwtSettings != null)
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = jwtSettings["Issuer"],
                         ValidAudience = jwtSettings["Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? ""))
                     };
                 } });
            services.AddTransient<TokenService>(); 
            return services;
        }
    }
}
