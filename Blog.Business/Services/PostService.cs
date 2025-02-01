using Blog.Data.Models.Concrete;
using Blog.Data.Context;
using Blog.Business.Repositories;
namespace Blog.Business.Services
{
    public class PostService : Repository<Post> , IPostRepository
    {

        public PostService(BlogContext context) : base(context)
        {
            
        }
    }
}