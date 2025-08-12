using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using profile_api.domain.Entities.User;
using profile_api.domain.Entities.Common;

namespace profile_api.domain.Entities
{
    public class AuditLog : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }

        [Required, StringLength(255)]
        public string? Action { get; set; }

        [StringLength(50)]
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}
