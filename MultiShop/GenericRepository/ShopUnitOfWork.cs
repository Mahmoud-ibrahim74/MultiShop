using MultiShop.GenericRepository.Base;
using MultiShop.Models;
using MultiShop.ShopDbContext;

namespace MultiShop.GenericRepository
{
    public class ShopUnitOfWork :IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ShopUnitOfWork(AppDbContext context)
        {
            _context = context;
            _UProduct = new MainRepository<Products>(_context);
            _UCategories = new MainRepository<Categories>(_context);
            _UContactUs = new MainRepository<ContactUs>(_context);
            _UReview  = new MainRepository<ReviewsProducts>(_context);
            _UCart = new MainRepository<Cart>(_context);
        }

        public IRepository<Products> _UProduct { get; private set; }

        public IRepository<Categories> _UCategories { get; private set; }

        public IRepository<ContactUs> _UContactUs { get; private set; }

        public IRepository<ReviewsProducts> _UReview { get; private set; }

        public IRepository<Cart> _UCart { get; private set; }

    public int CommitChanges()
        {        
          return  _context.SaveChanges();
        }

        public async Task<int> CommitChangesAsync()
        {
            return  await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
