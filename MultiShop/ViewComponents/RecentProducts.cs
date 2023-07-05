using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;
using MultiShop.ShopDbContext;

namespace MultiShop.ViewComponents
{
    [ViewComponent(Name = "RecentProducts")]
    public class RecentProducts : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public RecentProducts(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // get products from last week
            return View("Index", await _unitOfWork._UProduct.FindByColumnsListLimitAsync(x => x.ProductCreation >= DateTime.Now.AddDays(-8), 8));
        }

    }
}
