using Blog.Data.Models.Concrete;
using Blog.Data.Context;
using Blog.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Business.Services
{
    public class Repository<T> : IRepository<T> where T : CoreEntity
    {
        private readonly BlogContext _context;
        private DbSet<T> _entities;

        public Repository(BlogContext context)
        {
            this._context = context;
            this._entities = context.Set<T>();
        }

        public void Add(T entity)
        {
            if(entity == null) throw new ArgumentNullException("entity is null");
            _entities.Add(entity);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                RollBack();
            }
            
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
           return _entities.Any(exp);
        }

        public void Delete(Guid id)
        {
            if(id == null) throw new ArgumentNullException("entity is null");
            T entity = _entities.SingleOrDefault(x => x.Id == id);
            if(entity == null) throw new ArgumentNullException("entity is null");
            _entities.Remove(entity);
            _context.SaveChanges();
            
        }

        public IEnumerable<T> GetAll()
        {
           return _entities.AsEnumerable();  
        }

        public T GetById(Guid id)
        {
            if(id == null) throw new ArgumentNullException("entity is null");
            return _entities.SingleOrDefault(x => x.Id == id);
        }

        public void RollBack()
        {
            _context.Dispose();
        }

        public int Save()
        {
           return _context.SaveChanges();
        }

        // İstenilirse Save metodunu Bool tipi döndürecek şekilde de yazılabilir.
        // public bool SaveChanges()
        // {
        //     return _context.SaveChanges() > 0 ? true : false;
        // }

        public void Update(T entity)
        {
            if(entity == null) throw new ArgumentNullException("entity is null");
           _context.Entry(entity).State = EntityState.Modified;
           _context.SaveChanges();
        }
    }

}