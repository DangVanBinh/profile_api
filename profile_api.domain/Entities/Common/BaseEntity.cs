using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profile_api.domain.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public DateTime DeletedAt { get; set; }
        [Required]
        public bool Active { get; set; }

        public BaseEntity()
        {
            
        }
        public void SoftDelete()
        {
            DeletedAt = DateTime.Now;
            Active = false;
        }
    }
}
