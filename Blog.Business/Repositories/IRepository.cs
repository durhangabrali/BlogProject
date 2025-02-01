using System.Linq.Expressions;
using Blog.Data.Models.Concrete;

namespace Blog.Business.Repositories
{
    public interface IRepository<T> where T : CoreEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
        T GetById(Guid id);
        IEnumerable<T> GetAll();

        int Save();
        void RollBack();
        bool Any(Expression<Func<T,bool>> exp);
    }    
}