using MultiShop.Models;

namespace MultiShop.GenericRepository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Products> _UProduct { get; }
        IRepository<Categories> _UCategories { get; }
        IRepository<ContactUs> _UContactUs { get; }
        IRepository<ReviewsProducts> _UReview { get; }
        IRepository<Cart> _UCart { get; }
        int CommitChanges();
        Task<int> CommitChangesAsync();
        
    }
}
