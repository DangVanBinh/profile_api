using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using profile_api.domain.Entities.User;

namespace profile_api.domain.DefaultInitializeData
{
    public class ApplicationRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static List<AppRole> GetDefaultRoles()
        {
            var roles = new List<AppRole>
            {
                new(Admin),
                new(User)
            };
            return roles;
        }
    }
}
