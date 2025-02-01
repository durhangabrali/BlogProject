using Blog.Data.Models.Concrete;
using Blog.Data.Context;
using Blog.Business.Repositories;

namespace Blog.Business.Services
{
    public class CategoryService : Repository<Category> , ICategoryRepository
    {

        public CategoryService(BlogContext context) : base(context)
        {
            
        }
    }
}