using System.Collections.Generic;

namespace verbum_service_application.Repository
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class
    {
        //private readonly DbContext _context;
        //private readonly DbSet<T> _dbSet;

        //public EntityFrameworkRepository(DbContext context)
        //{
        //    _context = context;
        //    _dbSet = _context.Set<T>();
        //}

        //public T GetById(int id)
        //{
        //    return _dbSet.Find(id);
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    return _dbSet.ToList();
        //}

        //public void Add(T entity)
        //{
        //    _dbSet.Add(entity);
        //}

        //public void Update(T entity)
        //{
        //    _context.Entry(entity).State = EntityState.Modified;
        //}

        //public void Delete(T entity)
        //{
        //    _dbSet.Remove(entity);
        //}
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
