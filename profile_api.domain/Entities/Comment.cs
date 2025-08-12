using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using profile_api.domain.Entities.Common;
using profile_api.domain.Entities.User;

namespace profile_api.domain.Entities
{
    public class Comment : BaseEntity
    {
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }
        public Comment? ParentComment { get; set; }
        public bool IsApproved { get; set; } = true;
    }
}
