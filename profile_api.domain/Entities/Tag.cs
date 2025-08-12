using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using profile_api.domain.Entities.Common;

namespace profile_api.domain.Entities
{
    public class Tag : BaseEntity
    {
        [Required, StringLength(50)]
        public string? Name { get; set; }

        [Required, StringLength(50)]
        public string? Slug { get; set; }

        public ICollection<PostTag>? PostTags { get; set; }
    }
}
