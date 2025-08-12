using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using profile_api.domain.Entities.Common;
using profile_api.domain.Entities.User;

namespace profile_api.domain.Entities
{
    public class PostImage : BaseEntity
    {
        [Required, StringLength(255)]
        public string? FileName { get; set; }

        [Required]
        public string? FileUrl { get; set; }

        [StringLength(50)]
        public string? FileType { get; set; }

        public long? FileSize { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public Post? Post { get; set; }

        [ForeignKey(nameof(UploadedByUser))]
        public Guid UploadedBy { get; set; }
        public AppUser UploadedByUser { get; set; } = null!;
    }
}
