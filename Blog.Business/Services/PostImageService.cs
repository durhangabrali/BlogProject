using Blog.Data.Models.Concrete;
using Blog.Data.Context;
using Blog.Business.Repositories;

namespace Blog.Business.Services
{
    public class PostImageService : Repository<PostImage> , IPostImageRepository
    {

        public PostImageService(BlogContext context) : base(context)
        {
            
        }

        public void SetFalse(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}