using Microsoft.EntityFrameworkCore;
using MultiShop.GenericRepository.Base;
using MultiShop.ShopDbContext;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MultiShop.GenericRepository
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _appDbContext;
        public MainRepository(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        public IEnumerable<T> FindAll()
        {
            return _appDbContext.Set<T>().ToList();
        }

        public Task<List<T>> FindAllAsync()
        {
            return _appDbContext.Set<T>().ToListAsync();
        }
        public T FindById(int? id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public async Task<T> FindByIdAsync(int? id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }
        public T FindByColumns(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().SingleOrDefault(predicate);
        }

        public async Task<T> FindByColumnsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appDbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }
        public List<T> FindByColumnsList(Expression<Func<T, bool>> predicate)
        {
            var query = _appDbContext.Set<T>().Where(predicate);
            return query.ToList();
        }
        public async Task<List<T>> FindByColumnsListAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _appDbContext.Set<T>().Where(predicate);
            return await query.ToListAsync();
        }
        public async Task<List<T>> FindByColumnsListLimitAsync(Expression<Func<T, bool>> predicate, int limit)
        {
            var query = _appDbContext.Set<T>().Where(predicate).Take(limit);
            return await query.ToListAsync();
        }
        public void InsertOne(T product)
        {
            _appDbContext.Set<T>().Add(product);
            _appDbContext.SaveChanges();
        }

        public void UpdateOne(T product)
        {
            _appDbContext.Set<T>().Update(product);
            _appDbContext.SaveChanges();
        }

        public void DeleteOne(T product)
        {
            _appDbContext.Set<T>().Remove(product);
            _appDbContext.SaveChanges();
        }

        public async Task InsertOneAsync(T product)
        {
            _appDbContext.Set<T>().Add(product);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateOneAsync(T product)
        {
            _appDbContext.Set<T>().Update(product);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteOneAsync(T product)
        {
            _appDbContext.Set<T>().Remove(product);
            await _appDbContext.SaveChangesAsync();

        }

        public bool ItemExists(Expression<Func<T, bool>> predicate)
        {
            return (_appDbContext.Set<T>()?.Any(predicate)).GetValueOrDefault();
        }

        public T SelectNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().AsNoTracking().SingleOrDefault(predicate);

        }

        public T SelectNoTrackingById(int p_id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> OrderByDescListLimitAsync(Expression<Func<T, bool>> predicate, int limit)
        {
            var query = _appDbContext.Set<T>().Where(predicate).Take(limit);
            return await query.ToListAsync();
        }
    }
}
