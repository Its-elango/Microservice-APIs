﻿using E_Commerce.DTO;
using E_Commerce.Repository;
using E_Commerce.Repository.Interface;
using E_Commerce.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDatabaseSettings(this IServiceCollection services)
        {
            services?.AddTransient(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return configuration.GetConnectionString("connect");
            });
            return services;
        }
        public static IServiceCollection RegisterAuthendicationSettings(this IServiceCollection services)
        {
            services.AddTransient(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return configuration.GetSection("JWT").Get<JWTSettings>();
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 var jwtSettings = services.BuildServiceProvider().GetService<IConfiguration>().GetSection("JWT");
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtSettings["Issuer"],
                     ValidAudience = jwtSettings["Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                 };
             });
            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAdmin,adminRepository>();
            services.AddTransient<loginRepository>();
            services.AddTransient<ICustomer,customerRepository>();
            services.AddTransient<ISeller,sellerRepository>();
            return services;
        } 
        public static IServiceCollection RegisterApiInvoker(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<InvokeApi>();
            
            return services;
        }
    }
}
