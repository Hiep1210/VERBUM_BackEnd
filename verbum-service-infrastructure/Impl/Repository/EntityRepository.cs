using Microsoft.EntityFrameworkCore;
using verbum_service_application.Repository;
using verbum_service_infrastructure.DataContext;

namespace verbum_service_infrastructure.Impl.Repository
{
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        protected readonly verbum_dbContext _context;
        private readonly DbSet<T> _dbSet;

        public EntityRepository(verbum_dbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
