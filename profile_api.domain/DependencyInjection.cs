using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using profile_api.domain.Handlers;
using profile_api.domain.Repositories;
using profile_api.domain.Repositories.Interfaces;
using profile_api.domain.Services;
using profile_api.domain.Services.Interfaces;

namespace profile_api.domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Đăng ký Services
            services.AddScoped<IAuthService, AuthService>();

            // Đăng ký Repositories
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<CategoryHandler>();

            // Đăng ký DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Đăng ký thêm các service khác nếu có
            // services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
