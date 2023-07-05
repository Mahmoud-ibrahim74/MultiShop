using System.Linq.Expressions;

namespace MultiShop.GenericRepository.Base
{
    public interface IRepository<T> where T : class
    {
        T FindById(int? id);
        Task<T> FindByIdAsync(int? id);
        T FindByColumns(Expression<Func<T, bool>> predicate);  
        T SelectNoTracking(Expression<Func<T, bool>> predicate);
        T SelectNoTrackingById(int p_id);


        Task<T> FindByColumnsAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindByColumnsListAsync(Expression<Func<T, bool>> predicate);
        List<T> FindByColumnsList(Expression<Func<T, bool>> predicate);

        Task<List<T>> FindByColumnsListLimitAsync(Expression<Func<T, bool>> predicate,int limit);
        Task<List<T>> OrderByDescListLimitAsync(Expression<Func<T, bool>> predicate, int limit);



        void InsertOne(T product);
        Task InsertOneAsync(T product);
        void UpdateOne(T product);
        Task UpdateOneAsync(T product);
        void DeleteOne(T product);
        Task DeleteOneAsync(T product);
        IEnumerable<T> FindAll();
        Task<List<T>> FindAllAsync();  
        bool ItemExists(Expression<Func<T, bool>> predicate);
    }
}
