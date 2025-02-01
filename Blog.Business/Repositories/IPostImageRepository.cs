using Blog.Data.Models.Concrete;

namespace Blog.Business.Repositories
{
    public interface IPostImageRepository : IRepository<PostImage>
    {
        void  SetFalse(Guid id);   
    }

}