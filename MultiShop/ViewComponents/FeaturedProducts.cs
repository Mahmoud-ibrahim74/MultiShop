using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;
using MultiShop.ShopDbContext;

namespace MultiShop.ViewComponents
{
    [ViewComponent(Name = "FeaturedProducts")]
    public class FeaturedProducts : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public FeaturedProducts(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // get products from last week
            return View("Index", await _unitOfWork._UProduct.FindByColumnsListAsync(x =>x.ProductPrice > 10));
        }

    }
}
