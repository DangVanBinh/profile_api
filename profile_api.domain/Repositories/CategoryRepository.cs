using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using profile_api.domain.Entities;
using profile_api.domain.Repositories.Interfaces;

namespace profile_api.domain.Repositories
{
    public class CategoryRepository : RepositoryBase<Category> , ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
