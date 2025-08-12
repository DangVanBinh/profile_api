using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace profile_api.domain.Entities.User
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole(string name) : base(name)
        {

        }
    }
}
