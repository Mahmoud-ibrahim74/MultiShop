using Microsoft.AspNetCore.Mvc;
using MultiShop.GenericRepository.Base;
using MultiShop.ShopDbContext;

namespace MultiShop.ViewComponents
{
    [ViewComponent(Name = "AllProducts")]
    public class AllProducts : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public AllProducts(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync(int route_id)
        {
            if (route_id == 0)
                return View("Index", await _unitOfWork._UProduct.FindAllAsync());

            else
                return View("Index", await _unitOfWork._UProduct.FindByColumnsListAsync(x => x.ProductCategories == route_id));

            // get products from last week

        }

    }
}
