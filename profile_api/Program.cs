
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using profile_api.domain;
using profile_api.domain.Entities.User;
using profile_api.domain.Helpers;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace profile_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // C?u hình password
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // Lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // User
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders(); // Dùng cho t?o token xác nh?n

            // 3. C?u hình JWT (n?u b?n dùng xác th?c b?ng token)
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            builder.Services.AddSingleton(new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],

                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
        builder.Configuration["Jwt:Key"])),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllers();

            app.Run();
        }
    }
}
