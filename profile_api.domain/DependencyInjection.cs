using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace profile_api.domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Đăng ký Services
            //services.AddScoped<IProductService, ProductService>();

            // Đăng ký Repositories
            //services.AddScoped<IProductRepository, ProductRepository>();

            // Đăng ký DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Đăng ký thêm các service khác nếu có
            // services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
