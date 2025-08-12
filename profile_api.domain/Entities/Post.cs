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
    [Table("Post")]
    public class Post : BaseEntity
    {
        [Required, StringLength(255)]
        public string? Title { get; set; }

        [Required, StringLength(255)]
        public string? Slug { get; set; }

        [Required]
        public string? Content { get; set; }
        public string? Excerpt { get; set; }
        public string? ThumbnailUrl { get; set; }

        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }
        public AppUser? Author { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required]
        public string Status { get; set; } = "draft"; // draft, published, trashed
        public DateTime? PublishedAt { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostTag>? PostTags { get; set; }
        public ICollection<PostImage>? PostFiles { get; set; }
    }
}
