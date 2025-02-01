using Microsoft.Extensions.DependencyInjection;
using Blog.Business.Repositories;
using Blog.Business.Services;
using Blog.Data.Context;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Blog.Root
{
  // Servisler ortak olarak buradaki Root yapısı içerisinde enjekte edilecek
  // Root katmanı hem API tarafında hemde UI tarafında kullanılacak

  public class CompositionRoot
  {
    //public CompositionRoot() {}
    public static void InjectDependecies(IServiceCollection services)
    {
        services.AddScoped<BlogContext>();
        services.AddScoped(typeof(IPostRepository),typeof(PostService ));
        services.AddScoped(typeof(IPostImageRepository),typeof(PostImageService));
        services.AddScoped(typeof(ICategoryRepository),typeof(CategoryService));

        services.AddDbContext<BlogContext>(options => options.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=BlogProjectDb;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;",x => x.MigrationsAssembly("Blog.Ui")));
    }

  }
}