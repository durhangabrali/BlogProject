using Microsoft.EntityFrameworkCore;
using Blog.Data.Models.Concrete;

namespace Blog.Data.Context
{
    public class BlogContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostImage> PostImages { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            
        }
   }

}