using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace profile_api.domain.Entities.User
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostImage>? Files { get; set; }
        public ICollection<AuditLog>? AuditLogs { get; set; }
    }
}
