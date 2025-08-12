using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using profile_api.domain.Entities.User;

namespace profile_api.domain.DefaultInitializeData
{
    public class ApplicationAccount
    {
        public static AppUser GetAppUser()
        {
            Guid id = Guid.NewGuid();
            var defaultUser = new AppUser
            {
                Id = id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserName = "admin",
                FullName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "dangbinh4102@gmail.com",
                NormalizedEmail = "dangbinh4102@gmail.com".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            return defaultUser;
        }
    }
}
