using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using profile_api.domain.Entities.Common;

namespace profile_api.domain.Entities
{
    [Table("Category")]
    public class Category : BaseEntity
    {
        [Required, StringLength(100)]
        public string? Name { get; set; }
        [Required, StringLength(100)]
        public string? Slug { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
